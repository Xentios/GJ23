using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeyActionEvent : MonoBehaviour
{
    [SerializeField] KeyCode KeyCode;
    [SerializeField] UnityEvent UnityEvent;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode)){
            UnityEvent?.Invoke();
        }
        
    }
}
