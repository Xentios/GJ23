using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeUIText : MonoBehaviour
{
    public void ChangeTextValue(float value)
    {

        value *= 100;
        string formattedString = string.Format("{0:0.##}", value);
        GetComponent<TMPro.TextMeshProUGUI>().text = formattedString + " %";
    }
}
