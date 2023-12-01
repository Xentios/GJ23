using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonSkipper : MonoBehaviour
{
    [SerializeField]
    private UnityEvent unityEvents;

    [SerializeField]
    private List<GameObject> StoryPanels;

    [SerializeField]
    private GameObject ErrorPanel;

    private int panelIndex;

    private void OnEnable()
    {
        AutoInvoke();
    }

    private void AutoInvoke()
    {
        var length = StoryPanels[panelIndex].GetComponentInChildren<AudioSource>().clip.length;     
        Invoke("Skip", length);
    }

    public void Skip()
    {
        CancelInvoke();
        
        StoryPanels[panelIndex].SetActive(false);
        panelIndex++;
        if (panelIndex < StoryPanels.Count)
        {
            AutoInvoke();
            StoryPanels[panelIndex].SetActive(true);
        }
        else
        {           
#if UNITY_WEBGL
            ErrorPanel.SetActive(true);
#else
            unityEvents.Invoke();
#endif
        }      
    }
}
