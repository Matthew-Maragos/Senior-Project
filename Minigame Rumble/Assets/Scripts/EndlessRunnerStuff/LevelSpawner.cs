using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    [SerializeField] public Transform spawnPoint;
    [SerializeField] public Transform triggerZone;
    [SerializeField] public GameObject[] levelPrefabs;
    [SerializeField] public float levelLength = 30f; // Same for all level prefabs
    [SerializeField] public int maxSpawnedLevels = 5;[SerializeField] public GameObject firstLevelPrefab;
    
    private Queue<GameObject> spawnedLevels = new Queue<GameObject>();
    private Vector3 nextSpawnPos;

    void Start()
    {
        nextSpawnPos = spawnPoint.position;

        // Spawn the first level manually
        GameObject firstLevel = Instantiate(firstLevelPrefab, nextSpawnPos, Quaternion.identity);
        spawnedLevels.Enqueue(firstLevel);
        nextSpawnPos += new Vector3(levelLength, 0f, 0f);

        // Spawn the rest randomly
        for (int i = 1; i < maxSpawnedLevels; i++)
        {
            SpawnNextSegment();
        }
    }

    void Update()
    {
        if (triggerZone.position.x >= nextSpawnPos.x - levelLength)
        {
            SpawnNextSegment();
        }
    }

    void SpawnNextSegment()
    {
        int randIndex = Random.Range(0, levelPrefabs.Length);
        GameObject newLevel = Instantiate(levelPrefabs[randIndex], nextSpawnPos, Quaternion.identity);
        spawnedLevels.Enqueue(newLevel);

        nextSpawnPos += new Vector3(levelLength, 0f, 0f);

        if (spawnedLevels.Count > maxSpawnedLevels)
        {
            GameObject oldLevel = spawnedLevels.Dequeue();
            Destroy(oldLevel);
        }
    }
}
