public class RechargeToFullForAds : RewardAdsCaller
{
    protected override void Reward()
    {
        Recharge.Instance.RechargeToFull();
    }
}