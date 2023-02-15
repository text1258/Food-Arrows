using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;

public class AdsShower : MonoBehaviour
{
    public static AdsShower Instance;

    private bool isMinutePassed = true;
    private UnityAction getReward;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
    }

    private void Start()
    {
    }

    public void GetReward()
    {
        getReward.Invoke();
    }

    public void CallVideoAd(UnityAction getReward)
    {
        this.getReward = getReward;
    }

    private IEnumerator MinuteAdsTimer()
    {
        isMinutePassed = false;
        yield return new WaitForSeconds(60);
        isMinutePassed = true;
        yield break;
    }
}
