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
        Paint,
        Spike,
        End
            
    }

    [SerializeField]
    private GameObject targetObject;

    [SerializeField]
    private GameObject slicer;

    [SerializeField]
    private GameObject painter;
    [SerializeField]
    private GameObject colorChecker;

    [SerializeField]
    private GameObject UICameraEventSystem;


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
            case GamePhases.Cut:
            break;
            case GamePhases.Paint:
            slicer.SetActive(false);
            painter.SetActive(true);
            colorChecker.SetActive(true);


            targetObject.AddComponent<MeshCollider>();
            Material[] mats = targetObject.GetComponent<Renderer>().materials;
            
            List<PaintSet> sets = new List<PaintSet>();

            for (int i = 0; i < mats.Length; i++)
            {
                sets.Add(new PaintSet("_MainTex", "_BumpMap", "_ParallaxMap", true, false, false, mats[i]));
            }

            //PaintSet set=new PaintSet("_MainTex", "_BumpMap", "_ParallaxMap",true,false,false, mat);
            targetObject.AddInkCanvas(sets);
            //targetObject.AddComponent<Es.InkPainter.InkCanvas>();
            break;
            case GamePhases.Spike:
            break;
            case GamePhases.End:
            break;
            default:
            break;
        }

    }

}
