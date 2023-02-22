using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using Random = UnityEngine.Random;

public class TargetSpawner : MonoBehaviour
{
    public static TargetSpawner instance;

    [SerializeField] private float timeBetweenSpawn;

    private List<Target> currentLevelTargets;
    private Target currentTarget;

    public Target CurrentTarget => currentTarget;

    private void Awake()
    {
        instance = this;
        currentLevelTargets = Player.instance.CurrentLevel.SpawnAtThisLevelTargets;
        StartCoroutine(SpawnTarget());
    }

    public IEnumerator SpawnTarget()
    {
        yield return new WaitForSeconds(timeBetweenSpawn);
        currentTarget = Instantiate(currentLevelTargets[Random.Range(0, currentLevelTargets.Count - 1)]);
        PlayerStates.instance.UpdateAllStatesUI();
        yield break;
    }
}