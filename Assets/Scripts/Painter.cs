using Es.InkPainter;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Painter : MonoBehaviour
{
    private enum UseMethodType
    {
        RaycastHitInfo,
        WorldPoint,
        NearestSurfacePoint,
        DirectUV,
    }

    [SerializeField]
    private GameEvent jobFinished;

    [SerializeField]
    private InputActionReference mouseMovement;
    [SerializeField]
    private InputActionReference mouseLeftClick;
    [SerializeField]
    private InputActionReference mouseRightClick;


    [SerializeField]
    private Brush brush;

    [SerializeField]
    private UseMethodType useMethodType = UseMethodType.RaycastHitInfo;

    [SerializeField]
    bool erase = false;

    private bool isPainting = false;
    private void OnEnable()
    {
        
        mouseMovement.action.performed += MouseMovementEvent;
        mouseLeftClick.action.performed += MouseLeftClickEvent;
        mouseRightClick.action.started += MouseRightDownEvent;
        mouseRightClick.action.canceled += MouseRightUpEvent;



        mouseMovement.action.Enable();
        mouseLeftClick.action.Enable();
        mouseRightClick.action.Enable();
        
    }
    void OnDisable()
    {
        
        mouseMovement.action.performed -= MouseMovementEvent;
        mouseLeftClick.action.performed -= MouseLeftClickEvent;
        mouseRightClick.action.started -= MouseRightDownEvent;
        mouseRightClick.action.canceled -= MouseRightUpEvent;



        mouseMovement.action.Disable();
        mouseLeftClick.action.Disable();
        mouseRightClick.action.Disable();        
    }

    private void MouseRightDownEvent(InputAction.CallbackContext obj)
    {
        erase = true;
        isPainting = true;
    }

    private void MouseRightUpEvent(InputAction.CallbackContext obj)
    {
        erase = false;
        isPainting = false;
    }

    private void MouseLeftClickEvent(InputAction.CallbackContext obj)
    {
        isPainting = true;
    }

    private void MouseMovementEvent(InputAction.CallbackContext callback)
    {
        if (isPainting == false) return;
        var mousePosition=callback.ReadValue<Vector2>();

        var ray = Camera.main.ScreenPointToRay(mousePosition);
        bool success = true;
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            var paintObject = hitInfo.transform.GetComponent<InkCanvas>();
            if (paintObject != null)
                switch (useMethodType)
                {
                    case UseMethodType.RaycastHitInfo:
                    success = erase ? paintObject.Erase(brush, hitInfo) : paintObject.Paint(brush, hitInfo);
                    break;

                    case UseMethodType.WorldPoint:
                    success = erase ? paintObject.Erase(brush, hitInfo.point) : paintObject.Paint(brush, hitInfo.point);
                    break;

                    case UseMethodType.NearestSurfacePoint:
                    success = erase ? paintObject.EraseNearestTriangleSurface(brush, hitInfo.point) : paintObject.PaintNearestTriangleSurface(brush, hitInfo.point);
                    break;

                    case UseMethodType.DirectUV:
                    if (!(hitInfo.collider is MeshCollider))
                        Debug.LogWarning("Raycast may be unexpected if you do not use MeshCollider.");
                    success = erase ? paintObject.EraseUVDirect(brush, hitInfo.textureCoord) : paintObject.PaintUVDirect(brush, hitInfo.textureCoord);
                    break;
                }
            if (!success)
                Debug.LogError("Failed to paint.");
        }
    }

   
}
