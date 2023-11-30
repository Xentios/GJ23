using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(RectTransform))]
public class UIMover : MonoBehaviour
{
    [SerializeField]
    public float MoveDelta = 1f;
    [SerializeField]
    public float Duration = 1.0f;
    [SerializeField]
    public bool Hidden = false;

    [SerializeField]
    public RectTransform rectTransform;

    private void Awake()
    {
       // rectTransform = GetComponent<RectTransform>();       
    }

  

    [ContextMenu("Hide")]
    public void HideY()
    {
        if (Hidden == true) return;

        Hidden = true;
        Debugger.Log("HideY of " + gameObject.name, Debugger.PriorityLevel.Medium);
        rectTransform.DOAnchorPosY(rectTransform.anchoredPosition.y - MoveDelta, Duration);      
    }
    [ContextMenu("Show")]
    public void ShowY()
    {
        if (Hidden == false) return;
       
        Hidden = false;
        Debugger.Log("ShowY of " + gameObject.name, Debugger.PriorityLevel.Medium);
        rectTransform.DOAnchorPosY(rectTransform.anchoredPosition.y + MoveDelta, Duration);
    }
}
