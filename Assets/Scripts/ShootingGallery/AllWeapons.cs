using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AllWeapons", menuName = "ScriptableObjects/AllItems/AllWeapons")]
public class AllWeapons : AllItems
{

    [SerializeField] private List<Weapon> weapons;
    public List<Weapon> Weapons => weapons;
}