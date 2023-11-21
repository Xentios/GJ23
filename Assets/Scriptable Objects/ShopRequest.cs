using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ShopRequests/ShopRequest")]
public class ShopRequest : ScriptableObject
{
    [SerializeField]
    public int ShapeID;
    [SerializeField]
    public string ShapeName;
    [SerializeField]
    public Color Color;
    [SerializeField]
    public string ColorName;
    [Range(0.001f, 1)]
    [SerializeField]
    public float PressScale;
    [SerializeField]
    public int SpikeTypeID;
    [SerializeField]
    public int SpikeCount;
}
