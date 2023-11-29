using Es.InkPainter;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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
    private GameEvent paintAmountChanged;

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
    private GameObject sprayFX;

    [SerializeField]
    private UseMethodType useMethodType = UseMethodType.RaycastHitInfo;

    [SerializeField]
    bool erase = false;

    [SerializeField]
    private double setTimeLeftToPaint;

    [SerializeField]
    private FloatVariable timeLeftToPaintSO;

    private double timeLeftToPaint;
    private double  timeStartedToPaint;

    private bool isPainting;

    private Plane visualPlane;

    private AudioSource spraySound;
    private void Awake()
    {
        spraySound = GetComponent<AudioSource>();
        visualPlane  = new Plane(Vector3.up, Vector3.zero);
    }

  
    private void OnEnable()
    {
        timeLeftToPaint = setTimeLeftToPaint;
        timeStartedToPaint = 0;
        isPainting = false;
        timeLeftToPaintSO.Value = 1f;
        spraySound.volume = 1;
        spraySound.pitch = 1;

        var topPosition=GameManager.Instance.GetTopPlaneOfTarget();
        visualPlane= new Plane(Vector3.up, topPosition+Vector3.up);

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



        //mouseMovement.action.Disable();
        //mouseLeftClick.action.Disable();
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
        sprayFX.SetActive(true);
        spraySound.Play();
        isPainting = true;
        timeStartedToPaint = time;
    }

    private void StopPainting(double time)
    {
        sprayFX.SetActive(false);
        spraySound.Stop();       
        isPainting = false;       
        timeLeftToPaint -= time-timeStartedToPaint;
        spraySound.volume =(float) ((timeLeftToPaint*2) / (setTimeLeftToPaint));
    }

    private void MouseMovementEvent(InputAction.CallbackContext callback)
    {        
        var mousePosition = callback.ReadValue<Vector2>();
        var ray = Camera.main.ScreenPointToRay(mousePosition);

      
        var modelPos = Vector3.zero;
        if ( visualPlane.Raycast(ray,out float distance))
        {
            modelPos = ray.GetPoint(distance);
        }
        transform.position = modelPos;

        if (isPainting == false) return;


        

        var usedTime = callback.startTime - timeStartedToPaint;     
        if (usedTime > timeLeftToPaint)
        {
            jobFinished.TriggerEvent();
        }
        else
        {
            timeLeftToPaintSO.Value =(float)  (( timeLeftToPaint- usedTime )/setTimeLeftToPaint);
            spraySound.volume = Mathf.Max(0.3f, timeLeftToPaintSO.Value);
            paintAmountChanged.TriggerEvent();
        }


       
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

    public float CheckColors(MeshRenderer meshRenderer,Color requestedColor)
    {
        return colorCheker.CalculateColorArea(meshRenderer, requestedColor);
    }

    public int[] CheckALLColors(MeshRenderer meshRenderer)
    {
        return colorCheker.CalculateAllColors(meshRenderer);
    }

    public void ChangeColorOfBrush(Image image)
    {
        brush.Color = image.color;
        var results=sprayFX.GetComponentsInChildren<ParticleSystem>();
        

        
        foreach (var vfx in results)
        {
            ParticleSystem.MainModule settings = vfx.main;
            settings.startColor = image.color;
            
            //vfx.gets.GetSetData<Color>("colorOverLifetime", color);
        }
    }
      
       // sprayFX.SetActive(true);
    
    public void ChangeSizeOfBrush(float value)
    {
        brush.Scale = value;
    }


}
