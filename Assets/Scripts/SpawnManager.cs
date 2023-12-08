using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] itemsToSpawn;
    public float spawnFirstItem = 5f;
    public float spawnInterval = 15f;
    public bool isSpawning = false;
    public bool isPaused = false;

    private Camera mainCamera;

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
    }

    public void PauseSpawning()
    {
        isPaused = true;
    }

    public void ResumeSpawning()
    {
        isPaused = false;
    }

    System.Collections.IEnumerator SpawnItemsRoutine()
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

    void SpawnRandomItem()
    {
        if (itemsToSpawn.Length == 0)
        {
            return;
        }

        int randomIndex = Random.Range(0, itemsToSpawn.Length);
        GameObject itemToSpawn = itemsToSpawn[randomIndex];

        float screenWidth = mainCamera.orthographicSize * mainCamera.aspect * 2f;
        float randomX = Random.Range(-screenWidth / 2f, screenWidth / 2f);

        float spawnY = mainCamera.transform.position.y + 10f;

        Vector3 spawnPosition = new Vector3(randomX, spawnY, 0f);
        Instantiate(itemToSpawn, spawnPosition, Quaternion.identity);
    }
}