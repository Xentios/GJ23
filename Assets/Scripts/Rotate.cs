using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Rotate : MonoBehaviour
{
    [SerializeField]
    private RotateMode  rotateMode= RotateMode.WorldAxisAdd;

    private void Start()
    {
        transform.DORotate(new Vector3(0, 360, 0), 10.0f, rotateMode)
           .SetLoops(-1, LoopType.Restart)
           .SetEase(Ease.InOutCubic);
    }
}
