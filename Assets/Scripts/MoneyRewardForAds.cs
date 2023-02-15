public class MoneyRewardForAds : RewardAdsCaller
{
    protected override void Reward()
    {
        Player.Instance.AddMoneyForAdversiting();
    }
}