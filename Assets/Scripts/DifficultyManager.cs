using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public Camera cam;
    private float lastYPosition;
    private const float MaxYPosition = 500;

    private void Start()
    {
        lastYPosition = cam.transform.position.y;
    }

    private void Update()
    {
        float currentYPosition = cam.transform.position.y;

        // Check if the current position is less than the maximum threshold
        if (currentYPosition <= MaxYPosition && currentYPosition - lastYPosition >= 50f)
        {
            // Increase difficulty settings for cloud spawner
            CloudSpawner.Instance.spawnInterval += 0.5f;
            CloudSpawner.Instance.spawnIntervalBlack -= 0.5f;

            // Increase player's movement speed
            PlayerBehavior.Instance.IncreaseMovementSpeed(1.1f);

            // Update the last Y position
            lastYPosition = currentYPosition;
        }
    }
}