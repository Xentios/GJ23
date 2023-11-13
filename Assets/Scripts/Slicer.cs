using EzySlice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using DG.Tweening;
using System.Reflection;
using UnityEngine.Events;

public class Slicer : MonoBehaviour
{
    [SerializeField]
    private GameEvent jobFinished;

    [SerializeField]
    private InputActionReference mouseMovement;
    [SerializeField]
    private InputActionReference mouseLeftClick;
    [SerializeField]
    private InputActionReference mouseRightClick;
    [SerializeField]
    private InputActionReference mouseScrollWithCTRL;
    [SerializeField]
    private InputActionReference mouseScroll;

    [SerializeField]
    private GameObject sliceTarget;
    [SerializeField]
    private Material slicedFaceMaterial;

    [SerializeField]
    private Transform sliceHolder;

    private int indexOfSliceHolder;

    [SerializeField]
    private List<GameObject> sliceHolderPrefabs;

    [SerializeField]
    private LayerMask sliceableLayer;

    private List<EzySlice.Plane> planeList;


    private GameObject lastUpperPart;
    private GameObject lastLowerPart;


    private Ray DebugRay;
    private bool isSlicing;

    private void Awake()
    {
        planeList = new();       
    }

    private void OnEnable()
    {
        mouseScroll.action.performed += MouseScrollEvent;
        mouseMovement.action.performed += MouseMovementEvent;
        mouseLeftClick.action.performed += MouseLeftClickEvent;
        mouseRightClick.action.performed += MouseRightClickEvent;
        mouseScrollWithCTRL.action.performed += MouseScrollWithCTRLEvent;

        mouseScroll.action.Enable();
        mouseMovement.action.Enable();
        mouseLeftClick.action.Enable();
        mouseRightClick.action.Enable();
        mouseScrollWithCTRL.action.Enable();
    }
    void OnDisable()
    {
        mouseScroll.action.performed -= MouseScrollEvent;
        mouseMovement.action.performed -= MouseMovementEvent;
        mouseLeftClick.action.performed -= MouseLeftClickEvent;
        mouseRightClick.action.performed -= MouseRightClickEvent;
        mouseScrollWithCTRL.action.performed -= MouseScrollWithCTRLEvent;

        mouseScroll.action.Disable();
        mouseMovement.action.Disable();
        mouseLeftClick.action.Disable();
        mouseRightClick.action.Disable();
        mouseScrollWithCTRL.action.Disable();
    }

    private void SetPlanes()
    {
        planeList.Clear();
        for (int i = 0; i < sliceHolder.childCount; i++)
        {
            var sliceTargetMeshFilter = sliceTarget.GetComponent<MeshFilter>();          
            var bounds = sliceTargetMeshFilter.sharedMesh.bounds;
            var distance = DistanceToPlane(sliceTargetMeshFilter.transform.TransformPoint(bounds.center), sliceHolder.GetChild(i).transform.right, sliceHolder.GetChild(i).transform.position);
            Debugger.Log("Distance is " + distance, Debugger.PriorityLevel.Medium);
            distance /= sliceTargetMeshFilter.transform.lossyScale.x;            
            planeList.Add(new EzySlice.Plane(sliceHolder.GetChild(i).transform.right, -distance));
        }
    }

    public void Slice()
    {
        Slice(GetRandomPlane());
    }
    public void Slice(EzySlice.Plane thePlane)
    {
        var tr = new TextureRegion(0f, 0f, 1.0f, 1.0f);        
        EzySlice.SlicedHull result = EzySlice.Slicer.Slice(sliceTarget, thePlane, tr, slicedFaceMaterial);
        //SliceInstantiate(this GameObject obj, Plane pl, TextureRegion cuttingRegion, Material crossSectionMaterial = null)
        //EzySlice.SlicerExtensions.SliceInstantiate(sliceTarget, thePlane,new TextureRegion());
        if (result != null)
        {
           
            var lowerHull = result.CreateLowerHull(sliceTarget, slicedFaceMaterial);
            var upperHull = result.CreateUpperHull(sliceTarget, slicedFaceMaterial);

            lowerHull.transform.position = sliceTarget.transform.position;
            upperHull.transform.position = sliceTarget.transform.position;
            Destroy(lastLowerPart);
            //Destroy(lastUpperPart);
            lastLowerPart = lowerHull;
            lastUpperPart = upperHull;
            var rb = lastUpperPart.AddComponent<Rigidbody>();
            rb.AddForce(Random.insideUnitSphere * 100);
            lastUpperPart.AddComponent<MeshCollider>().convex=true;
            Destroy(sliceTarget);
            sliceTarget = lastLowerPart;
          
        }

    }

