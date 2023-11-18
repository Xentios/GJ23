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
    [SerializeField]
    public int SpikeTypeID;
    [SerializeField]
    public int SpikeCount;
}
