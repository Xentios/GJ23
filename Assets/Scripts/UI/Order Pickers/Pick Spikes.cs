using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PickSpikes : Pickers
{
    private void OnEnable()
    {        
        textField.text = ""+shopRequest.SpikeCount;
    }
}
