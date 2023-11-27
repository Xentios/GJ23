
public class PickColor : Pickers
{

    protected override void OnEnableActions()
    {
        image.color = shopRequest.Color;
        textField.text = shopRequest.ColorName;
        image.fillAmount = shopRequest.ColorPercentage;
    }
}
