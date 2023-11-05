using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable3DObject : MonoBehaviour, IDragHandler, IDropHandler,IEndDragHandler,IBeginDragHandler
{

    [SerializeField]
    private float max_distance_raycast;
    [SerializeField]
    private LayerMask layerMask;

    private Camera UICamera;

    private Vector3 lastPosition;


    private Rigidbody rigidbody;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
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
            var lookAt = Vector3.Cross(hit.normal, transform.up);
            var slot = hit.point;

            lookAt = lookAt.y < 0 ? -lookAt : lookAt;
            
           
            //transform.rotation =Quaternion.Euler(Vector3.Cross(hit.normal, transform.right) );
            transform.rotation= Quaternion.LookRotation(hit.point+lookAt, hit.normal);
            transform.position = hit.point;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        transform.position = lastPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        lastPosition = transform.position;
        rigidbody.isKinematic = true;
    }
}
