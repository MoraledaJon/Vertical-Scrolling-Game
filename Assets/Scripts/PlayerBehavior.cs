using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public float bounceForce = 10f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Calculate screen boundaries based on camera size and aspect ratio
        float screenWidth = Camera.main.orthographicSize * Camera.main.aspect;

        // Adjust the maximum horizontal position dynamically
        float maxHorizontalPosition = screenWidth - 0.25f;

        // Check and limit the horizontal position
        float clampedX = Mathf.Clamp(transform.position.x, -maxHorizontalPosition, maxHorizontalPosition);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision is with the floor or the drawn line
        if (collision.gameObject.CompareTag("Floor") || collision.gameObject.CompareTag("DrawnLine"))
        {
            // Apply a bounce force
            BounceOffWall(collision.contacts[0].normal);
        }
    }

    private void BounceOffWall(Vector2 collisionNormal)
    {
        // Calculate the bounce direction using the collision normal
        Vector2 bounceDirection = Vector2.Reflect(rb.velocity.normalized, collisionNormal);

        // Apply the bounce force
        rb.velocity = bounceDirection * bounceForce;
    }
}