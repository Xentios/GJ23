using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ResourceHandler : MonoBehaviour
{
    [SerializeField]
    public int resourceID;
    [SerializeField]
    private Rigidbody rb;


    private void Start()
    {        
        var rd = Random.onUnitSphere+Random.insideUnitSphere*10;
        rb.AddForce(Vector3.back*15 + rd, ForceMode.VelocityChange);
    }


}
