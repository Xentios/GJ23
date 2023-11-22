
public class PickColor : Pickers
{
  
    private void Start()
    {
        image.color = shopRequest.Color;
        textField.text = shopRequest.ColorName;
        image.fillAmount = shopRequest.ColorPercentage;
    }
}
