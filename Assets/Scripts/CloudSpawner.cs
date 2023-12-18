using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    public GameObject cloudPrefab;
    public float spawnInterval = 2.0f; // Time between each spawn
    public float minCloudSpeed = 0.5f; // Minimum speed of the cloud
    public float maxCloudSpeed = 2.0f; // Maximum speed of the cloud
    public float minZPosition = -1f;   // Minimum Z position (closer to the camera)
    public float maxZPosition = 1f;    // Maximum Z position (farther from the camera)
	public float minScale = 0.1f;
	public float maxScale = 1f;
	public float spawnMaxY = 2f;
    private float screenWidthWorldUnits;
	public GameObject cam;

	private bool isPaused = false;

    private void Start()
    {
        screenWidthWorldUnits = Camera.main.orthographicSize * Camera.main.aspect;
        StartCoroutine(SpawnClouds());
    }

    private IEnumerator SpawnClouds()
    {
        while (true)
        {
            
			if (!isPaused) // Check if not paused
            {
				float scale = Random.Range(minScale, maxScale); // Scale for size
				float speed = Mathf.Lerp(maxCloudSpeed, minCloudSpeed, scale - minScale);

				float spawnYPosition = Random.Range(0, spawnMaxY); // Y position range

				// Correctly map scale to Z position
				float normalizedScale = (scale - minScale) / (maxScale - minScale); // Normalize scale value to 0-1 range
				float spawnZPosition = Mathf.Lerp(minZPosition, maxZPosition, normalizedScale);

				// Calculate additional offset to ensure the cloud is off-screen
				GameObject tempCloud = Instantiate(cloudPrefab, Vector3.zero, Quaternion.identity);
				tempCloud.transform.localScale = new Vector3(scale, scale, 1);
				float offset = tempCloud.GetComponent<SpriteRenderer>().bounds.extents.x;
				Destroy(tempCloud);

				int direction = Random.Range(0, 2) * 2 - 1; // -1 (left) or 1 (right)
				Vector3 spawnPosition = new Vector3(screenWidthWorldUnits * direction + offset * direction, cam.transform.position.y + spawnYPosition, spawnZPosition);

				GameObject cloud = Instantiate(cloudPrefab, spawnPosition, Quaternion.identity);
				cloud.transform.localScale = new Vector3(scale, scale, 1);

				Rigidbody2D rb = cloud.GetComponent<Rigidbody2D>();
				rb.velocity = new Vector2(-direction * speed, 0);
			
			}
			
			yield return new WaitForSeconds(spawnInterval);
        }
    }
	
	public void PauseSpawning()
    {
        isPaused = true;
    }

    public void ResumeSpawning()
    {
        isPaused = false;
    }
	
	public void PauseAllClouds()
	{
		Cloud[] clouds = FindObjectsOfType<Cloud>();
		foreach (Cloud cloud in clouds)
		{
			cloud.PauseMovement();
		}
	}

	public void ResumeAllClouds()
	{
		Cloud[] clouds = FindObjectsOfType<Cloud>();
		foreach (Cloud cloud in clouds)
		{
			cloud.ResumeMovement();
		}
	}
}