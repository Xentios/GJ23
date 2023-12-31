using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PerfectScoreAnimation : MonoBehaviour
{

    public void OnDisable()
    {
        transform.localScale = new Vector3(0, 1, 1);
    }
    public void GetScore(float score)
    {
        if (score > 0.95f)
        {
            transform.DOScale(1f, 0.3f).SetEase(Ease.OutQuart).SetDelay(0.3f);
        }
    }
}
