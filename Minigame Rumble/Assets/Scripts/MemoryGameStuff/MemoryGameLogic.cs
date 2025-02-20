using UnityEngine;

public class MemoryGameLogic : MonoBehaviour
{
    public BoxHandler theboxes;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        theboxes = GetComponent<BoxHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckForMatch()
    {
        
    }
}
