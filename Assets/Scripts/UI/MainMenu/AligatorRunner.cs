using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class AligatorRunner : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI textFieldToSteal;
    [SerializeField]
    TextMeshProUGUI textFieldInMouth;
    [SerializeField]
    TextMeshProUGUI textToAnimate;
    [SerializeField]
    private float typingSpeed = 0.1f;
    void Start() {
        //textToAnimate.maxVisibleCharacters = 4;
        var target = new Vector3(-169.399994f, -123.699997f, 0);
        transform.DOLocalMove(target, 1f).OnComplete(ArriveLocation).SetEase(Ease.OutQuad);
    }
   
    private void ArriveLocation()
    {
        textFieldToSteal.text = "Press '     ' TO START ";
        textFieldInMouth.gameObject.SetActive(true);
        var target = new Vector3(900f, -123.699997f, 0);
        transform.DOLocalMove(target, 1f).OnComplete(ArriveLocation).OnComplete(ExitScreen);

    }

    private void ExitScreen()
    {
        textToAnimate.gameObject.SetActive(true);

        textToAnimate.maxVisibleCharacters = textToAnimate.text.Length;

        // Use DOTween.To to gradually decrease maxVisibleCharacters to 4
        DOTween.To(
            () => textToAnimate.maxVisibleCharacters,
            x => textToAnimate.maxVisibleCharacters = x,
            4,
            3f
        ).SetEase(Ease.OutQuad).OnComplete(WriteSorry);


       // DOTween.To(() => textToAnimate.maxVisibleCharacters, x => textToAnimate.maxVisibleCharacters = textToAnimate.text.Length, 4, 2f)
            
    }

    private void WriteSorry()
    {
        textToAnimate.text = "Tip:I GUESS YOU CAN STILL CLICK....";
        textToAnimate.maxVisibleCharacters = 4;
        DOTween.To(() => textToAnimate.maxVisibleCharacters, x => textToAnimate.maxVisibleCharacters = x, textToAnimate.text.Length, 3f);
    }
    // Update is called once per frame

}
