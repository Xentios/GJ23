using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixTimeDelta : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnDestroy()
    {
        Time.timeScale = 1f;
    }
}
