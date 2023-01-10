using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Missile : MonoBehaviour
{
    [SerializeField] private float timeBeforeDestroy;
    
    private void OnCollisionEnter(Collision collision)
    {
        Target collisionTarget = collision.gameObject.GetComponent<Target>();
        if (collisionTarget != null)
        {
            collisionTarget.notHitByMissile = false;
            collision.gameObject.GetComponent<Collider>().enabled = false;
            collision.gameObject.GetComponent<MeshRenderer>().enabled = false;
            foreach (Transform child in collision.transform)
            {
                child.gameObject.SetActive(true);
            }
            collisionTarget.targetSpawner.SpawnTarget();
            Destroy(collision.gameObject, collision.gameObject.GetComponent<Target>().TimeBeforeDestroy);
        }
        Destroy(this.gameObject, timeBeforeDestroy);
    }
}