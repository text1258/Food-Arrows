using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SpeedUpRechatgeForAds : RewardAdsCaller
{
    [SerializeField] private float speedUp;
    [SerializeField] private float speedUpTime;
    [SerializeField] private Button speedUpButton;
    [SerializeField] private Image timer;
    [SerializeField] private Image timerBackground;
    protected override void Reward()
    {
        StartCoroutine(SpeedUpRecharge());
    }

    private IEnumerator SpeedUpRecharge()
    {
        speedUpButton.gameObject.SetActive(false);
        timer.gameObject.SetActive(true);
        timerBackground.gameObject.SetActive(true);
        Recharger.Instance.SpeedUp = speedUp;
        for (float i = 0; i < speedUpTime; i += Time.deltaTime)
        {
            timer.fillAmount = i / speedUpTime;
            yield return null;
        }
        speedUpButton.gameObject.SetActive(true);
        timer.gameObject.SetActive(false);
        timerBackground.gameObject.SetActive(false);
        Recharger.Instance.SpeedUp = 1f;
        yield break;
    }
}