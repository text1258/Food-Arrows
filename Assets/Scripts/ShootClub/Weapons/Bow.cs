using System.Collections;
using UnityEngine;

public class Bow : InstantiatedWeapon
{
    [SerializeField] private Vector3 maxPullbackPosition;
    [SerializeField] private float maxPullbackForce;
    [SerializeField] private float bowStringPullbackTime;
    [SerializeField] private float bowStringReturnTime;
    [SerializeField] private GameObject bowStringOrigin;

    private float pullback;
    private Vector3 startBowStringOriginPosition;
    private GameObject arrow;
    private bool isRuturning = false;

    protected override void OnClcicknputDown()
    {
        if (isRuturning == false)
        {
            startBowStringOriginPosition = bowStringOrigin.transform.localPosition;
            arrow = Instantiate(missilePrefab, parent: bowStringOrigin.transform);
            arrow.GetComponent<Rigidbody>().useGravity = false;
        }
    }

    protected override void OnClickInput()
    {
        if (isRuturning == false)
        {
            if (pullback < 1)
            {
                pullback += Time.deltaTime / bowStringPullbackTime;
            }
            bowStringOrigin.transform.localPosition = Vector3.Lerp(startBowStringOriginPosition, maxPullbackPosition, pullback);
        }
    }

    protected override void OnClickInputUp()
    {
        if (isRuturning == false)
        {
            Strike();
        }
    }

    protected override void OnStrike()
    {
        arrow.transform.SetParent(null);
        arrow.GetComponent<Rigidbody>().AddForce(arrow.transform.forward * maxPullbackForce * pullback);
        pullback = 0f;
        StartCoroutine(RuturnBowStringPosition());
    }

    private IEnumerator RuturnBowStringPosition()
    {
        isRuturning = true;
        while (pullback < 1) 
        {
            pullback += Time.deltaTime / bowStringReturnTime;
            bowStringOrigin.transform.localPosition = Vector3.Lerp(startBowStringOriginPosition, maxPullbackPosition, 1 - pullback);
            yield return null;
        }
        bowStringOrigin.transform.localPosition = startBowStringOriginPosition;
        pullback = 0f;
        isRuturning = false;
        arrow = null;
        yield break;
    }
}