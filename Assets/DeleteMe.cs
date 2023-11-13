using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteMe : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        var currentScale = other.transform.localScale;
        
        currentScale.y -= 0.06f;
        other.transform.localScale = currentScale;
    }

    private void OnTriggerEnter(Collider other)
    {
        var currentScale = other.transform.localScale;

        currentScale.y -= 0.01f;
        other.transform.localScale = currentScale;
    }

    //private void OnCollisionStay(Collision other)
    //{
    //    var currentScale = other.transform.localScale;
    //    currentScale.y -= 0.1f;
    //    other.transform.localScale = currentScale;
    //}
}
