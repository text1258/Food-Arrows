using UnityEngine;

public abstract class RewardAdsCaller : MonoBehaviour
{
    public void ShowVideoAd()
    {
        AdsShower.instance.CallVideoAd(Reward);
    }

    protected virtual void Reward() { }
}