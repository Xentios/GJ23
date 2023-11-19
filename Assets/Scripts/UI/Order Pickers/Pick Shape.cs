using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PickShape : Pickers
{
   
    private void Start()
    {       
        textField.text = shopRequest.ShapeName;
    }
}
