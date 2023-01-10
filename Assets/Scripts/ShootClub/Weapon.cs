using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/Items/Weapon")]
public class Weapon : Item
{
    [SerializeField] private GameObject weaponPrefab;
    [SerializeField] private uint missileCount;
    public GameObject WeaponPrefab => weaponPrefab;
    public uint MissileCount => missileCount;
}