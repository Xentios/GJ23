using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class ChangeCursor : MonoBehaviour
{

    [SerializeField]
    private InputActionAsset map;

    [SerializeField]
    private GameObject cursorVisibilityObject;

    [SerializeField]
    private bool IsVisible;

    private bool WasVisible;

    private void OnEnable()
    {
        Cursor.visible = cursorVisibilityObject.activeInHierarchy;
        map.Enable();
        
    }

    private void OnDisable()
    {
        WasVisible = Cursor.visible;
        Cursor.visible = IsVisible;
        map.Disable();       
    }
   
}
