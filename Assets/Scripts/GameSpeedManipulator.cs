using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSpeedManipulator : MonoBehaviour
{   
    [SerializeField]
    private float timeSpeed;

    private float old_value;

    public void SpeedUp()
    {
        old_value = Time.timeScale;
        Time.timeScale = timeSpeed;
    }

    public void SpeedDown()
    {      
        Time.timeScale = old_value;
    }

}
