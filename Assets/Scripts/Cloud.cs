using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    private Camera mainCamera;
    private float screenWidthWorldUnits;
    private bool hasBeenOnScreen;

    void Start()
    {
        mainCamera = Camera.main;
        screenWidthWorldUnits = mainCamera.orthographicSize * mainCamera.aspect;
        hasBeenOnScreen = false;
    }

    void Update()
    {
        // Check if the cloud is on screen
        if (!hasBeenOnScreen && IsCloudOnScreen())
        {
            hasBeenOnScreen = true;
        }

        // Destroy the cloud if it has been on screen and now is fully off-screen
        if (hasBeenOnScreen && !IsCloudOnScreen())
        {
            Destroy(gameObject);
        }
    }

    private bool IsCloudOnScreen()
    {
        float cloudHalfWidth = transform.localScale.x * GetComponent<SpriteRenderer>().sprite.bounds.extents.x;
        float cloudRightEdge = transform.position.x + cloudHalfWidth;
        float cloudLeftEdge = transform.position.x - cloudHalfWidth;

        bool isCloudOffScreenLeft = cloudRightEdge < -screenWidthWorldUnits;
        bool isCloudOffScreenRight = cloudLeftEdge > screenWidthWorldUnits;

        // Return false (off-screen) only if the cloud is fully off the screen on either side
        return !(isCloudOffScreenLeft || isCloudOffScreenRight);
    }
}