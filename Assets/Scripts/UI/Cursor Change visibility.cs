using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorChangeVisibility : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnEnable()
    {
        Cursor.visible = true;
    }

    private void OnDisable()
    {
        Cursor.visible = false;
    }
}
