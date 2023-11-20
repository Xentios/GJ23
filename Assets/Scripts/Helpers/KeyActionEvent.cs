using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class KeyActionEvent : MonoBehaviour
{
    [SerializeField] InputActionReference keyPress;
    [SerializeField] UnityEvent UnityEvent;


    private void OnEnable()
    {
        keyPress.action.performed+=TriggerEvent;
    }

    private void OnDisable()
    {
        keyPress.action.performed -= TriggerEvent;
    }



    private void TriggerEvent(InputAction.CallbackContext callbackContext)
    {
        UnityEvent?.Invoke();
    }
}
