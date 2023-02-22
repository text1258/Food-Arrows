namespace UI
{
    public class AddMoneyForAdvertisingText : ResourceText
    {
        public override void ShowText()
        {
            text.text = $"+{Player.instance.CurrentLevel.MoneyForAdvertising} монет";
        }
    }
}