using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TargetSwitcher : MonoBehaviour
{
    // Start is called before the first frame update

    private CinemachineTargetGroup targetGroup;
    void Awake()
    {
        targetGroup = GetComponent<CinemachineTargetGroup>();
    }


            private void OnTriggerEnter(Collider other)
    {
        Debugger.Log(other.name + " is on Trigger enter to " + transform.name);
        if (other.tag != "Target") return;

        var targets = targetGroup.m_Targets;
        targets[targets.Length - 1].target = other.transform;
        GameObject.Destroy(other);
    }

    private void OnCollisionEnter(Collision other)
    {
        Debugger.Log(other.transform.name + " is on Trigger enter to " + transform.name);
    }
}
