using UnityEngine;

public class Arrow : Missile
{
    [SerializeField] private float timeBeforeDestroy;

    private void OnCollisionEnter(Collision collision)
    {
        GetComponent<Rigidbody>().useGravity = true;
        Destroy(gameObject, timeBeforeDestroy);
    }
}