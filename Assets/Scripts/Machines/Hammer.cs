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

    [SerializeField]
    private GameObject hammerVisual;
    [SerializeField]
    private LayerMask hammerableMask;
    [SerializeField]
    private GameObject hammerHead;

    [SerializeField]
    private AnimationCurve animationCurve;

    private Plane visualPlane;

    private GameObject spike;
    private Bounds colliderBounds;

    private MeshRenderer hammerHeadMesh;

    private List<GameObject> hammeredSpikes;
    private void OnEnable()
    {
        hammeredSpikes = new List<GameObject>();

        for (int i = 0; i < 90; i++)
        {
            var resultAngle = animationCurve.Evaluate(i);
            Debugger.Log(i + " ---Animated Angle Test---> " + resultAngle, Debugger.PriorityLevel.LeastImportant);
        }

        hammerHeadMesh = hammerHead.GetComponent<MeshRenderer>();
        var targetObject = GameManager.Instance.targetObject.transform;

        Vector3 topPosition = targetObject.position+Vector3.Scale(Vector3.up, targetObject.lossyScale)*targetObject.GetComponent<MeshFilter>().sharedMesh.bounds.extents.y;
        visualPlane = new Plane(Vector3.up, topPosition + Vector3.up);
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
        if (spike == null) return;
       
        var size = colliderBounds.extents.y;      
        var center = hammerHead.GetComponent<MeshRenderer>().bounds.center;
        var angle = AngleBetweenTwoPoints(colliderBounds.center,center);
        Debugger.Log("Hammer Strike angle "+angle, Debugger.PriorityLevel.MustShown);
        Debugger.Log("colliderBounds.center  " + colliderBounds.center, Debugger.PriorityLevel.MustShown);
        Debugger.Log("hammerHead.GetComponent<MeshRenderer>().bounds.center " + center, Debugger.PriorityLevel.MustShown);       
        spike.transform.parent.transform.position += Vector3.down*(size);
        var resultAngle=animationCurve.Evaluate(Mathf.Abs(90 - angle));
        Debugger.Log("Animated Angle " + resultAngle, Debugger.PriorityLevel.MustShown);
        spike.transform.Rotate(Vector3.forward, resultAngle*Mathf.Sign(90- angle));       
        spike.layer = 0;
        hammeredSpikes.Add(spike);
        CalculateSpikeScore();
        DisableSpikeOutline();

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
        var extends= hammerHeadMesh.bounds.extents;
        var center= hammerHeadMesh.bounds.center;
        if(Physics.BoxCast(center, extends, Vector3.down, out RaycastHit hitinfo, Quaternion.identity, 1000f, hammerableMask))
        {
           
            var hitObject = hitinfo.transform.gameObject;
            hammerVisual.transform.localRotation = new Quaternion(0, 0.142496109f, 1.49011594e-08f, 0.989795446f);

            DisableSpikeOutline();
            spike = hitObject;
            EnableSpikeOutline();
            colliderBounds = hitinfo.collider.bounds;
        }
        else
        {
            DisableSpikeOutline();
            hammerVisual.transform.localRotation = new Quaternion(0, 0, 0, 1);
        }
    }

    private void EnableSpikeOutline()
    {
        var outline = spike.GetComponent<QuickOutline.Outline>();

        outline.enabled = true;
        outline.OutlineMode = QuickOutline.Outline.Mode.OutlineVisible;
        outline.OutlineWidth = 3f;
    }

    private void DisableSpikeOutline()
    {
        if (spike == null) return;

        spike.GetComponent<QuickOutline.Outline>().enabled = false;
        spike = null;
    }

    public float AngleBetweenPlaneAndPoint(Vector3 planeNormal, Vector3 pointOnPlane, Vector3 targetPoint)
    {   
        Vector3 toTarget = targetPoint - pointOnPlane;        
        float angle = Vector3.Angle(planeNormal, toTarget);
        return angle;
    }
    public float AngleBetweenTwoPoints(Vector3 point1, Vector3 point2)
    {
        Vector3 direction = point2 - point1;
        float angle = Vector3.Angle(Vector3.right, direction);
        float crossProduct = Vector3.Cross(Vector3.right, direction).z;
        if (crossProduct < 0)
        {
            angle = 360f - angle;
        }
        return angle;
    }

    public void RotateTowardsWithAngle(Transform objectToRotate, Vector3 startPoint, Vector3 targetPoint, float rotationAngle)
    {
        Vector3 direction = targetPoint - startPoint;
        Quaternion rotation = Quaternion.LookRotation(direction.normalized) * Quaternion.Euler(0, rotationAngle, 0);
        objectToRotate.localRotation = rotation;
    }

    //Returns a score between 0 and 1, 1 is perfect score
    public float  CalculateSpikeScore()
    {
        float average=0;
        foreach (var spike in hammeredSpikes)
        {
            var current= Vector3.Dot(spike.transform.up, Vector3.up);
            Debugger.Log(current, Debugger.PriorityLevel.High);
            average += current;
        }
        average /= hammeredSpikes.Count;

        return average;
    }

}
