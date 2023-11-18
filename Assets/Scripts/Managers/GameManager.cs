using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Es.InkPainter;
using static Es.InkPainter.InkCanvas;

public class GameManager : MonoBehaviour
{

    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }

            return instance;
        }
    }

    enum GamePhases
    {
        Cut,
        Press,
        Paint,
        Spike,
        Hammer,
        
        End
            
    }

    [SerializeField]
    public ShopRequest ShopRequest;

    [SerializeField]
    public GameObject targetObject;

    [SerializeField]
    private GameObject slicer;

    [SerializeField]
    private GameObject pressMachine;

    [SerializeField]
    private GameObject painter;
    [SerializeField]
    private GameObject colorChecker;

    [SerializeField]
    private GameObject Hammer;

    [SerializeField]
    private GameObject spikeEvents;
    [SerializeField]
    private Cinemachine.CinemachineTargetGroup cinemachineTargetGroup;


    private GamePhases currentGamePhase;

    
    public void GoToNextGameEvent()
    {
        currentGamePhase++;
        ChangePhases();
    }

    public void GetTargetObject(GameObject targetGameObject)
    {
        targetObject = targetGameObject;
    }


    private void ChangePhases()
    {
        switch (currentGamePhase)
        {
            case GamePhases.Press:
            slicer.SetActive(false);
            pressMachine.SetActive(true);
            CalculateArea(targetObject.GetComponent<MeshFilter>().mesh, targetObject.transform.lossyScale);
            //CalculateArea(targetObject.GetComponent<MeshFilter>().sharedMesh);
            //targetObject.AddComponent<BoxCollider>().isTrigger = true;
            break;
            case GamePhases.Paint:
            pressMachine.SetActive(false);
            painter.SetActive(true);
            colorChecker.SetActive(true);
            colorChecker.GetComponent<ColorCheker>().targetColor = ShopRequest.Color;
            InkCanvasAdder();
            break;
            case GamePhases.Spike:
            CalculateColor();
            painter.SetActive(false);
            colorChecker.SetActive(false);           
            cinemachineTargetGroup.AddMember(targetObject.transform, 1, 1);
            spikeEvents.SetActive(true);

            break;
            case GamePhases.Hammer:
            spikeEvents.SetActive(false);
            Hammer.SetActive(true);
            break;
            case GamePhases.End:
            break;
            default:
            break;
        }

    }

    public void CalculateArea(Mesh mf,Vector3 scale)
    {
        scale.y = 1;
        //var mf = targetObject.GetComponent<MeshFilter>().sharedMesh;
        float area=0f;

        for (int i = 0; i < mf.triangles.Length; i += 3)
        {
            int vertexIndex1 = mf.triangles[i];
            int vertexIndex2 = mf.triangles[i + 1];
            int vertexIndex3 = mf.triangles[i + 2];
            Vector3 normal = mf.normals[i / 3];

            Vector3 v1 = mf.vertices[vertexIndex1];
            Vector3 v2 = mf.vertices[vertexIndex2];
            Vector3 v3 = mf.vertices[vertexIndex3];

            v1=Vector3.Scale(v1,scale);
            v2=Vector3.Scale(v2,scale);
            v3 = Vector3.Scale(v3, scale);

            if (Vector3.up == normal)
            {
                Debug.Log("BINGO " + i);
                area += Vector3.Cross(v2 - v1, v3 - v1).magnitude * 0.5f;
            }
        }

        Debug.Log(area);
    }

        [ContextMenu("Calculate Color")]

    public void CalculateColor()
    {
        painter.GetComponent<Painter>().CheckColors(targetObject.GetComponent<MeshRenderer>());

    }

    [ContextMenu("AddInkCanvas")]
    public void InkCanvasAdder()
    {
        targetObject.gameObject.SetActive(false);
        targetObject.AddComponent<MeshCollider>();
        Material[] mats = targetObject.GetComponent<MeshRenderer>().materials;

        List<PaintSet> sets = new List<PaintSet>();

        for (int i = 0; i < mats.Length; i++)
        {
            sets.Add(new PaintSet("_MainTex", "_BumpMap", "_ParallaxMap", true, false, false, mats[i]));
        }

        //PaintSet set=new PaintSet("_MainTex", "_BumpMap", "_ParallaxMap",true,false,false, mat);
        targetObject.AddInkCanvas(sets);
        targetObject.gameObject.SetActive(true);
    }

}
