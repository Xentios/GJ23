using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ResourceCollector : MonoBehaviour
{

    [SerializeField]
    public CollectedResources CollectedResources;

    [SerializeField]
    private InputActionReference mouseMovement;

    [SerializeField]
    private LayerMask layerMask;

    private Plane visualPlane;
    private void OnEnable()
    {
        mouseMovement.action.performed += MouseMovement;
    }

    private void OnDisable()
    {
        mouseMovement.action.performed -= MouseMovement;
    }

    private void Start()
    {
        visualPlane = new Plane(Vector3.back, Vector3.zero);
    }

    private void MouseMovement(InputAction.CallbackContext callbackContext)
    {
      
        var mousePosition = callbackContext.ReadValue<Vector2>();
        Debugger.Log("Mouse Movement " + mousePosition, Debugger.PriorityLevel.LeastImportant);
        var ray = Camera.main.ScreenPointToRay(mousePosition);


        var modelPos = Vector3.zero;
        if (visualPlane.Raycast(ray, out float distance))
        {
            modelPos = ray.GetPoint(distance);
        }
        transform.position = modelPos;


        Collider[] results = new Collider[100];
        var count = Physics.OverlapSphereNonAlloc(modelPos, 2.2f, results, layerMask);
        for (int i = count-1; i >=0; i--)
        {
            Destroy(results[i].gameObject);
           
        }
       
    }
}
