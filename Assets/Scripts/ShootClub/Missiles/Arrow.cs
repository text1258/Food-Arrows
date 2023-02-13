using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float timeBeforeDestroy;

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject, timeBeforeDestroy);
    }
}