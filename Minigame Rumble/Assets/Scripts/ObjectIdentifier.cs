using UnityEngine;

public class ObjectIdentifier : MonoBehaviour
{
    [SerializeField] private GameObject thingtospawn;
    private Vector3 originalPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalPosition = new Vector3(0, 0, 0);
        if (thingtospawn == null)
        {
            Debug.LogError("No object was found");
        }
        else
        {
            switch (thingtospawn.name)
            {
                case "Box":
                    Debug.Log("This is a box");
                    Instantiate(thingtospawn, originalPosition, Quaternion.identity);
                    break;
                case "Circle":
                    Debug.Log("This is a circle");
                    Instantiate(thingtospawn, originalPosition, Quaternion.identity);
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
