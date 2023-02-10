public class TargetRewardMoneyText : ResourceText
{
    public void ShowText(string value)
    {
        text.text = $"Награда: {value}";
    }
}