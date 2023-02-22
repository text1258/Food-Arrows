public class TargetRewardMoneyText : ResourceText
{
    public override void ShowText()
    {
        try
        {
            text.text = $"Награда: {TargetSpawner.instance.CurrentTarget.RewardMoney}";
        }
        catch { }
    }
}