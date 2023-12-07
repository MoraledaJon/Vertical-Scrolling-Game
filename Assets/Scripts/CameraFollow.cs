using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float verticalFollowOffsetPercent = 0.15f; // 10% above the middle

    void Update()
    {
        // Calculate the threshold above the middle of the camera
        float threshold = Camera.main.orthographicSize * verticalFollowOffsetPercent;

        if(player)
        {
            // Check if the player is above the threshold
            if (player.position.y > transform.position.y + threshold)
            {
                // Move the camera up with the player
                Vector3 newPosition = new Vector3(transform.position.x, player.position.y - threshold, transform.position.z);
                transform.position = newPosition;
            }
        }
    }
}