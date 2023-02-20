using UnityEngine;

public class CannonMissile : Missile
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Target>() != null)
        {
            collision.gameObject.GetComponent<Target>().Stability -= stabilityDamage;
        }
        DestroyMissle();
    }
}