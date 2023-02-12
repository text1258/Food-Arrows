namespace UI
{
    public class AddMoneyForAdvertisingText : ResourceText
    {
        public override void ShowText()
        {
            text.text = $"+ {Player.Instance.CurrentLevel.MoneyForAdvertising} денег (за рекламу)";
        }
    }
}