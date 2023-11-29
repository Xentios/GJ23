using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class AnimateTextLetterbyLetter : MonoBehaviour
{
    [SerializeField]
    private float startDelay;
    [SerializeField]
    private float duration;

    private TextMeshProUGUI textToAnimate;

    private void Awake()
    {
        textToAnimate = GetComponent<TextMeshProUGUI>();
    }

    
    void Start()
    {
        textToAnimate.maxVisibleCharacters = 0;
        DOTween.To(() => textToAnimate.maxVisibleCharacters, x => textToAnimate.maxVisibleCharacters = x, textToAnimate.text.Length, duration).SetDelay(startDelay);
    }

}
