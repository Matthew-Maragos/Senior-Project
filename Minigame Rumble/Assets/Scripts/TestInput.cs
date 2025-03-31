using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class TestInput : MonoBehaviour
{
    public EventSystem eventSystem;
    void Update()
    {
        if (Gamepad.current != null && Gamepad.current.buttonSouth.wasPressedThisFrame)
        {
            Debug.Log("Forcing UI Submit!");
            GameObject selectedObj = eventSystem.currentSelectedGameObject;

            if (selectedObj != null)
            {
                ExecuteEvents.Execute(selectedObj, new BaseEventData(eventSystem), ExecuteEvents.submitHandler);
            }
            else
            {
                Debug.LogWarning("No UI element is selected!");
            }
        }
    }
}