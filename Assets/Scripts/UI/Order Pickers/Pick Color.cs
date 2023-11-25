
public class PickColor : Pickers
{
  
    private void OnEnable()
    {
        image.color = shopRequest.Color;
        textField.text = shopRequest.ColorName;
        image.fillAmount = shopRequest.ColorPercentage;
    }
}
