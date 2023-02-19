using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Target : MonoBehaviour
{
    [SerializeField] private Vector3 targetMaxSpawnZoneSize;
    [SerializeField] private Vector3 targetMinSpawnZoneSize;
    [SerializeField] private Vector3 maxTargetAngels;
    [SerializeField] private bool isChangeVisibility;
    [SerializeField] private float minInvisibilityTime;
    [SerializeField] private float maxInvisibilityTime;
    [SerializeField] private float minVisibleTime;
    [SerializeField] private float maxVisibleTime;
    [SerializeField] private bool isMoving;
    [SerializeField] private float speed;
    [SerializeField] private uint rewardMoney;
    [SerializeField] private float timeBeforeDestroy;
    [HideInInspector] public bool notHitByMissile = true;

    public uint RewardMoney => rewardMoney;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 255, 0, 0.5f);
        Gizmos.DrawCube((targetMaxSpawnZoneSize + targetMinSpawnZoneSize) / 2, targetMaxSpawnZoneSize - targetMinSpawnZoneSize);
    }

    private void Awake()
    {
        transform.rotation = Quaternion.Euler(Random.Range(-maxTargetAngels.x, maxTargetAngels.x), Random.Range(-maxTargetAngels.y, maxTargetAngels.y), Random.Range(-maxTargetAngels.z, maxTargetAngels.z));
        transform.position = RandomVector(targetMaxSpawnZoneSize, targetMinSpawnZoneSize);
        if (isChangeVisibility == true)
        {
            StartCoroutine(ChangeVisibility());
        }
        if (isMoving == true)
        {
            StartCoroutine(MoveToRandomPoint());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Missile>() != null)
        {
            DestroyTarget();
        }
    }

    public IEnumerator ChangeVisibility()
    {
        while (notHitByMissile)
        {
            GetComponent<MeshRenderer>().enabled = false;
            float pastTime = 0f;
            while (pastTime < Random.Range(maxInvisibilityTime, minInvisibilityTime))
            {
                pastTime += Time.deltaTime;
                yield return null;
            }
            if (notHitByMissile)
            {
                GetComponent<MeshRenderer>().enabled = true;
            }
            pastTime = 0f;
            while (pastTime < Random.Range(maxVisibleTime, minVisibleTime))
            {
                pastTime += Time.deltaTime;
                yield return null;
            }
        }
        GetComponent<MeshRenderer>().enabled = false;
        yield break;
    }

    public IEnumerator MoveToRandomPoint()
    {
        Vector3 direction = RandomVector(targetMaxSpawnZoneSize, targetMinSpawnZoneSize);
        while (notHitByMissile)
        {
            transform.position = Vector3.Lerp(transform.position, direction, Time.deltaTime * speed);
            if (Vector3.Distance(transform.position, direction) <= 0.1f)
            {
                direction = RandomVector(targetMaxSpawnZoneSize, targetMinSpawnZoneSize);
            }
            yield return null;
        }
        yield break;
    }

    public void DestroyTarget()
    {
        notHitByMissile = false;
        Player.Instance.Money += rewardMoney;
        GetComponent<Collider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
        StartCoroutine(TargetSpawner.Instance.SpawnTarget());
        Destroy(gameObject, timeBeforeDestroy);
    }

    private static Vector3 RandomVector(Vector3 a, Vector3 b)
    {
        return new Vector3(Random.Range(a.x, b.x), Random.Range(a.y, b.y), Random.Range(a.z, b.z));
    }
}
