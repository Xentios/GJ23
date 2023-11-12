using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable3DObject : MonoBehaviour,IPointerExitHandler,IPointerEnterHandler, IDragHandler, IDropHandler,IEndDragHandler,IBeginDragHandler
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
    private void Awake()
    {
        rigidbodyy = GetComponent<Rigidbody>();
        outline = GetComponent<QuickOutline.Outline>();
        objectHeight = GetComponent<MeshFilter>().mesh.bounds.extents;
    }
    private void Start()
    {
        UICamera = GameObject.FindGameObjectWithTag("UICamera").GetComponent<Camera>();       
    }
    // Start is called before the first frame update
    public void OnDrag(PointerEventData eventData)
    {
        Debugger.Log("UI element drag position: " + transform.position, Debugger.PriorityLevel.LeastImportant);
        var mousePos = UICamera.ScreenToWorldPoint(eventData.position);
        mousePos.z = transform.position.z;
        transform.position = mousePos;

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
            Quaternion oldRotation = transform.rotation; // Get the current rotation  (im calling oldRotation here because that is what it will be in a second)
            Quaternion newRotation = Quaternion.LookRotation(newForward, newUp);
            transform.rotation = newRotation;
            transform.position = hit.point+ objectHeight;
            isPlaced = true;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (isPlaced==true) return;

        transform.position = lastPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        outline.enabled = false;
        if (isPlaced == true) return;

        transform.position = lastPosition;
      
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
}
