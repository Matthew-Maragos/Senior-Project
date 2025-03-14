using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class MemoryGameSetup : MonoBehaviour
{
    public Sprite[] numberSprites;
    public List<Sprite> imageSprites;
    [SerializeField] private Vector2 _initialPositions = new Vector2(-8, 4);
    private int colLength = 5;
    
    //X and Y spacing for the boxes
    [SerializeField] private Vector2 _spacing;
    
    //X: MinWidth, Y: MinHeight, Z: MaxWidth, W: MaxHeight
    [SerializeField] private Vector4 _minAndMaxWidthsAndHeights;
    
    //Variables for expanding and contraction of the image
    [SerializeField] private float expansionMultiplier = 1.5f;
    private float expansionDuration = 0.5f;
    private float contractionDuration = 0.5f;
    
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
        //Get boxes from the pool instead of instantiating them
        for (int i = 0; i < numBoxes; i++)
        {
            GameObject newBox = ObjectPooler.Instance.GetPooledObject();
            if (newBox == null)
            {
                Debug.LogWarning("No pooled object avaliable");
                continue;
            }
            
            //Set the box's position in the grid
            newBox.transform.position =
                new Vector3(_initialPositions.x + (_spacing.x * (i % colLength)), _initialPositions.y + (-_spacing.y * (i / colLength)), 0);
            newBox.SetActive(true);//Activate the box
            
            //Set the numbers for the front of the boxes
            SpriteRenderer boxSpriteRenderer = newBox.GetComponent<SpriteRenderer>();
            boxSpriteRenderer.sprite = numberSprites[i];
            
            //Set the images for the back of the boxes
            SpriteRenderer image = newBox.transform.GetChild(0).GetComponent<SpriteRenderer>();
            int randIndx = Random.Range(0, imageSprites.Count);
            image.sprite = imageSprites[randIndx];
            image.sortingOrder = -2;
            imageSprites.RemoveAt(randIndx);
            
            //Calculate the sprite's original dimensions (in world units)
            float spriteWidth = image.sprite.bounds.size.x;
            float spriteHeight = image.sprite.bounds.size.y;
            
            //Determine the maximum scale factor so that the image doesn't exceed maxwidth and maxheight
            float maxScale = Mathf.Min(_minAndMaxWidthsAndHeights.z / spriteWidth, _minAndMaxWidthsAndHeights.w / spriteHeight);
            
            //Determine the minimum scale factor so that the image meets minwidth and minheight
            float minScale = Mathf.Max(_minAndMaxWidthsAndHeights.x / spriteWidth, _minAndMaxWidthsAndHeights.y / spriteHeight);
            
            //The image's original scale is 1. So if that scale is within our minScale and maxScale, keep it. Otherwise, clamp it to the appropriate boundary
            float scaleFactor = Mathf.Clamp(1f, minScale, maxScale);
            
            //Apply the uniform scale so that we preserve the sprite's aspect ratio
            image.transform.localScale = new Vector3(scaleFactor, scaleFactor, 1);
            
            BoxHandler bh = newBox.GetComponent<BoxHandler>();
            if (bh != null)
            {
                bh.ResetAvaliability();
            }
        }
    }

    //All the getters are below. First one is returning the number of boxes.
    public int GetNumBoxes()
    {
        return numBoxes;
    }

    public float GetExpansionDuration()
    {
        return expansionDuration;
    }

    public float GetExpansionMultiplier()
    {
        return expansionMultiplier;
    }

    public float GetContractionDuration()
    {
        return contractionDuration;
    }
}
