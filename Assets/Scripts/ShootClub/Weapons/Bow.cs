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

    protected override void OnMouseInputUp()
    {
        if (isRuturning == false)
        {
            Strike();
        }
    }

    protected override void OnMouseInputDown()
    {
        if (isRuturning == false)
        {
            startBowStringOriginPosition = bowStringOrigin.transform.localPosition;
            arrow = Instantiate(missilePrefab, bowStringOrigin.transform.transform.position, missilePrefab.transform.rotation, parent: shotingPart.transform);
        }
    }

    protected override void OnMouseInput()
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

    protected override void OnStrike()
    {
        arrow.transform.SetParent(null);
        arrow.GetComponent<Rigidbody>().AddForce(arrow.transform.forward * maxPullbackForce * pullback);
        pullback = 0f;
        StartCoroutine(RuturnBowString());
    }

    private IEnumerator RuturnBowString()
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
        yield break;
    }
}