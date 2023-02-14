using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/Items/Weapon")]
public class Weapon : Item
{
    [SerializeField] private GameObject weaponPrefab;
    [SerializeField] private uint missileCount;
    [SerializeField] private Sprite missileSprite;
    [SerializeField] private float cooldown;
    [SerializeField] private float missileRechargeTime;
    public GameObject WeaponPrefab => weaponPrefab;
    public uint MissileCount => missileCount;
    public Sprite MissileSprite => missileSprite;
    public float Cooldown => cooldown;
    public float MissileRechargeTime => missileRechargeTime;

}