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
        
        //Measure its' width for spacing
        SpriteRenderer sr = firstLevel.GetComponent<SpriteRenderer>();
        tileWidth = (sr != null) ? sr.sprite.bounds.size.x : 20f;
        
        //Compute where the next level should appear
        nextSpawnX = (firstLevel.transform.position.x + tileWidth) + 55f;
        
        StartCoroutine(SpawnLoop());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void SpawnLevel()
    {
        GameObject prefab = levelObjects[Random.Range(0, levelObjects.Count)];
        
        //Reposition the levels for different spawnings
        switch (prefab.name)
        {
            case "FirstLevel":
                spawnY = -2.5f;
                break;
            case "SecondLevel":
                spawnY = 6.42f;
                break;
            case "ThirdLevel":
                spawnY = 2.7f;
                break;
            case "FourthLevel":
                spawnY = 2.18f;
                break;
            case "FifthLevel 1":
                spawnY = 0.71f;
                break;
            case "FifthLevel2":
                spawnY = 0.71f;
                break;
            case "FifthLevel3":
                spawnY = 0.71f;
                break;
            case "FifthLevel4":
                spawnY = 0.71f;
                break;
            case "FifthLevel5":
                spawnY = 0.71f;
                break;
            case "FifthLevel6":
                spawnY = 0.71f;
                break;
        }
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
