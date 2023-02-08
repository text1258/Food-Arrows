namespace UI
{
    public class AddMoneyForAdvertisingButton : ResourceText
    {
        public override void ShowText()
        {
            text.text = $"+ {Player.Instance.CurrentLevel.MoneyForAdvertisingCount}{phrase}";
        }
    }
}