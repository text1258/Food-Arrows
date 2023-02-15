using UnityEngine;

public abstract class RewardAdsCaller : MonoBehaviour 
{
    public void ShowVideoAd()
    {
        AdsShower.Instance.CallVideoAd(Reward);
    }

    protected virtual void Reward() { }
}