using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Missile : MonoBehaviour
{
    [SerializeField] private float timeBeforeDestroy;
    [SerializeField] protected sbyte stabilityDamage;

    public void DestroyMissle()
    {
        Destroy(gameObject, timeBeforeDestroy);
    }
}