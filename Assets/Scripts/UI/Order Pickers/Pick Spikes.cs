using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PickSpikes : Pickers
{
    private void Start()
    {        
        textField.text = ""+shopRequest.SpikeCount;
    }
}
