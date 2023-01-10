using System.Collections.Generic;
using UnityEngine;

public abstract class Level : ScriptableObject
{
    [SerializeField] protected uint number;
    [SerializeField] protected List<Food> openInThisLevelFoods;
    [SerializeField] protected List<Product> openInThisLevelProducts;
    [SerializeField] protected List<Weapon> openInThisLevelWeapons;
    [SerializeField] protected List<Target> spawnAtThisLevelTargets;
    [SerializeField] protected uint moneyForAdvertisingCount;

    public uint Number => number;
    public List<Food> OpenInThisLevelFoods => openInThisLevelFoods;
    public List<Product> OpenInThisLevelProducts => openInThisLevelProducts;
    public List<Weapon> OpenInThisLevelWeapons => openInThisLevelWeapons;
    public List<Target> SpawnAtThisLevelTargets => spawnAtThisLevelTargets;
    public uint MoneyForAdvertisingCount => moneyForAdvertisingCount;
}