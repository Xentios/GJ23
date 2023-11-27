using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PickShape : Pickers
{
    [SerializeField]
    private List<Sprite> imageList;

    protected override void OnEnableActions()
    {       
        textField.text = shopRequest.ShapeName;
        image.sprite = imageList[shopRequest.ShapeID];
    }
}
