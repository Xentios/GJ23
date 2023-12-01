using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class UIMoverSideWays : MonoBehaviour
{
    [SerializeField]
    public float MoveDelta = 1f;
    [SerializeField]
    public float Duration = 1.0f;
    [SerializeField]
    public bool Hidden = false;
    [SerializeField]
    public float ShowDelay = 3f;

    [SerializeField]
    private RectTransform rectTransform;

    [SerializeField]
    public Button denyButton;
    [SerializeField]
    public Button acceptButton;

    [ContextMenu("Hide")]
    public void HideX()
    {
        if (Hidden == true) return;

        Hidden = true;
        Debugger.Log("HideY of " + gameObject.name, Debugger.PriorityLevel.Medium);
        rectTransform.DOAnchorPosX(rectTransform.anchoredPosition.x - MoveDelta, Duration/4);      
    }
    [ContextMenu("Show")]
    public void ShowX()
    {
        if (Hidden == false) return;
       
        Hidden = false;
        Debugger.Log("ShowY of " + gameObject.name, Debugger.PriorityLevel.Medium);
        rectTransform.DOAnchorPosX(rectTransform.anchoredPosition.x + MoveDelta, Duration).SetDelay(ShowDelay).OnComplete(ShowEnableButtons);
    }

    private void ShowEnableButtons()
    {
        denyButton.interactable = true;
        acceptButton.interactable = true;            
    }
}
