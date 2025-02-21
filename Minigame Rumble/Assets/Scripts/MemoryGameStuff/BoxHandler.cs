using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoxHandler : MonoBehaviour
{
    [SerializeField] private GameObject box;
    private bool isAvaliable;
    public SpriteRenderer image;
    private List<GameObject> chosenBoxes;
    public MemoryGameLogic thelogic;

    public Vector3 origScale;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isAvaliable = true;
        if (thelogic == null)
        {
            thelogic = FindAnyObjectByType<MemoryGameLogic>();
        }

        if (transform.childCount > 0)
        {
            image = transform.GetChild(0).GetComponent<SpriteRenderer>();
            origScale = image.transform.localScale;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (isAvaliable)
        {
            thelogic.BoxClicked(this.gameObject);
            isAvaliable = !isAvaliable;
        }
        
    }

    public void ResetAvaliability()
    {
        isAvaliable = true;
    }
    
}
