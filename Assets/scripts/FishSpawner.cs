using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    public GameObject fishPrefab;
    public int numberOfFish = 10;
    private Bounds spawnBounds;
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
            Random.Range(spawnBounds.min.x, spawnBounds.max.x),
            Random.Range(spawnBounds.min.y, spawnBounds.max.y)
        );

        Instantiate(fishPrefab, randomPos, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
