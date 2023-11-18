using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Es.InkPainter;
using DG.Tweening;
using static Es.InkPainter.InkCanvas;
using System;

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
            SliderShapeArea.FinalValue =CalculateArea(targetObject.GetComponent<MeshFilter>().mesh, targetObject.transform.lossyScale)/25;//TODO HARDCODED
            //CalculateArea(targetObject.GetComponent<MeshFilter>().sharedMesh);
            //targetObject.AddComponent<BoxCollider>().isTrigger = true;
            break;
            case GamePhases.Paint:
            SliderPressValue.FinalValue =1-targetObject.transform.lossyScale.y;
            pressMachine.SetActive(false);
            painter.SetActive(true);
            colorChecker.SetActive(true);
            colorChecker.GetComponent<ColorCheker>().targetColor = ShopRequest.Color;
            InkCanvasAdder();
            break;
            case GamePhases.Spike:
            SliderPaintArea.FinalValue = CalculateColor()/0.028f;//TODO HARD CODED
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
           
          
           
           
            SliderSpikeCount.FinalValue = 1f;
            ScorePanel.SetActive(true);

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
    
    public float CalculateArea(Mesh mf,Vector3 scale)
    {
        scale.y = 1;        
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

        Debugger.Log("Area value is "+area,Debugger.PriorityLevel.MustShown);
        return area;
    }

        [ContextMenu("Calculate Color")]

    public float CalculateColor()
    {
       return  painter.GetComponent<Painter>().CheckColors(targetObject.GetComponent<MeshRenderer>());

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
            GoToNextGameEvent();
        }
    }

    public float GetOverallResults()
    {
        var final = SliderHammerScore.FinalValue + SliderSpikeCount.FinalValue + SliderPaintArea.FinalValue + SliderPressValue.FinalValue + SliderShapeArea.FinalValue;
        final /= 5;
        return final;
    }
}
