using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceCameraPriority : MonoBehaviour
{
    [SerializeField]
    CinemachineBrain cinemachineBrain;

    [SerializeField]
    private CinemachineVirtualCamera myCinemachineVirtualCamera;

    private ICinemachineCamera lastActiveCam;
  

    public void ActiveMyCamera()
    {
        lastActiveCam = cinemachineBrain.ActiveVirtualCamera;
        if (lastActiveCam.Equals(myCinemachineVirtualCamera)) return;
        myCinemachineVirtualCamera.Priority = 11;
       
        //myCinemachineVirtualCamera.SOlo
    }

    public void DeActiveMyCamera()
    {
        if (lastActiveCam.Equals(myCinemachineVirtualCamera)) return;
        myCinemachineVirtualCamera.Priority = 9;
    }
}
