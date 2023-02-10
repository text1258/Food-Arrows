public class MoneyText : ResourceText
{
    public override void ShowText()
    {
        text.text = $"Деньги: {Player.Instance.Money}";
    }
}