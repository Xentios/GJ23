using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Hammer : MonoBehaviour
{
    [SerializeField]
    private GameEvent jobFinished;

    [SerializeField]
    private InputActionReference mouseMovement;
    [SerializeField]
    private InputActionReference mouseLeftClick;


    private Plane visualPlane;
    private void OnEnable()
    {
        visualPlane = new Plane(Vector3.up, Vector3.up);//TODO fix placement
        mouseMovement.action.performed += MouseMovementEvent;
        mouseLeftClick.action.started += MouseLeftClickEvent;     

        mouseMovement.action.Enable();
        mouseLeftClick.action.Enable();
    }

    
    void OnDisable()
    {
        mouseMovement.action.performed -= MouseMovementEvent;
        mouseLeftClick.action.started -= MouseLeftClickEvent;
      
        mouseMovement.action.Disable();
        mouseLeftClick.action.Disable();       
    }

    private void MouseLeftClickEvent(InputAction.CallbackContext obj)
    {
        Debugger.Log("Hammer Strike", Debugger.PriorityLevel.High);
    }

    private void MouseMovementEvent(InputAction.CallbackContext callback)
    {
        var mousePosition = callback.ReadValue<Vector2>();
        var ray = Camera.main.ScreenPointToRay(mousePosition);


        var modelPos = Vector3.zero;
        if (visualPlane.Raycast(ray, out float distance))
        {
            modelPos = ray.GetPoint(distance);
        }
        transform.position = modelPos;
    }

}
