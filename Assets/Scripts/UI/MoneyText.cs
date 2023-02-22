public class MoneyText : ResourceText
{
    public override void ShowText()
    {
        text.text = $"Монеты: {Player.instance.Money}";
    }
}