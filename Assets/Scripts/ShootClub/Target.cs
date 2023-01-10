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
    [HideInInspector] public Player player;
    [HideInInspector] public TargetSpawner targetSpawner;
    [HideInInspector] public bool notHitByMissile = true;
    
    public Vector3 TargetMaxSpawnZoneSize => targetMaxSpawnZoneSize;
    public Vector3 TargetMinSpawnZoneSize => targetMinSpawnZoneSize;
    public Vector3 MaxTargetAngels => maxTargetAngels;
    public bool IsChangeVisibility => isChangeVisibility;
    public bool IsMoving => isMoving;
    public uint RewardMoney => rewardMoney;
    public float TimeBeforeDestroy => timeBeforeDestroy;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 255, 0, 0.5f);
        Gizmos.DrawCube((targetMaxSpawnZoneSize + targetMinSpawnZoneSize) / 2, targetMaxSpawnZoneSize - targetMinSpawnZoneSize);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Missile>() != null)
        {
            player.Money += rewardMoney;
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
        Vector3 direction = RandomVector(TargetMaxSpawnZoneSize, TargetMinSpawnZoneSize);
        while (notHitByMissile)
        {
            transform.position = Vector3.Lerp(transform.position, direction, Time.deltaTime * speed);
            if (Vector3.Distance(transform.position, direction) <= 0.1f)
            {
                direction = RandomVector(TargetMaxSpawnZoneSize, TargetMinSpawnZoneSize);
            }
            yield return null;
        }
        yield break;
    }

    private static Vector3 RandomVector(Vector3 a, Vector3 b)
    {
        return new Vector3(Random.Range(a.x, b.x), Random.Range(a.y, b.y), Random.Range(a.z, b.z));
    }
}
