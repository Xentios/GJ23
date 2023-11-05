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
    private GameObject theMeat; 
    [SerializeField]
    private Material theMaterial;

    private List<EzySlice.Plane> planeList;

    private void Awake()
    {
        planeList = new();
        
    }

    private void Update()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(new Vector3( Mouse.current.position.x.value, Mouse.current.position.y.value, 25f));        
        mousePos.y = transform.position.y;
        transform.position = mousePos;

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            transform.DOMoveY(0.5f, 0.5f).SetEase(Ease.InCubic).OnComplete(() =>
            transform.DOMoveY(1, 0.5f).SetEase(Ease.InFlash)
            );

            SetPlanes();
            foreach (var plane in planeList)
            {
                
                Slice(plane);
            }
            
        }
    }

    private void SetPlanes()
    {
        planeList.Clear();
        for (int i = 0; i < transform.childCount; i++)
        {
            var x = transform.GetChild(i).GetComponent<MeshFilter>();
            var normals = x.mesh.normals;
            planeList.Add(new EzySlice.Plane(x.transform.position, x.transform.up));
        }
    }

    public void Slice()
    {
      
        Slice( GetRandomPlane());

    }
    public void Slice(EzySlice.Plane thePlane)
    {
        var tr = new TextureRegion(0.0f, 0.0f, 1.0f, 1.0f);
        EzySlice.Slicer.Slice(theMeat, thePlane, tr, theMaterial)?.CreateLowerHull(theMeat, theMaterial);
    }

    public void Slice(EzySlice.Plane thePlane, TextureRegion theTr)
    {
        EzySlice.Slicer.Slice(theMeat, thePlane, theTr, theMaterial).CreateLowerHull(theMeat,theMaterial);
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
            Slice(new EzySlice.Plane(theMeat.transform.position, Vector3.right),new TextureRegion(0.0f, 0.0f, 1.0f, 1.0f));
    }

    public void OnPointerClick(PointerEventData eventData)
    {
       
    }
}