    public void Slice(EzySlice.Plane thePlane, TextureRegion theTr)
    {
        EzySlice.Slicer.Slice(sliceTarget, thePlane, theTr, slicedFaceMaterial)?.CreateLowerHull(sliceTarget, slicedFaceMaterial);
    }

    public EzySlice.Plane GetRandomPlane()
    {
        Vector3 randomPosition = Random.insideUnitSphere / 2;

        //randomPosition += positionOffset;

        Vector3 randomDirection = Random.insideUnitSphere.normalized;
        return new EzySlice.Plane(randomPosition, randomDirection);
    }


   
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        for (int i = 0; i < sliceHolder.childCount; i++)
        {
            Gizmos.DrawLine(sliceHolder.GetChild(i).transform.position, sliceHolder.GetChild(i).transform.position + sliceHolder.GetChild(i).transform.right);
        }
        

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(DebugRay.origin, DebugRay.direction * 100f);
    }


    private void MouseLeftClickEvent(InputAction.CallbackContext callback)
    {
        if (isSlicing == true) return;

        isSlicing = true;
        sliceHolder.GetComponent<Shaker>().StopShaking();
       // Cursor.visible = false;
        Vector2 cursorPosition = Mouse.current.position.ReadValue();
        Cursor.lockState = CursorLockMode.Locked;

        //sliceTarget.GetComponent<QuickOutline.Outline>().enabled=false;
        SetPlanes();
        transform.DOMoveY(0f, 0.5f).SetEase(Ease.InCubic).OnComplete(() =>
        transform.DOMoveY(1, 0.5f).SetEase(Ease.InFlash).OnComplete(() =>
        {
            isSlicing = false;
            //Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Mouse.current.WarpCursorPosition(cursorPosition);
            sliceHolder.GetComponent<Shaker>().StartShaking();
            jobFinished.TriggerEvent(sliceTarget);
        }

        )); ;


        foreach (var plane in planeList)
        {
            Slice(plane);
        }
      
        



    }

    


    private void MouseRightClickEvent(InputAction.CallbackContext callback)
    {
        if (isSlicing == true) return;

        indexOfSliceHolder++;
        indexOfSliceHolder %= sliceHolderPrefabs.Count;
        GameObject.Destroy(sliceHolder.gameObject);
        sliceHolder=Instantiate(sliceHolderPrefabs[indexOfSliceHolder],transform).transform;        
    }

    private void MouseMovementEvent(InputAction.CallbackContext callback)
    {
        if (isSlicing == true) return;

        var value = callback.ReadValue<Vector2>();
        var distanceOfSlicer = Vector3.Distance(transform.position, Camera.main.transform.position);
        Vector3 mousePosition = new Vector3(value.x, value.y, distanceOfSlicer);
        var mousePos = Camera.main.ScreenToWorldPoint(mousePosition);
        mousePos.y = transform.position.y;
        transform.position = mousePos;
    }

    private void MouseScrollEvent(InputAction.CallbackContext callback)
    {
        if (isSlicing == true) return;
        if (Keyboard.current.ctrlKey.isPressed || Keyboard.current.shiftKey.isPressed || Keyboard.current.altKey.isPressed) return;

        var result = callback.ReadValue<float>();
        Debugger.Log("MouseScrollEvents result= " + result, Debugger.PriorityLevel.MustShown);
        var speed = 10f;            
        Quaternion rotation = Quaternion.Euler(0, Mathf.Sign(result)*speed, 0);
        sliceHolder.transform.rotation *= rotation;
    }

    private void MouseScrollWithCTRLEvent(InputAction.CallbackContext callback)
    {
        if (isSlicing == true) return;

        var result = callback.ReadValue<float>();
        Debugger.Log("MouseScrollwithCTRLEvent result= " + result, Debugger.PriorityLevel.MustShown);
        var speed = 0.1f;
        result =1+(Mathf.Sign(result) * speed);
        sliceHolder.localScale = sliceHolder.localScale * result;
    }

    private float DistanceToPlane(Vector3 point, Vector3 planeNormal, Vector3 pointOnPlane)
    {
        point.y = 0;
        pointOnPlane.y = 0;
        return Vector3.Dot(point - pointOnPlane, planeNormal);
    }
    private float DistanceIgnoreY(Vector3 a, Vector3 b)
    {
        Vector2 a2D = new Vector2(a.x, a.z);
        Vector2 b2D = new Vector2(b.x, b.z);
        return Vector2.Distance(a2D, b2D);
    }


    private float MapF(float OldMin, float OldMax, float NewMin, float NewMax, float OldValue)
    {

        float OldRange = (OldMax - OldMin);
        float NewRange = (NewMax - NewMin);
        float NewValue = (((OldValue - OldMin) * NewRange) / OldRange) + NewMin;

        return (NewValue);
    }




}
