using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeUIText : MonoBehaviour
{
    public void ChangeTextValue(float value)
    {

        value *= 100;
        GetComponent<TMPro.TextMeshProUGUI>().text = value + "%";
    }
}
