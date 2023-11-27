using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PickSpikes : Pickers
{
    protected override void OnEnableActions()
    {        
        textField.text = ""+shopRequest.SpikeCount;
    }
}
