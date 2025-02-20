using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoxHandler : MonoBehaviour
{
    private GameObject box;
    private bool isAvaliable;
    private SpriteRenderer image;

    private List<GameObject> chosenBoxes;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       box = this.gameObject; 
       image = box.transform.GetChild(0).GetComponent<SpriteRenderer>();
       isAvaliable = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (isAvaliable)
        {
            ShowImage();
            chosenBoxes.Add(box);
            isAvaliable = !isAvaliable;
        }
        
    }
    

    public void EmptyChosenItemsList()
    {
        chosenBoxes.Clear();
    }

    public List<GameObject> GetChosenItems()
    {
        return chosenBoxes;
    }
    public void ShowImage()
    {
        image.sortingOrder = 1;
    }
    public void ResetBox()
    {
        image.sortingOrder = -1;
        isAvaliable = true;
    }
    
}
