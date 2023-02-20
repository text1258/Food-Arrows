using System.Collections;
using UnityEngine;

public class Arrow : Missile
{
    [SerializeField] private float jammingLength;
    private bool wasCollised = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (wasCollised == false)
        {
            GetComponent<Rigidbody>().useGravity = true;
            wasCollised = true;
            if (collision.gameObject.GetComponent<Target>() != null)
            {
                collision.gameObject.GetComponent<Target>().Stability -= stabilityDamage;
                GetComponent<Rigidbody>().isKinematic = true;
                transform.SetParent(collision.transform);
                transform.position += Vector3.forward * jammingLength;
                StartCoroutine(WaitingArrowDestroy(collision.gameObject.GetComponent<Target>()));
            }
            else
            {
                DestroyMissle();
            }
        }
    }

    private IEnumerator WaitingArrowDestroy(Target collisionTarget)
    {
        yield return new WaitUntil(() => collisionTarget.notHitByMissile == false);
        transform.SetParent(null);
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().useGravity = true;
        DestroyMissle();
    }
}