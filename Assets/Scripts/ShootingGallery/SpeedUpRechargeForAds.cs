using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SpeedUpRechargeForAds : AdVideoRevarder
{
    [SerializeField] private float speedUp;
    [SerializeField] private float speedUpTime;
    [SerializeField] private Button speedUpButton;
    [SerializeField] private Image timer;
    [SerializeField] private Image timerBackground;

    protected override void GiveReward()
    {
        StartCoroutine(SpeedingUpRechare());
    }

    private IEnumerator SpeedingUpRechare()
    {
        speedUpButton.gameObject.SetActive(false);
        timer.gameObject.SetActive(true);
        timerBackground.gameObject.SetActive(true);
        Recharger.instance.SpeedUp = speedUp;
        for (float i = 0; i < speedUpTime; i += Time.deltaTime)
        {
            timer.fillAmount = i / speedUpTime;
            yield return null;
        }
        speedUpButton.gameObject.SetActive(true);
        timer.gameObject.SetActive(false);
        timerBackground.gameObject.SetActive(false);
        Recharger.instance.SpeedUp = 1f;
        yield break;
    }
}