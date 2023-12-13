using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBackgroundManager : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Camera mainCamera;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        mainCamera = Camera.main;

        FitImageToScreen();
    }

    void FitImageToScreen()
    {
        float cameraHeight = mainCamera.orthographicSize * 2f;
        float cameraWidth = cameraHeight * mainCamera.aspect;

        // Use the size of the sprite instead of bounds
        Vector2 spriteSize = spriteRenderer.size / spriteRenderer.sprite.pixelsPerUnit;

        float scaleX = cameraWidth / spriteSize.x;
        float scaleY = cameraHeight / spriteSize.y;

        transform.localScale = new Vector3(scaleX, scaleY, 1f);
    }
}