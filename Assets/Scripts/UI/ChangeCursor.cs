using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCursor : MonoBehaviour
{
    [SerializeField]
    private bool IsVisible;

    private bool WasVisible;

    private void OnEnable()
    {
        WasVisible = Cursor.visible;
        Cursor.visible = IsVisible;
    }

    private void OnDisable()
    {
        Cursor.visible = WasVisible;
    }
   
}
