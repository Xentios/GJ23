using EzySlice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using DG.Tweening;

public class Slicer : MonoBehaviour, IPointerClickHandler
{
    // Start is called before the first frame update
    [SerializeField]
    private InputAction mouseScroll;

    [SerializeField]
    private GameObject theMeat; 
    [SerializeField]
    private Material theMaterial;
    [SerializeField]
    private List<Transform> points;

    private List<EzySlice.Plane> planeList;


    private EzySlice.Plane debugPlane;

    private GameObject lastUpperPart;
    private GameObject lastLowerPart;

    private void Awake()
    {
        planeList = new();
      
    }

    private void Start()
    {
        mouseScroll.performed += MouseScrollEvents;
        debugPlane = //new EzySlice.Plane();
        new EzySlice.Plane(points[0].localPosition, points[1].localPosition, points[2].localPosition);
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
       
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            transform.DOMoveY(-0.5f, 0.5f).SetEase(Ease.InCubic).OnComplete(() =>
            transform.DOMoveY(0, 0.5f).SetEase(Ease.InFlash)
            );

            SetPlanes();
            foreach (var plane in planeList)
            {
                
                Slice(plane);
            }
            //theMeat.SetActive(false);
        }
    }

    private void SetPlanes()
    {
        planeList.Clear();
        for (int i = 0; i < transform.childCount; i++)
        {
            var x = theMeat.GetComponent<MeshFilter>();          
            Debug.Log(x.transform.position);
            //  planeList.Add(new EzySlice.Plane(x.transform.position, x.transform.up));
            var bounds=x.mesh.bounds;
            Debug.Log(x.transform.TransformPoint(bounds.center));
            var distance = Vector3.Distance(transform.GetChild(0).transform.position, x.transform.TransformPoint(bounds.center));
            distance = DistanceToPlane(x.transform.TransformPoint(bounds.center), transform.GetChild(0).transform.right, transform.GetChild(0).transform.position);
            planeList.Add(new EzySlice.Plane(transform.GetChild(0).transform.right,-distance));
        }
    }

    public void Slice()
    {
      
        Slice( GetRandomPlane());

    }
    public void Slice(EzySlice.Plane thePlane)
    {
        var tr = new TextureRegion(0.0f, 0.0f, 1.0f, 1.0f);
        var result = EzySlice.Slicer.Slice(theMeat, thePlane, tr, theMaterial);
        if (result != null)
        {
            var lowerHull = result.CreateLowerHull(theMeat, theMaterial);
            var upperHull= result.CreateUpperHull(theMeat, theMaterial);

            lowerHull.transform.position= theMeat.transform.position + Vector3.forward * 1;
            upperHull.transform.position= theMeat.transform.position + Vector3.forward *-1;
            Destroy(lastLowerPart);
            Destroy(lastUpperPart);
            lastLowerPart = lowerHull;
            lastUpperPart = upperHull;
            lastUpperPart.AddComponent<Rigidbody>();
        }
       
    }

    public void Slice(EzySlice.Plane thePlane, TextureRegion theTr)
    {
        EzySlice.Slicer.Slice(theMeat, thePlane, theTr, theMaterial)?.CreateLowerHull(theMeat,theMaterial);
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
            Slice(new EzySlice.Plane(Vector3.left/2, theMeat.transform.right),new TextureRegion(0.0f, 0.0f, 1.0f, 1.0f));
        if (GUI.Button(new Rect(409, 790, 250, 230), "Slice Points"))
            Slice(debugPlane, new TextureRegion(0.0f, 0.0f, 1.0f, 1.0f));
    }

    public void OnPointerClick(PointerEventData eventData)
    {
       
    }

    private void OnDrawGizmos()
    {
        debugPlane.OnDebugDraw(Color.red);
    }


    private void MouseScrollEvents(InputAction.CallbackContext callback)
    {
        var result = callback.ReadValue<float>();
        Quaternion rotation = Quaternion.Euler(0, result, 0);
        transform.GetChild(0).transform.rotation *= rotation;
    }

    private float DistanceToPlane(Vector3 point, Vector3 planeNormal, Vector3 pointOnPlane)
    {
        return Vector3.Dot(point - pointOnPlane, planeNormal);
    }
}
