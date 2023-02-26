using System.Collections;
using UnityEngine;

public class Bow : InstantiatedWeapon
{
    [SerializeField] private Vector3 maxPullbackPosition;
    [SerializeField] private float pullbackForce;
    [SerializeField] private float pullbackTime;
    [SerializeField] private float returnTime;
    [SerializeField] private GameObject bowStringOrigin;

    private float pullback;
    private Vector3 startBowStringOriginPosition;
    private GameObject arrow;

    protected override void OnClicknputDown()
    {
        startBowStringOriginPosition = bowStringOrigin.transform.localPosition;
        arrow = Instantiate(missilePrefab, parent: bowStringOrigin.transform);
        arrow.GetComponent<Rigidbody>().useGravity = false;
    }

    protected override void OnClickInput()
    {
        if (pullback < 1)
        {
            pullback += Time.deltaTime / pullbackTime;
        }
        bowStringOrigin.transform.localPosition = Vector3.Lerp(startBowStringOriginPosition, maxPullbackPosition, pullback);
    }

    protected override void OnClickInputUp()
    {
        Strike();
    }

    protected override void OnStrike()
    {
        if (pullback < 0.95f)
        {
            Destroy(arrow);
        }
        else
        {
            arrow.transform.SetParent(null);
            arrow.GetComponent<Rigidbody>().AddForce(arrow.transform.forward * pullbackForce);
        }
        arrow = null;
        pullback = 0f;
        StartCoroutine(RuturnBowStringPosition());
    }

    private IEnumerator RuturnBowStringPosition()
    {
        while (pullback < 1) 
        {
            pullback += Time.deltaTime / returnTime;
            bowStringOrigin.transform.localPosition = Vector3.Lerp(startBowStringOriginPosition, maxPullbackPosition, 1 - pullback);
            yield return null;
        }
        bowStringOrigin.transform.localPosition = startBowStringOriginPosition;
        pullback = 0f;
        yield break;
    }
}