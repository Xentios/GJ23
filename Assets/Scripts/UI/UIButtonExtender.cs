using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("UI/UI Button", 30)]
public class UIButtonExtender : UnityEngine.UI.Button
{
    public void Click()
    {
        onClick.Invoke();
    }
}
