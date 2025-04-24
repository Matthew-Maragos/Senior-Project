using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    public GameObject levelToSpawnFirst;
    public List<GameObject> levelObjects;
    private List<GameObject> levelHeiarchy;
    [SerializeField] private float spawnY;
    [SerializeField] private float spawnInterval = 3f;
    [SerializeField] private int maxLevelsOnScreen = 5;
    private float tileWidth;
    private float nextSpawnX;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        levelHeiarchy = new List<GameObject>();
        
        //Spawn the first level and store it
        GameObject firstLevel = Instantiate(levelToSpawnFirst, new Vector3(0, spawnY, 0), Quaternion.identity);
        levelHeiarchy.Add(firstLevel);
        
        //Measure its width for spacing
        SpriteRenderer sr = firstLevel.GetComponent<SpriteRenderer>();
        tileWidth = (sr != null) ? sr.sprite.bounds.size.x : 10f;
        
        //Compute where the next level should appear
        nextSpawnX = (firstLevel.transform.position.x + tileWidth);
       //nextSpawnX = -20f;
        StartCoroutine(SpawnLoop());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void SpawnLevel()
    {
        GameObject prefab = levelObjects[Random.Range(0, levelObjects.Count)];
        GameObject inst = Instantiate(prefab, new Vector3(nextSpawnX, spawnY, 0), Quaternion.identity);
        
        levelHeiarchy.Add(inst);
        nextSpawnX += tileWidth;
    }
    
    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnLevel();
            CleanupOldest();
        }
        
    }

    private void CleanupOldest()
    {
        if (levelHeiarchy.Count > maxLevelsOnScreen)
        {
            GameObject oldest = levelHeiarchy[0];
            levelHeiarchy.RemoveAt(0);
            Destroy(oldest);
        }
    }
    
}
