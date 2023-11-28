using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ShopRequests/ShopRequest")]
public class ShopRequest : ScriptableObject
{
    private enum ShapeEnums
    {
        Circle,
        Hexagon,
        Triangle,
        Pangon,
    }

 
    
    private int[] ShapeSizeArray = new int[4] { 30, 20, 18, 10};



    [SerializeField]
    public int ShapeID;
    [SerializeField]
    public string ShapeName;
    [SerializeField]
    public Color Color;
    [Range(0.25f, 1)]
    [SerializeField]
    public float ColorPercentage;
    [SerializeField]
    public string ColorName;
    [Range(0.001f, 1)]
    [SerializeField]
    public float PressScale;
    [SerializeField]
    public int SpikeTypeID;
    [SerializeField]
    public int SpikeCount;

    [ContextMenu("Randomize Variables")]
    public void Randomize()
    {
        ShapeID = Random.Range(0, System.Enum.GetValues(typeof(ShapeEnums)).Length);        
        ShapeName = ((ShapeEnums) ShapeID).ToString();

        var coloursList = new List<Color>() { Color.red,  Color.green ,Color.blue,Color.yellow };
        var coloursListNames = new List<string>() { "RED","GREEN", "BLUE","YELLOW" };
        var colorIndex = Random.Range(0, coloursList.Count);
        Color = coloursList[colorIndex];
        ColorName = coloursListNames[colorIndex];
        var rangeAttribute=ReadRangeAttributeLimits("ColorPercentage");
        ColorPercentage = Random.Range(rangeAttribute.min, rangeAttribute.max);

        rangeAttribute = ReadRangeAttributeLimits("PressScale");
        PressScale = Random.Range(rangeAttribute.min, rangeAttribute.max);

        SpikeTypeID = 0;
        SpikeCount= Random.Range(1, 6);

    }

    private RangeAttribute ReadRangeAttributeLimits(string fieldName)
    {
        System.Type myType = this.GetType();
        System.Reflection.FieldInfo myField = myType.GetField(fieldName);
        RangeAttribute rangeAttribute = null;

        if (myField != null)
        {
            object[] attributes = myField.GetCustomAttributes(typeof(RangeAttribute), true);

            if (attributes.Length > 0)
            {
                rangeAttribute = (RangeAttribute) attributes[0];
                     
            }
        }

        return rangeAttribute;
    }

    public float GetWantedArea()
    {
        return (float) ShapeSizeArray[ShapeID];
    }
}
