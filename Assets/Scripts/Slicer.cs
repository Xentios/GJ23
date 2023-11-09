using EzySlice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using DG.Tweening;
using System.Reflection;

public class Slicer : MonoBehaviour, IPointerClickHandler
{
    // Start is called before the first frame update

    [SerializeField]
    private float  testFloat;

    [SerializeField]
    private InputAction mouseScroll;

    [SerializeField]
    private GameObject sliceTarget; 
    [SerializeField]
    private Material slicedFaceMaterial;

    [SerializeField]
    private LayerMask  sliceableLayer;

    private List<EzySlice.Plane> planeList;


    private EzySlice.Plane debugPlane;

    private GameObject lastUpperPart;
    private GameObject lastLowerPart;


    private Ray DebugRay;
    private bool isSlicing;

    private void Awake()
    {
        planeList = new();
      
    }

    private void Start()
    {
        mouseScroll.performed += MouseScrollEvents;
        //debugPlane = //new EzySlice.Plane();       
        //debugPlane.Compute();
      //  debugPlane.Compute(planeList[0].dist, planeList[0].normal);
    }

    private void OnEnable()
    {
        mouseScroll.Enable();
    }
    void OnDisable()
    {
        mouseScroll.Disable();
    }

    private void Update()
    {

        var distanceOfSlicer = Vector3.Distance(transform.position, Camera.main.transform.position);
        var mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Mouse.current.position.x.value, Mouse.current.position.y.value, distanceOfSlicer));
        mousePos.y = transform.position.y;
        transform.position = mousePos;


        if(isSlicing == false)
        {
            //Ray rayFirst = Camera.main.ScreenPointToRay(new Vector3(Mouse.current.position.x.value, Mouse.current.position.y.value, distanceOfSlicer));
            //DebugRay = rayFirst;
            //if (Physics.Raycast(rayFirst, out RaycastHit hitF, 1000f, sliceableLayer))
            //{
            //    hitF.transform.GetComponent<QuickOutline.Outline>().enabled = true;
            //    sliceTarget = hitF.transform.gameObject;
            //}
            //else if ((sliceTarget != null))
            //{
            //    sliceTarget.GetComponent<QuickOutline.Outline>().enabled = false;
            //    sliceTarget = null;
            //}

        }
      


            if (Mouse.current.leftButton.wasPressedThisFrame && sliceTarget!=null)
        {


            isSlicing = true;
              sliceTarget.GetComponent<QuickOutline.Outline>().enabled=false;
            //transform.DOMoveY(-0.5f, 0.5f).SetEase(Ease.InCubic).OnComplete(() =>
            //transform.DOMoveY(0, 0.5f).SetEase(Ease.InFlash)
            //);

            SetPlanes();
            foreach (var plane in planeList)
            {

                Slice(plane);
            }


           
            
           
        }
    }

    //private EzySlice.Plane SetCurrentPlane(int childIndex)
    //{
    //    var sliceTargetMeshFilter = sliceTarget.GetComponent<MeshFilter>();
    //    var bounds = sliceTargetMeshFilter.sharedMesh.bounds;
    //    var distance = DistanceToPlane(sliceTargetMeshFilter.transform.TransformPoint(bounds.center), transform.GetChild(0).transform.right, transform.GetChild(0).transform.position);


    //}

    private void SetPlanes()
    {
        planeList.Clear();
        for (int i = 0; i < transform.childCount; i++)
        {
            var sliceTargetMeshFilter = sliceTarget.GetComponent<MeshFilter>();          
          
            //  planeList.Add(new EzySlice.Plane(x.transform.position, x.transform.up));
            var bounds=sliceTargetMeshFilter.sharedMesh.bounds;
            //Debug.Log(sliceTargetMeshFilter.transform.TransformPoint(bounds.center));
            //var distance = DistanceIgnoreY(transform.GetChild(0).transform.position, sliceTargetMeshFilter.transform.TransformPoint(bounds.center));
            var distance = DistanceToPlane(sliceTargetMeshFilter.transform.TransformPoint(bounds.center), transform.GetChild(i).transform.right, transform.GetChild(i).transform.position);
            Debugger.Log("Distance is "+distance , Debugger.PriorityLevel.Medium);
            //var meshDistance = Mathf.Sqrt((bounds.extents.x * bounds.extents.x) + bounds.extents.z * bounds.extents.z);
            //Debugger.Log("Mesh Distance is " + meshDistance, Debugger.PriorityLevel.Medium);
            //Debugger.Log("bounds.size is " + bounds.size, Debugger.PriorityLevel.Medium);
            distance /= sliceTargetMeshFilter.transform.lossyScale.x;
            //distance = MapF(0, meshDistance, 0, 1, Mathf.Abs(distance))*Mathf.Sign(distance);
            planeList.Add(new EzySlice.Plane(transform.GetChild(i).transform.right, -distance));
        }
    }

    public void Slice()
    {
      
        Slice( GetRandomPlane());

    }
    public void Slice(EzySlice.Plane thePlane)
    {
        var tr = new TextureRegion(1f, 1f, 1.0f, 1.0f);
        var result = EzySlice.Slicer.Slice(sliceTarget, thePlane, tr, slicedFaceMaterial);
        if (result != null)
        {
            
            var componentCopy = sliceTarget.GetComponent<QuickOutline.Outline>();
            var lowerHull = result.CreateLowerHull(sliceTarget, slicedFaceMaterial);
            var upperHull= result.CreateUpperHull(sliceTarget, slicedFaceMaterial);

            lowerHull.transform.position = sliceTarget.transform.position;
            upperHull.transform.position = sliceTarget.transform.position;
            Destroy(lastLowerPart);
            Destroy(lastUpperPart);
            lastLowerPart = lowerHull;
            lastUpperPart = upperHull;
            //CopyComponent(sliceTarget.GetComponent<QuickOutline.Outline>(), lastLowerPart);
            //var component=lastUpperPart.AddComponent<QuickOutline.Outline>();
            //var copy = component.GetCopyOf(sliceTarget.GetComponent<QuickOutline.Outline>());
            
            //var resul22t=lastLowerPart.AddComponent<QuickOutline.Outline>();
            //componentCopy.CopyValuesToOut(resul22t);
            //lastUpperPart.AddComponent<QuickOutline.Outline>();
            
            lastUpperPart.AddComponent<Rigidbody>();

            Destroy(sliceTarget);
            sliceTarget = lastLowerPart;
        }
       
    }

    public void Slice(EzySlice.Plane thePlane, TextureRegion theTr)
    {
        EzySlice.Slicer.Slice(sliceTarget, thePlane, theTr, slicedFaceMaterial)?.CreateLowerHull(sliceTarget,slicedFaceMaterial);
    }

    public EzySlice.Plane GetRandomPlane()
    {
        Vector3 randomPosition = Random.insideUnitSphere / 2;

        //randomPosition += positionOffset;

        Vector3 randomDirection = Random.insideUnitSphere.normalized;
        return new EzySlice.Plane(randomPosition, randomDirection);
    }


    void OnGUI()
    {

        if (GUI.Button(new Rect(109, 790, 150, 230), "Slice"))
            Slice();

        if (GUI.Button(new Rect(209, 790, 250, 230), "SlicePlane"))
            Slice(new EzySlice.Plane(Vector3.left/2, sliceTarget.transform.right),new TextureRegion(0.0f, 0.0f, 1.0f, 1.0f));
        if (GUI.Button(new Rect(409, 790, 250, 230), "Slice Points"))
            Slice(debugPlane, new TextureRegion(0.0f, 0.0f, 1.0f, 1.0f));
    }

    public void OnPointerClick(PointerEventData eventData)
    {
       
    }

    private void OnDrawGizmos()
    {
        debugPlane.OnDebugDraw(Color.red);

       
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.GetChild(0).transform.position, transform.GetChild(0).transform.position+ transform.GetChild(0).transform.right);

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(DebugRay.origin,DebugRay.direction*100f);
    }


    private void MouseScrollEvents(InputAction.CallbackContext callback)
    {
        var result = callback.ReadValue<float>();
        Quaternion rotation = Quaternion.Euler(0, result, 0);
        transform.GetChild(0).transform.rotation *= rotation;
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
