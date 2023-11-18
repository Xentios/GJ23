using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class SliderMidleFill : MonoBehaviour
{
   
    public float center = 0.5f;

    [SerializeField]
    private Slider slider;
    [SerializeField]
    private RectTransform sliderFill;
    [SerializeField]
    private Image sliderFillImage;
    [SerializeField]
    private ShopRequest shopRequest;
    [SerializeField]
    private Gradient colorGradient;

    [SerializeField]
    private RectTransform middle;

    public void OnEnable()
    {
        center = shopRequest.PressScale;
        //middle=
    }

    public void onValueChanged()
    {
       
        sliderFill.anchorMin = new Vector2(0,Mathf.Clamp(slider.handleRect.anchorMin.y, 0, center));
        sliderFill.anchorMax = new Vector2(1,Mathf.Clamp(slider.handleRect.anchorMin.y, center, 1));

        var topValue = slider.value - center;
        var bottomValue = Mathf.Sign(topValue) == 1 ? (1f - center) : (center - 0f);
        sliderFillImage.color = colorGradient.Evaluate(1- ((Mathf.Abs(topValue) / bottomValue)));
    }
}
