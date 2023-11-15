
using UnityEngine;
using UnityEngine.EventSystems;

using DG.Tweening;

public class Draggable3DObjectNewInput : MonoBehaviour,IPointerExitHandler,IPointerEnterHandler, IDragHandler,IEndDragHandler,IBeginDragHandler
{

    [SerializeField]
    private float max_distance_raycast;
    [SerializeField]
    private LayerMask layerMask;

    private Camera UICamera;

    private Vector3 lastPosition;

    private bool isPlaced;

    private Rigidbody rigidbodyy;
    private QuickOutline.Outline outline;
    private Vector3 objectHeight;

    private bool isReallyPlaced;
    private void Awake()
    {
        rigidbodyy = GetComponent<Rigidbody>();
        outline = GetComponent<QuickOutline.Outline>();
        objectHeight = GetComponent<MeshFilter>().mesh.bounds.extents;
        objectHeight *= transform.lossyScale.y;
    }
    private void Start()
    {
        UICamera = GameObject.FindGameObjectWithTag("UICamera").GetComponent<Camera>();       
    }
    // Start is called before the first frame update
    public void OnDrag(PointerEventData eventData)
    {
        Debugger.Log("UI element drag position on Start: " + transform.position, Debugger.PriorityLevel.LeastImportant);

        if (isPlaced == false && isReallyPlaced ==false)
        {
            var mousePos = UICamera.ScreenToWorldPoint(eventData.position);
            mousePos.z = transform.position.z;
            transform.position = mousePos;
        }
       

        Ray ray = Camera.main.ScreenPointToRay(eventData.position);
        if (Physics.Raycast(ray, out RaycastHit hit, max_distance_raycast, layerMask, QueryTriggerInteraction.Collide))
        {
            //Why this was wrong Cross order?
            //var lookAt = Vector3.Cross(hit.normal, transform.up);
            //var slot = hit.point;
            //lookAt = lookAt.y < 0 ? -lookAt : lookAt;
            ////transform.rotation =Quaternion.Euler(Vector3.Cross(hit.normal, transform.right) );
            //transform.rotation= Quaternion.LookRotation(hit.point+lookAt, hit.normal);

            Vector3 newUp = hit.normal;
            Vector3 left = Vector3.Cross(transform.forward, newUp); // The cross of this.transform.forward and newUp will give you the the direction that defines your left (its the left because unity uses a left-handed system)
            Vector3 newForward = Vector3.Cross(newUp, left); // Take the cross of newUp and the newely found left and you will get your newForward for your character           
            Quaternion newRotation = Quaternion.LookRotation(newForward, newUp);
            if (isReallyPlaced==false)
            {
                // transform.SetPositionAndRotation(hit.point, newRotation);
                MoveTransform(hit.point);
                transform.rotation = newRotation;
            }
           
            //transform.Translate(transform.up * objectHeight.y);
            isPlaced = true;
        }
        else
        {
            isPlaced = false;
        }

        Debugger.Log("On Drag End position of object " + transform.position, Debugger.PriorityLevel.LeastImportant);
    }

 

    public void OnEndDrag(PointerEventData eventData)
    {
        
        Debugger.Log("On End Drag of " + transform.name, Debugger.PriorityLevel.High);
        Debugger.Log("isPlaced is  " + isPlaced, Debugger.PriorityLevel.High);
        // transform.SetPositionAndRotation(transform.position + transform.up * objectHeight.y, transform.rotation);
        //transform.position+transform.up * objectHeight.y;
       // MoveTransform(transform.position);
        isReallyPlaced = true;
        outline.enabled = false;
        gameObject.layer = LayerMask.NameToLayer("Hammerable");
        if (isPlaced == true) return;

        //transform.position = lastPosition;
      
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        lastPosition = transform.position;
        rigidbodyy.isKinematic = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        outline.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        outline.enabled = false;
    }

    private void MoveTransform(Vector3 position)
    {
        if (isReallyPlaced == true) return;        
        transform.position = position+transform.up * objectHeight.y;
    }
}
