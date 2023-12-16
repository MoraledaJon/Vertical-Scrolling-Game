using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float verticalFollowOffsetPercent = 0.15f; // 10% above the middle

    void Update()
    {
        if(GameOverManager.instance.isGameOver)
        {
            return;
        }
        float threshold = Camera.main.orthographicSize * verticalFollowOffsetPercent;

        if(player)
        {
            if (player.position.y > transform.position.y + threshold)
            {
                Vector3 newPosition = new Vector3(transform.position.x, player.position.y - threshold, transform.position.z);
                transform.position = newPosition;
            }
        }
    }
}