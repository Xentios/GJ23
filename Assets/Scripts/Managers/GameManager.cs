using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Es.InkPainter;
using DG.Tweening;
using static Es.InkPainter.InkCanvas;
using System;
using UnityEngine.SceneManagement;

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
        Start,
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
    public List<Transform> ShowCaseLocation;
    [SerializeField]
    public List<ShopResult> ShopResults;
    private ShopResult currentShopResult;
    private int customerIndex;

    [SerializeField]
    public List<GameEvent> GameEvents;

    [SerializeField]
    public GameObject targetObjectPrefab;
    [HideInInspector]
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

    private int PlacedSpikes;

    [SerializeField]
    private Cinemachine.CinemachineTargetGroup cinemachineTargetGroup;

    [SerializeField]
    private GameObject ScorePanel;
    [SerializeField]
    private SliderValueChanger SliderShapeArea;
    [SerializeField]
    private SliderValueChanger SliderPressValue;
    [SerializeField]
    private SliderValueChanger SliderPaintArea;
    [SerializeField]
    private SliderValueChanger SliderSpikeCount;
    [SerializeField]
    private SliderValueChanger SliderHammerScore;


    private GamePhases currentGamePhase;


    private void Start()
    {
        ChangePhases();
    }
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
        GameEvents[(int) currentGamePhase].TriggerEvent();
        switch (currentGamePhase)
        {
            case GamePhases.Start:
           
            currentShopResult = ShopResults[customerIndex];
            targetObject = Instantiate(targetObjectPrefab).transform.GetChild(0).gameObject;
            PlacedSpikes = 0;
            break;
            case GamePhases.Cut:

            break;
            case GamePhases.Press:
            float calculatedArea = CalculateArea(targetObject.GetComponent<MeshFilter>().mesh, targetObject.transform.lossyScale);
            float resultOfArea = MathF.Min(calculatedArea/ShopRequest.GetWantedArea(), 1f);
            float wantedShape = slicer.GetComponent<Slicer>().IndexOfSliceHolder == ShopRequest.ShapeID ? 1f : 0f;
            SliderShapeArea.FinalValue = (resultOfArea+wantedShape)/2f;
            currentShopResult.CutPercentage = SliderShapeArea.FinalValue * 100f;

            break;
            case GamePhases.Paint:
            SliderPressValue.FinalValue = (1 - (Mathf.Abs(ShopRequest.PressScale - targetObject.transform.lossyScale.y)));
            currentShopResult.PressPercentage = SliderPressValue.FinalValue * 100f;

            InkCanvasAdder();
            break;
            case GamePhases.Spike:
            var lenght = cinemachineTargetGroup.m_Targets.Length;
            for (int i = lenght - 1; i >= 0; i--)
            {
                cinemachineTargetGroup.RemoveMember(cinemachineTargetGroup.m_Targets[i].target.transform);
            }

            SliderPaintArea.FinalValue = CalculateColor() / 0.028f;//TODO HARD CODED
            currentShopResult.PaintPercentage = SliderPaintArea.FinalValue;//TODO FIX HERE

            //TODO CALCULATE OTHER COLORS


            targetObject.layer = LayerMask.NameToLayer("Sliceable");
            cinemachineTargetGroup.AddMember(targetObject.transform, 1, 1);


            break;
            case GamePhases.Hammer:
            SliderSpikeCount.FinalValue = (float) PlacedSpikes / (float) ShopRequest.SpikeCount;
            currentShopResult.SpikePercentage = SliderSpikeCount.FinalValue * 100f;

            break;
            case GamePhases.End:

            var realScale = targetObject.transform.lossyScale;
            targetObject.transform.parent = null;
            targetObject.transform.localScale = realScale;

            targetObject.transform.parent = new GameObject("Final Result").transform;
            foreach (var spikes in Hammer.GetComponent<Hammer>().hammeredSpikes)
            {
                spikes.transform.parent = targetObject.transform.parent;
            }
            targetObject = targetObject.transform.parent.gameObject;
            targetObject.AddComponent<Rotate>();

            break;
            default:
            break;
        }

    }

    [ContextMenu("Calculate Area")]
    public void CalculateAreaForEditor()
    {
        CalculateArea(targetObject.GetComponent<MeshFilter>().mesh, targetObject.transform.lossyScale);
    }

    public float CalculateArea(Mesh mf, Vector3 scale)
    {
        scale.y = 1;
        float area = 0f;

        for (int i = 0; i < mf.triangles.Length; i += 3)
        {
            int vertexIndex1 = mf.triangles[i];
            int vertexIndex2 = mf.triangles[i + 1];
            int vertexIndex3 = mf.triangles[i + 2];
            Vector3 normal = mf.normals[i / 3];

            Vector3 v1 = mf.vertices[vertexIndex1];
            Vector3 v2 = mf.vertices[vertexIndex2];
            Vector3 v3 = mf.vertices[vertexIndex3];

            v1 = Vector3.Scale(v1, scale);
            v2 = Vector3.Scale(v2, scale);
            v3 = Vector3.Scale(v3, scale);

            if (Vector3.up == normal)
            {
                area += Vector3.Cross(v2 - v1, v3 - v1).magnitude * 0.5f;
            }
        }

        Debugger.Log("Area value is " + area, Debugger.PriorityLevel.MustShown);
        return area;
    }

    [ContextMenu("Calculate Color")]

    public float CalculateColor()
    {
        return painter.GetComponent<Painter>().CheckColors(targetObject.GetComponent<MeshRenderer>(), ShopRequest.Color);

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
            sets.Add(new PaintSet("_BaseMap", "_BumpMap", "_ParallaxMap", true, false, false, mats[i]));
        }

        //PaintSet set=new PaintSet("_MainTex", "_BumpMap", "_ParallaxMap",true,false,false, mat);
        targetObject.AddInkCanvas(sets);
        targetObject.gameObject.SetActive(true);
    }


    public void ASpikePlaced()
    {
        PlacedSpikes++;
        if (PlacedSpikes >= ShopRequest.SpikeCount)
        {
            GoToNextGameEvent();
        }
    }

    public void ASpikeHammered(float result)
    {
        PlacedSpikes--;
        if (PlacedSpikes <= 0)
        {
            SliderHammerScore.FinalValue = result;
            currentShopResult.HammerPercentage = result * 100;
            GoToNextGameEvent();
        }
    }

    public float GetOverallResults()
    {
        var final = SliderHammerScore.FinalValue + SliderSpikeCount.FinalValue + SliderPaintArea.FinalValue + SliderPressValue.FinalValue + SliderShapeArea.FinalValue;
        final /= 5;
        return final;
    }

    public Vector3 GetTopPlaneOfTarget()
    {
        var targetObjectTransform = targetObject.transform;

        Vector3 topPosition = targetObjectTransform.position + Vector3.Scale(Vector3.up, targetObjectTransform.lossyScale) * targetObjectTransform.GetComponent<MeshFilter>().sharedMesh.bounds.extents.y;
        return topPosition;
    }

    public Vector3 GetBottomOffPositionOfAnObject(GameObject TO)
    {
        var targetObjectTransform = TO.transform;

        Vector3 bottomPosition = targetObjectTransform.position - Vector3.Scale(Vector3.up, targetObjectTransform.localScale) * targetObjectTransform.GetComponent<MeshFilter>().sharedMesh.bounds.extents.y;
        return bottomPosition;
    }

    public void StartNextPhase()
    {

        targetObject.transform.parent = ShowCaseLocation[customerIndex];
        targetObject.transform.localPosition = Vector3.zero;
        var bottomPosition = GetBottomOffPositionOfAnObject(targetObject.transform.GetChild(0).gameObject);
        var distance = Vector3.Distance(targetObject.transform.position, bottomPosition);
        Debug.Log(distance);
        distance *= targetObject.transform.localScale.y;
        var currentPosition = targetObject.transform.localPosition;
        currentPosition.y -= distance;
        targetObject.transform.localPosition = currentPosition;
        targetObject.GetComponent<Rotate>().StopRotating();
        customerIndex++;

        if (customerIndex >= ShopResults.Count)
        {
            SceneManager.LoadScene("BattleScene");
            return;
        }
        currentGamePhase = 0;
        ChangePhases();
    }
}
