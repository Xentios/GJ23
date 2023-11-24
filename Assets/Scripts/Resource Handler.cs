using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ResourceHandler : MonoBehaviour
{
    [SerializeField]
    private int resourceID;
    [SerializeField]
    private Rigidbody rb;



    private void Start()
    {
        var randomVector = Random.Range(1, 3);
        var rd = Random.onUnitSphere+Random.insideUnitSphere*10;
        rb.AddForce(Vector3.back*15 + rd, ForceMode.VelocityChange);
    }

    private void OnTriggerEnter(Collider other)
    {
        var collector=other.GetComponent<ResourceCollector>();
        collector.CollectedResources.ResourceList[resourceID]++;
        rb.isKinematic = true;
        transform.DOBlendableMoveBy(Vector3.up * 30, 1f).OnComplete(()=>Destroy(gameObject));
    }

   


}
