using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoxHandler : MonoBehaviour
{
    [SerializeField] private GameObject box;
    private bool isAvaliable;
    private List<GameObject> chosenBoxes;
    public MemoryGameLogic thelogic;
    private SpriteRenderer _cachedImage;
    private Transform _childTransform;
    public Vector3 origScale;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Cache the image component if it exists
        if (transform.childCount > 0)
        {
            _cachedImage = transform.GetChild(0).GetComponent<SpriteRenderer>();
            _childTransform = transform.GetChild(0);
            origScale = _cachedImage.transform.localScale;
        }
        isAvaliable = true;
        if (thelogic == null)
        {
            thelogic = FindAnyObjectByType<MemoryGameLogic>();
        }
    }
    
    //Public getter to return the cached sprite renderer
    public SpriteRenderer GetImageRenderer() => _cachedImage;
    
    //Public getter to return the child transform
    public Transform GetChildTransform() => _childTransform;
    public void OnMouseDown()
    {
        if (isAvaliable)
        {
            //A box has been clicked
            thelogic.BoxClicked(this.gameObject);
            isAvaliable = !isAvaliable;
        }
    }
    public void ResetAvaliability()
    {
        isAvaliable = true;
    }
    
}
