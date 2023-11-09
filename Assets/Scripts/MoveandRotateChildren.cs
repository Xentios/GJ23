using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveandRotateChildren : MonoBehaviour
{

    public float radius = 4.0f; // Radius of the circle

    private int numberOfChildren;
    void Start()
    {
        numberOfChildren = transform.childCount;
        var size=transform.GetChild(0).GetComponent<Renderer>().bounds.size;
        float longestSide = Mathf.Max(size.x, size.y, size.z);
        ArrangeChildren(longestSide);
    }

    void ArrangeChildren(float lenght)
    {
        var circumference = lenght * numberOfChildren;
        var r=circumference / (2 * Mathf.PI);
        radius = r;

        for (int i = 0; i < numberOfChildren; i++)
        {
            float angle = i * (2 * Mathf.PI / numberOfChildren); // Calculate angle
            float x = Mathf.Cos(angle) * radius; // Calculate x position
            float z = Mathf.Sin(angle) * radius; // Calculate z position

            Transform child = transform.GetChild(i); // Get child object
            Vector3 newPosition = new Vector3(x, child.position.y, z); // Create new position vector
            child.position = newPosition; // Set new position
            child.rotation = Quaternion.Euler(0, i * -10, 0);
        }
    }
}
