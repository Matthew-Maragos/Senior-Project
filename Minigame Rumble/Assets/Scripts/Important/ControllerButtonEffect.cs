using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ControllerButtonEffect : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public GameObject highlightImage, highlightImage2; // The images to show when button is selected
    
    public void OnSelect(BaseEventData eventData)
    {
        if (highlightImage != null) // Show images when button is selected
            highlightImage.SetActive(true); 
            highlightImage2.SetActive(true); 
    }

    public void OnDeselect(BaseEventData eventData)
    {
        if (highlightImage != null) // Hide image when button is deselected
            highlightImage.SetActive(false);
            highlightImage2.SetActive(false);
    }
}


