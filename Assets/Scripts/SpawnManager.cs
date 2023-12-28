using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] itemsToSpawn;
    public GameObject[] coinToSpawn;
    public float spawnFirstItem = 5f;
    public float spawnInterval = 15f;

    public float spawnFirstCoin = 5f;
    public float spawnIntervalCoin = 15f;
    public bool isSpawning = false;
    public bool isPaused = false;

    public float movementDistance = 1f; // Distance to move left and right
    public float movementDuration = 2f; // Duration of one move to the left or right

    private Camera mainCamera;

    public static SpawnManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        mainCamera = Camera.main;
    }

    public void StartSpawning()
    {
        if (isSpawning)
            return;

        isSpawning = true;
        StartCoroutine(SpawnItemsRoutine());
        StartCoroutine(SpawnCoinCoroutine());
    }

    public void PauseSpawning()
    {
        isPaused = true;
    }

    public void ResumeSpawning()
    {
        isPaused = false;
    }

    IEnumerator SpawnItemsRoutine()
    {
        yield return new WaitForSeconds(spawnFirstItem);

        while (isSpawning)
        {
            while (isPaused)
            {
                yield return null;
            }

            SpawnRandomItem();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    IEnumerator SpawnCoinCoroutine()
    {
        yield return new WaitForSeconds(spawnFirstCoin);

        while (isSpawning)
        {
            while (isPaused)
            {
                yield return null;
            }

            SpawnCoin();
            yield return new WaitForSeconds(spawnIntervalCoin);
        }
    }

    void SpawnRandomItem()
    {
        SpawnObject(itemsToSpawn);
    }

    void SpawnCoin()
    {
        SpawnObject(coinToSpawn);
    }

    void SpawnObject(GameObject[] objectsToSpawn)
    {
        if (objectsToSpawn.Length == 0)
        {
            return;
        }

        int randomIndex = Random.Range(0, objectsToSpawn.Length);
        GameObject itemToSpawn = objectsToSpawn[randomIndex];

        Vector3 spawnPosition = GetRandomSpawnPosition();
        GameObject spawnedObject = Instantiate(itemToSpawn, spawnPosition, Quaternion.identity);

        StartCoroutine(MoveObjectLeftRight(spawnedObject.transform));
    }

    Vector3 GetRandomSpawnPosition()
    {
        float screenWidth = mainCamera.orthographicSize * mainCamera.aspect * 2f;
        // Adjust the spawn position to account for movement distance
        float adjustedScreenWidth = screenWidth - movementDistance * 2f;
        float randomX = Random.Range(-adjustedScreenWidth / 2f, adjustedScreenWidth / 2f);
        float spawnY = mainCamera.transform.position.y + 10f;

        return new Vector3(randomX, spawnY, 0f);
    }

    IEnumerator MoveObjectLeftRight(Transform objectTransform)
    {
        Vector3 startPosition = objectTransform.position;
        Vector3 leftPosition = startPosition + new Vector3(-movementDistance, 0, 0);
        Vector3 rightPosition = startPosition + new Vector3(movementDistance, 0, 0);

        while (true)
        {
            // Check if the object still exists
            if (objectTransform == null) yield break;

            yield return MoveToPosition(objectTransform, leftPosition);

            // Check again as the object could have been destroyed during the MoveToPosition coroutine
            if (objectTransform == null) yield break;

            yield return MoveToPosition(objectTransform, rightPosition);
        }
    }

    IEnumerator MoveToPosition(Transform objectTransform, Vector3 targetPosition)
    {
        float elapsedTime = 0;
        Vector3 startingPos = objectTransform.position;

        while (elapsedTime < movementDuration)
        {
            // Check if the object still exists
            if (objectTransform == null) yield break;

            float fraction = elapsedTime / movementDuration;
            float smoothFraction = Mathf.SmoothStep(0, 1, fraction);

            objectTransform.position = Vector3.Lerp(startingPos, targetPosition, smoothFraction);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Final position set, check for null one last time
        if (objectTransform != null)
        {
            objectTransform.position = targetPosition;
        }
    }
}