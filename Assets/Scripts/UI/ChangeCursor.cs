using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class ChangeCursor : MonoBehaviour
{

    [SerializeField]
    private InputActionAsset map;

    [SerializeField]
    private bool IsVisible;

    private bool WasVisible;

    private void OnEnable()
    {
        WasVisible = Cursor.visible;
        Cursor.visible = IsVisible;
        map.Enable();
    }

    private void OnDisable()
    {
        Cursor.visible = WasVisible;
        map.Disable();
    }
   
}
