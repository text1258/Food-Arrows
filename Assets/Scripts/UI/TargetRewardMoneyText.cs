public class TargetRewardMoneyText : ResourceText
{
    public override void ShowText()
    {
        try
        {
            text.text = $"Награда: {TargetSpawner.Instance.CurrentTarget.RewardMoney}";
        }
        catch { }
    }
}