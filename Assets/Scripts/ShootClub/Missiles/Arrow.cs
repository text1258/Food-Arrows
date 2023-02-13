using UnityEngine;

public class Arrow : Missile
{
    [SerializeField] private float timeBeforeDestroy;

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject, timeBeforeDestroy);
    }
}