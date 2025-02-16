using System;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectIdentifier : MonoBehaviour
{
    private GameObject box;

    private SpriteRenderer image;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       box = this.gameObject; 
       image = box.transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        image.sortingOrder = 1;
        
    }
}
