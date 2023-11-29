using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using DG.Tweening;

public class SpaceCameraPriority : MonoBehaviour
{
    [SerializeField]
    CinemachineBrain cinemachineBrain;

    [SerializeField]
    private CinemachineVirtualCamera myCinemachineVirtualCamera;

    [SerializeField]
    private Volume volume;

    private ICinemachineCamera lastActiveCam;
  

    public void ActiveMyCamera()
    {
        lastActiveCam = cinemachineBrain.ActiveVirtualCamera;
       
        if (lastActiveCam.Equals(myCinemachineVirtualCamera)) return;
        InputSystem.DisableDevice(Mouse.current, keepSendingEvents: false);
        myCinemachineVirtualCamera.Priority = 11;
        DOTween.To(() => volume.weight, x => volume.weight = x, 1f, 0.6f);


        //myCinemachineVirtualCamera.SOlo
    }

    public void DeActiveMyCamera()
    {
       
        if (lastActiveCam.Equals(myCinemachineVirtualCamera)) return;
        myCinemachineVirtualCamera.Priority = 9;
        InputSystem.EnableDevice(Mouse.current);
        DOTween.To(() => volume.weight, x => volume.weight = x, 0.005f, 0.6f);
    }
}