using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PickShape : Pickers
{
    [SerializeField]
    private List<Sprite> imageList;
   
    private void OnEnable()
    {       
        textField.text = shopRequest.ShapeName;
        image.sprite = imageList[shopRequest.ShapeID];
    }
}
