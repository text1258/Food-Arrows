using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;

public class AdvertisementShower : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void ShowFullscreenAdv();

    [DllImport("__Internal")]
    private static extern void ShowRewardedVideo();

    [SerializeField] private UnityEvent onRewardMoneyVideoClose;
    [SerializeField] private UnityEvent onRewardVideoMissilesClose;
    [HideInInspector] private UnityEvent onRewardVideoClose;


    private void Start()
    {
        CallFullscreenAdv();
    }

    public void GetRewardForVideo()
    {
        onRewardVideoClose.Invoke();
    }

    public void CallFullscreenAdv()
    {
        ShowFullscreenAdv();
    }

    public void CallRewardedMoneyVideoAdv()
    {
        onRewardVideoClose = onRewardMoneyVideoClose;
        ShowRewardedVideo();
    }

    public void CallRewardedMissilesVideoAdv()
    {
        onRewardVideoClose = onRewardVideoMissilesClose;
        ShowRewardedVideo();
    }
}