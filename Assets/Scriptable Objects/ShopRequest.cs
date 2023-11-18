using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ShopRequests/ShopRequest")]
public class ShopRequest : ScriptableObject
{
    [SerializeField]
    public int ShapeID;
    [SerializeField]
    public Color Color;
    [Range(0.001f,1)]
    [SerializeField]
    public float PressScale;
    [SerializeField]
    public int SpikeTypeID;
    [SerializeField]
    public int SpikeCount;
}
