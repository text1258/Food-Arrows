using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TargetSpawner : MonoBehaviour
{
    [SerializeField] private TargetRewardMoneyText targetRewardMoneyText;
    [HideInInspector] private List<Target> currentLevelTargets;
    [HideInInspector] public Target currentTarget;

    private void Awake()
    {
        currentLevelTargets = Player.Instance.CurrentLevel.SpawnAtThisLevelTargets;
        SpawnTarget();
    }

    public void SpawnTarget()
    {
        currentTarget = Instantiate(currentLevelTargets[Random.Range(0, currentLevelTargets.Count - 1)]);
        targetRewardMoneyText.ShowText(currentTarget.RewardMoney.ToString());
        currentTarget.targetSpawner = this;
        currentTarget.transform.rotation = Quaternion.Euler(Random.Range(-currentTarget.MaxTargetAngels.x, currentTarget.MaxTargetAngels.x), Random.Range(-currentTarget.MaxTargetAngels.y, currentTarget.MaxTargetAngels.y), Random.Range(-currentTarget.MaxTargetAngels.z, currentTarget.MaxTargetAngels.z));
        Vector3 maxSpawnZoneSize = currentTarget.TargetMaxSpawnZoneSize;
        Vector3 minSpawnZoneSize = currentTarget.TargetMinSpawnZoneSize;
        currentTarget.transform.position = new Vector3(Random.Range(minSpawnZoneSize.x, maxSpawnZoneSize.x), Random.Range(minSpawnZoneSize.y, maxSpawnZoneSize.y), Random.Range(minSpawnZoneSize.z, maxSpawnZoneSize.z));
        if (currentTarget.IsChangeVisibility)
        {
            StartCoroutine(currentTarget.ChangeVisibility());
        }
        if (currentTarget.IsMoving)
        {
            StartCoroutine(currentTarget.MoveToRandomPoint());
        }
    }
}