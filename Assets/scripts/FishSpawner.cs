using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    public GameObject fishPrefab;
    public int numberOfFish = 10;
    private Rect spawnBounds;
    // Start is called before the first frame update
    void Start()
    {
        FishController fishController = fishPrefab.GetComponent<FishController>();
        spawnBounds = fishController.moveBounds;
        for (int i = 0; i < numberOfFish; i++)
        {
            SpawnFish();
        }
    }

    void SpawnFish()
    {
        Vector2 randomPos = new Vector2(
            Random.Range(spawnBounds.min.x + 1, spawnBounds.max.x - 1),
            Random.Range(spawnBounds.min.y + 1, spawnBounds.max.y - 1)
        );

        Instantiate(fishPrefab, randomPos, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
