using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ShopRequests/ShopResult")]
public class ShopResult : ScriptableObject
{
    [Header("Main Part")]

    [Tooltip("Damage Modifier")]
    [Range(0,100)]
    [SerializeField]
    public float CutPercentage;
    [Tooltip("HP Modifier")]
    [Range(0, 100)]
    [SerializeField]
    public float PressPercentage;

    //?? What should it modify?
    [Range(0, 100)]
    [SerializeField]
    public float PaintPercentage;

    [Tooltip("Thorns Dmg Modifier")]
    [Range(0, 100)]
    [SerializeField]
    public float SpikePercentage;

    //Armor Modifier maybe?
    [Range(0, 100)]
    [SerializeField]
    public float HammerPercentage;

    [Header("Elemental Damages")]
    //Fire Dmg
    [Tooltip("Fire Dmg")]
    [Range(0, 100)]
    [SerializeField]
    public float RedColorPercentage;
    [Tooltip("Ice Dmg")]
    [Range(0, 100)]
    [SerializeField]
    public float BlueColorPercentage;
    [Tooltip("Poison Dmg")]
    [Range(0, 100)]
    [SerializeField]
    public float GreenColorPercentage;

    //NEED MORE?
    //[SerializeField]
    //public float GreyColorPercentage;

}
