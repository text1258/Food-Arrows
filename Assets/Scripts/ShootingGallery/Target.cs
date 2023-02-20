using UnityEngine;
using Random = UnityEngine.Random;

public class Target : MonoBehaviour
{
    [SerializeField] private Vector3 targetMaxSpawnZoneSize;
    [SerializeField] private Vector3 targetMinSpawnZoneSize;
    [SerializeField] private Vector3 maxTargetAngels;
    [SerializeField] private uint rewardMoney;
    [SerializeField] private float timeBeforeDestroy;
    [HideInInspector] public bool notHitByMissile = true;
    private sbyte stability = 127;

    public uint RewardMoney => rewardMoney;

    public sbyte Stability
    {
        get => stability;
        set
        {
            stability = value;
            if (Stability <= 0)
            {
                DestroyTarget();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 255, 0, 0.5f);
        Gizmos.DrawCube((targetMaxSpawnZoneSize + targetMinSpawnZoneSize) / 2, targetMaxSpawnZoneSize - targetMinSpawnZoneSize);
    }

    private void Awake()
    {
        transform.rotation = Quaternion.Euler(Random.Range(-maxTargetAngels.x, maxTargetAngels.x), Random.Range(-maxTargetAngels.y, maxTargetAngels.y), Random.Range(-maxTargetAngels.z, maxTargetAngels.z));
        transform.position = RandomVector(targetMaxSpawnZoneSize, targetMinSpawnZoneSize);
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
