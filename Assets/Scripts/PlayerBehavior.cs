using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public float bounceForce = 10f;
    public float bounceForceTurbo = 20f;
    public Rigidbody2D rb;
    private GameObject floorGameObject;
    private Animator anim;

    public SpawnManager spawnManager;
    public GameOverManager gameOverManager;
	public CloudSpawner cloudSpawner;

    public ItemManager itemManager;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        // Check if the collision is with the floor or the drawn line
        if (collision.gameObject.CompareTag("Floor"))
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f); 
            rb.AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);
            anim.SetTrigger("start");
            if(!floorGameObject)
            {
                floorGameObject = collision.gameObject;
            }
        }

        else if(collision.gameObject.CompareTag("DrawnLine"))
        {
            Handheld.Vibrate();
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);
            Destroy(collision.gameObject);
            anim.SetTrigger("start");
			SoundManager.Instance.PlayBounceSound();
            
            if(!spawnManager.isSpawning)
            {
                spawnManager.StartSpawning();
				cloudSpawner.spawnMaxY = 30f;
				cloudSpawner.spawnInterval = 1f;
            }

            if (floorGameObject)
            {
                Destroy(floorGameObject);
            }
        }

        else if (collision.gameObject.CompareTag("turbo"))
        {
            SoundManager.Instance.PlayTurboItemSound();
            itemManager.Turbo_Item(rb, bounceForceTurbo, collision.gameObject);
            anim.SetTrigger("start");
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Half_Line"))
        {
			SoundManager.Instance.HalfLineItemSound();
            itemManager.Half_Line_Item(collision.gameObject);
        }
    }

    void FixedUpdate()
    {
        // Calculate screen boundaries based on camera size and aspect ratio
        float screenWidth = Camera.main.orthographicSize * Camera.main.aspect;

        // Adjust the maximum horizontal position dynamically
        float maxHorizontalPosition = screenWidth - 0.25f;

        // Check and limit the horizontal position
        float clampedX = Mathf.Clamp(transform.position.x, -maxHorizontalPosition, maxHorizontalPosition);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);

        // Check if the player is at the screen borders
        if (Mathf.Approximately(clampedX, -maxHorizontalPosition) || Mathf.Approximately(clampedX, maxHorizontalPosition))
        {
            rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y);
        }

        // Check if the player is below the camera's view and destroy it
        if (transform.position.y < Camera.main.transform.position.y - Camera.main.orthographicSize)
        {
            if(!gameOverManager.isGameOver)
            {
                gameOverManager.GameOver();
                Destroy(transform.gameObject);
            }
        }

    }
}