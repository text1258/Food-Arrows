public class MoneyText : ResourceText
{
    public override void ShowText()
    {
        text.text = $"{phrase}{Player.Instance.Money}";
    }
}