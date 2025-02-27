using UnityEngine;
using System.Collections.Generic;
public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance;
    public GameObject boxPrefab;
    public int poolSize = 20;
    private List<GameObject> pool;

    private void Awake()
    {
        Instance = this;
        pool = new List<GameObject>();

        //Pre-instantiate pool size boxes and instantiate them
        for (int i = 0; i < poolSize; i++)
        {
            GameObject box = Instantiate(boxPrefab);
            box.SetActive(false);
            pool.Add(box);
        }
    }

    //Retrieve an Inactive box from the pool
    public GameObject GetPooledObject()
    {
        foreach (GameObject box in pool)
        {
            if (!box.activeInHierarchy)
            {
                return box;
            }
        }

        return null;
    }

    public void ReturnPooledObject(GameObject box)
    {
        box.SetActive(false);
    }
}
