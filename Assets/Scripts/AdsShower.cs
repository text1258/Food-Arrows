using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;

public class AdsShower : MonoBehaviour
{
    public static AdsShower instance;

    private bool isMinutePassed = true;
    private UnityAction getReward;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
            if (isMinutePassed == true)
            {
#if UNITY_EDITOR || UNITY_ANDROID
                Debug.Log("FullScreenAds");
#else
                //ShowFullScreenAdv();
#endif
                StartCoroutine(MinuteAdsTimer());
            }
        }
    }

    //[DllImport("__Internal")]
    //private static extern void ShowFullScreenAdv();

    //[DllImport("__Internal")]
    //private static extern void ShowRewardedVideo();

    public void GetReward()
    {
        getReward.Invoke();
    }

    public void CallVideoAd(UnityAction getReward)
    {
#if UNITY_EDITOR || UNITY_ANDROID
        getReward.Invoke();
#else
        this.getReward = getReward;
        //ShowRewardedVideo();
#endif
    }

    private IEnumerator MinuteAdsTimer()
    {
        isMinutePassed = false;
        yield return new WaitForSeconds(60);
        isMinutePassed = true;
        yield break;
    }
}