using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyIfOffScreen : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        float lowerBound = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane)).y;

        if (transform.position.y < lowerBound)
        {
            Destroy(gameObject);
        }
    }
}