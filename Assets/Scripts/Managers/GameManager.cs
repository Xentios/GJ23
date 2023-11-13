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
                instance = new GameManager();
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
        End
            
    }


    private void Awake()
    {
#if UNITY_EDITOR==false
        Cursor.visible = false;
#endif
    }

    [SerializeField]
    private GameObject targetObject;

    [SerializeField]
    private GameObject slicer;

    [SerializeField]
    private GameObject pressMachine;

    [SerializeField]
    private GameObject painter;
    [SerializeField]
    private GameObject colorChecker;

    [SerializeField]
    private GameObject UICameraEventSystem;
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
            targetObject.AddComponent<BoxCollider>().isTrigger = true;
            break;
            case GamePhases.Paint:
            pressMachine.SetActive(false);
            painter.SetActive(true);
            colorChecker.SetActive(true);      
            InkCanvasAdder();
            break;
            case GamePhases.Spike:
            CalculateColor();
            painter.SetActive(false);
            colorChecker.SetActive(false);           
            cinemachineTargetGroup.AddMember(targetObject.transform, 1, 1);
            UICameraEventSystem.SetActive(true);

            break;
            case GamePhases.End:
            break;
            default:
            break;
        }

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
