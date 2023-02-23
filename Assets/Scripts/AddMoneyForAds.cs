using UnityEngine;

public class AddMoneyForAds : AdVideoRevarder
{
    protected override void GiveReward()
    {
        Player.instance.AddMoneyForAdversiting();
    }
}