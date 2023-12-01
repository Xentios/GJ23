using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlidersStart : MonoBehaviour
{
    [SerializeField]
    private Slider slider;

    [SerializeField]
    private string keyString;

    private void OnEnable()
    { 
        slider.value=PlayerPrefs.GetFloat(keyString, 1f);
    }
}
