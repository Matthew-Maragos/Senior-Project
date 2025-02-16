using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BoxClicker : MonoBehaviour
{
    public Sprite[] numberSprites;
    public List<Sprite> imageSprites;
    public GameObject box;
    private float initX = -8;
    private float initY = 4;
    private int colLength = 5;
    [SerializeField] private float XSpace;
    [SerializeField] private float YSpace;
    private int numBoxes = 20;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    { 
        SpawnBoxes();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnBoxes()
    {
        for (int i = 0; i < numBoxes; i++)
        { 
            box.GetComponent<SpriteRenderer>().sprite = numberSprites[i];
            Instantiate(box, new Vector3(initX + (XSpace * (i % colLength)), initY + (-YSpace * (i / colLength)), 0), Quaternion.identity);
        }
    }
    
}
