using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PickScale : Pickers
{

    protected override void OnAwakeActions()
    {       
        image.rectTransform.localScale = Vector3.one;
    }

    protected override void OnEnableActions()
    {
        
        var imageScale = image.rectTransform.localScale;
        imageScale.y =  shopRequest.PressScale;
        image.rectTransform.localScale = imageScale;
        string formattedString = string.Format("{0:0.#}", shopRequest.PressScale);
       
        textField.text = formattedString;
    }
}
