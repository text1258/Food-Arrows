using System.Collections.Generic;
using UI;
using UnityEngine;
using Random = UnityEngine.Random;

public class TargetSpawner : MonoBehaviour
{
    public static TargetSpawner Instance;

    private List<Target> currentLevelTargets;
    private Target currentTarget;

    public Target CurrentTarget => currentTarget;

    private void Awake()
    {
        Instance = this;
        currentLevelTargets = Player.Instance.CurrentLevel.SpawnAtThisLevelTargets;
        SpawnTarget();
    }

    public void SpawnTarget()
    {
        currentTarget = Instantiate(currentLevelTargets[Random.Range(0, currentLevelTargets.Count - 1)]);
        PlayerStates.Instance.UpdateAllStatesUI();
    }
}