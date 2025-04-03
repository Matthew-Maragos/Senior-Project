using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject hoverImage1, hoverImage2;

    public void OnPointerEnter(PointerEventData eventData)
    {
        hoverImage1.SetActive(true);
        hoverImage2.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hoverImage1.SetActive(false);
        hoverImage2.SetActive(false);
    }
}