using Es.InkPainter;
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
    private ColorCheker colorCheker;
    [SerializeField]
    private Brush brush;

    [SerializeField]
    private UseMethodType useMethodType = UseMethodType.RaycastHitInfo;

    [SerializeField]
    bool erase = false;

    [SerializeField]
    private double timeLeftToPaint;
    private double  timeStartedToPaint;

    private bool isPainting = false;
    private void OnEnable()
    {
        
        mouseMovement.action.performed += MouseMovementEvent;
        mouseLeftClick.action.started += MouseLeftDownEvent;
        mouseLeftClick.action.canceled += MouseLeftUpEvent;
        mouseRightClick.action.started += MouseRightDownEvent;
        mouseRightClick.action.canceled += MouseRightUpEvent;



        mouseMovement.action.Enable();
        mouseLeftClick.action.Enable();
        mouseRightClick.action.Enable();
        
    }
    void OnDisable()
    {
        
        mouseMovement.action.performed -= MouseMovementEvent;
        mouseLeftClick.action.started -= MouseLeftDownEvent;
        mouseLeftClick.action.canceled -= MouseLeftUpEvent;
        mouseRightClick.action.started -= MouseRightDownEvent;
        mouseRightClick.action.canceled -= MouseRightUpEvent;



        mouseMovement.action.Disable();
        mouseLeftClick.action.Disable();
        mouseRightClick.action.Disable();        
    }

    private void MouseRightDownEvent(InputAction.CallbackContext obj)
    {
        erase = true;
        StartPainting(obj.startTime);
    }

    private void MouseRightUpEvent(InputAction.CallbackContext obj)
    {
        erase = false;
        StopPainting(obj.time);      
    }

    private void MouseLeftDownEvent(InputAction.CallbackContext obj)
    {       
        StartPainting(obj.startTime);
    }

    private void MouseLeftUpEvent(InputAction.CallbackContext obj)
    {       
        StopPainting(obj.time);
    }

    private void StartPainting(double time)
    {
        isPainting = true;
        timeStartedToPaint = time;
    }

    private void StopPainting(double time)
    {
        isPainting = false;       
        timeLeftToPaint -= time-timeStartedToPaint;
    }

    private void MouseMovementEvent(InputAction.CallbackContext callback)
    {
        //Debug.Log("when NOT painting " +( callback.startTime-timeStartedToPaint));
        var mousePosition = callback.ReadValue<Vector2>();
        var distanceOfSlicer = Vector3.Distance(transform.position, Camera.main.transform.position);
        Vector3 SprayCanPosition = new Vector3(mousePosition.x, mousePosition.y, distanceOfSlicer);
        var mousePos = Camera.main.ScreenToWorldPoint(SprayCanPosition);
        mousePos.y = transform.position.y;
        transform.position = mousePos;

        if (isPainting == false) return;


        

        var usedTime = callback.startTime - timeStartedToPaint;     
        if (usedTime > timeLeftToPaint)
        {
            jobFinished.TriggerEvent();
        }


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

    public void CheckColors(MeshRenderer meshRenderer)
    {
        colorCheker.CalculateColorArea(meshRenderer, brush.Color);
    }
   
}
