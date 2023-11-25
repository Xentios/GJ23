using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PickScale : Pickers
{

    private void Awake()
    {       
        image.rectTransform.localScale = Vector3.one;
    }

    private void OnEnable()
    {
        var imageScale = image.rectTransform.localScale;
        imageScale.y =  shopRequest.PressScale;
        image.rectTransform.localScale = imageScale;
        string formattedString = string.Format("{0:0.#}", shopRequest.PressScale);
       
        textField.text = formattedString;
    }
}
