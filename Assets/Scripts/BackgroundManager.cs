using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class BackgroundManager : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Camera mainCamera;

    private float scaleX;
    private float scaleY;

    public Sprite[] Skyes;
    public GameObject skyPrefab;

    private float background_sizey = 0;

    private  float maxBackgroundyY = 0;
    private List<GameObject> createdSkyesList = new List<GameObject>();

    public Transform playerTransform; // Reference to the player's transform
    private float lastSkyBottomPosition; // Store the bottom position of the last created sky

    private int currentSky = 0;
    private int skyCount = 0;
    private bool skyTransition = false;

    public int skyesToShow = 5;
	
	public bool isTitleScreen = false;


    void Start()
    {
		if(isTitleScreen)
		{
			spriteRenderer = GetComponent<SpriteRenderer>();
			mainCamera = Camera.main;

			FitImageToScreen();
		}
		else
		{
			spriteRenderer = GetComponent<SpriteRenderer>();
			mainCamera = Camera.main;

			FitImageToScreen();
			CreateFirstSky();
		}
    }

    void Update()
    {
		if(isTitleScreen)
			return;
		
        if(playerTransform.transform.position.y > maxBackgroundyY - background_sizey)
        {
            if (skyCount % skyesToShow == 0)
            {
                currentSky++;
                skyTransition = true;
            }

            GameObject newBackground = Instantiate(skyPrefab, new Vector3(transform.position.x, maxBackgroundyY, transform.position.z), Quaternion.identity);
            newBackground.GetComponent<SpriteRenderer>().sprite = Skyes[currentSky];
            newBackground.transform.localScale = new Vector3(scaleX, scaleY, 1f);
            maxBackgroundyY += background_sizey;
            createdSkyesList.Add(newBackground);
            skyCount++;

            if (skyTransition)
            {
                currentSky++;
                skyTransition = false;
            }


            if (createdSkyesList.Count > 3)
            {
                Destroy(createdSkyesList[0]);
                createdSkyesList.RemoveAt(0);
            }
        }
    }

    void FitImageToScreen()
    {
        float cameraHeight = mainCamera.orthographicSize * 2f;
        float cameraWidth = cameraHeight * mainCamera.aspect;

        // Adjust the scale of the image to fit the screen
        float spriteWidth = spriteRenderer.bounds.size.x;
        float spriteHeight = spriteRenderer.bounds.size.y;

        scaleX = cameraWidth / spriteWidth;
        scaleY = cameraHeight / spriteHeight;

        transform.localScale = new Vector3(scaleX, scaleY, 1f);
    }

    void CreateFirstSky()
    {
        if (Skyes != null && Skyes.Length > 0 && skyPrefab != null)
        {
            Sprite firstSkySprite = Skyes[0];
            background_sizey = spriteRenderer.bounds.size.y;

            float spawnY = transform.position.y + background_sizey;

            maxBackgroundyY = background_sizey * 2;

            Vector3 spawnPosition = new Vector3(transform.position.x, spawnY, transform.position.z);


            GameObject newSky = Instantiate(skyPrefab, spawnPosition, Quaternion.identity);

            SpriteRenderer newSpriteRenderer = newSky.GetComponent<SpriteRenderer>();
            if (newSpriteRenderer == null)
            {
                newSpriteRenderer = newSky.AddComponent<SpriteRenderer>();
            }


            newSpriteRenderer.sprite = firstSkySprite;

            newSky.transform.localScale = new Vector3(scaleX, scaleY, 1f);

            createdSkyesList.Add(newSky);

            lastSkyBottomPosition = newSky.transform.position.y; 

        }
    }

}
