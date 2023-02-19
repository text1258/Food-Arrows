using UnityEngine;

public class CannonMissile : Missile
{
    [SerializeField] private float timeBeforeDestroy;

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject, timeBeforeDestroy);
    }
}