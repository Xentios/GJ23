using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ActivateNewCamera : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera gameEndCam;
   
    public void AnimationEvent()
    {
        gameEndCam.Priority = 11;
    }
}
