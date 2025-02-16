using System;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using Random = Unity.Mathematics.Random;

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
    [SerializeField] private float minWidth;
    [SerializeField] private float minHeight;
    [SerializeField] private float maxWidth;
    [SerializeField] private float maxHeight;
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
            GameObject newBox = Instantiate(box, new Vector3(initX + (XSpace * (i % colLength)), initY + (-YSpace * (i / colLength)), 0), Quaternion.identity);
            
            //Set the numbers for the front of the boxes
            SpriteRenderer boxsprrenderer = newBox.GetComponent<SpriteRenderer>();
            boxsprrenderer.sprite = numberSprites[i];
            
            //Set the images for the back of the boxes
            SpriteRenderer image = newBox.transform.GetChild(0).GetComponent<SpriteRenderer>();
            image.sprite = imageSprites[i];
            
            //Calculate the sprite's original dimensions (in world units)
            float spriteWidth = image.sprite.bounds.size.x;
            float spriteHeight = image.sprite.bounds.size.y;
            
            //Determine the maximum scale factor so that the image doesn't exceed maxwidth and maxheight
            float maxScale = Mathf.Min(maxWidth / spriteWidth, maxHeight / spriteHeight);
            
            //Determine the minimum scale factor so that the image meets minwidth and minheight
            float minScale = Mathf.Max(minWidth / spriteWidth, minHeight / spriteHeight);
            
            //The image's original scale is 1. So if that scale is within our minScale and maxScale, keep it. Otherwise, clamp it to the appropriate boundary
            float scaleFactor = Mathf.Clamp(1f, minScale, maxScale);
            
            //Apply the uniform scale so that we preserve the sprite's aspect ratio
            image.transform.localScale = new Vector3(scaleFactor, scaleFactor, 1);
            
            Debug.Log("Box " + (i + 1) + " has " + image.sprite.name);
            
        }
    }
    
}
