public class TargetRewardMoneyText : ResourceText
{
    public void ShowText(string value)
    {
        text.text = $"{phrase}{value}";
    }
}