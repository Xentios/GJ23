using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashSpindleColor : MonoBehaviour
{
    [SerializeField]
    private QuickOutline.Outline outline;

  
   

    private void OnEnable()
    {
        DOTween.To(() => outline.OutlineColor, color => outline.OutlineColor = color, Color.green, 1f)
          .SetLoops(-1, LoopType.Yoyo)
          .SetEase(Ease.InOutQuad);

        DOTween.To(() => outline.OutlineWidth, x => outline.OutlineWidth = x, 2f, 1f)
         .SetLoops(-1, LoopType.Yoyo)
         .SetEase(Ease.InOutQuad);

    }
}
