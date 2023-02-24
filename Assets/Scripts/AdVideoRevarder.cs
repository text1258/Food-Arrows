using UnityEngine;
using YG;

public abstract class AdVideoRevarder : MonoBehaviour
{
    [SerializeField] private YandexGame yandexGameSDK;

    public void ShowAdVideo()
    {
        yandexGameSDK.RewardVideoAd.AddListener(Reward);
        yandexGameSDK.ErrorVideoAd.AddListener(ClearReward);
        YandexGame.RewVideoShow(1);
    }

    private void Reward()
    {
        GiveReward();
        ClearReward();
    }

    private void ClearReward()
    {
        yandexGameSDK.RewardVideoAd.RemoveAllListeners();
        yandexGameSDK.ErrorVideoAd.RemoveAllListeners();
    }

    protected virtual void GiveReward() { }
}
