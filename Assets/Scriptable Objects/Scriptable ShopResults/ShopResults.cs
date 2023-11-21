using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ShopRequests/ShopResult")]
public class ShopResult : ScriptableObject
{
    //Dmg Modifier
    [SerializeField]
    public float CutPercentage;
    //HP Modifier
    [SerializeField]
    public float PressPercentage;
    //?? What should it modify?
    [SerializeField]
    public float PaintPercentage;
    //Thorns Dmg Modifier
    [SerializeField]
    public float SpikePercentage;
    //Armor Modifier maybe?
    [SerializeField]
    public float HammerPercentage;

    //Fire Dmg
    [SerializeField]
    public float RedColorPercentage;
    //Ice Dmg
    [SerializeField]
    public float BlueColorPercentage;
    //Poision Dmg 
    [SerializeField]
    public float GreenColorPercentage;

    //NEED MORE?
    //[SerializeField]
    //public float GreyColorPercentage;

}
