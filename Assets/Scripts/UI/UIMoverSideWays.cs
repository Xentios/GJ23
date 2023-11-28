using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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

    [ContextMenu("Hide")]
    public void HideX()
    {
        if (Hidden == true) return;

        Hidden = true;
        Debugger.Log("HideY of " + gameObject.name, Debugger.PriorityLevel.Medium);
        rectTransform.DOAnchorPosX(rectTransform.anchoredPosition.x - MoveDelta, Duration);      
    }
    [ContextMenu("Show")]
    public void ShowX()
    {
        if (Hidden == false) return;
       
        Hidden = false;
        Debugger.Log("ShowY of " + gameObject.name, Debugger.PriorityLevel.Medium);
        rectTransform.DOAnchorPosX(rectTransform.anchoredPosition.x + MoveDelta, Duration).SetDelay(ShowDelay);
    }
}
