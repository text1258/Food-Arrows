using System.Collections;
using UnityEngine;

public abstract class InstantiatedWeapon : MonoBehaviour
{
    public static InstantiatedWeapon Instance;

    [SerializeField] public Weapon weapon;
    [SerializeField] public Vector3 SpawnPos;
    [SerializeField] protected Animator weaponAnimator;
    [SerializeField] protected GameObject missilePrefab;
    [SerializeField] protected Sprite missileSprite;
    [SerializeField] protected float cooldown;
    [SerializeField] protected float missileRechargeTime;
    [HideInInspector] private uint currentMissileCount;
    [HideInInspector] protected float pastCooldown;
    [HideInInspector] private float pastMissileRechargeTime;

    private Coroutine currentRestoreRecharge;

    public uint CurrentMissileCount
    {
        get => currentMissileCount;
        set
        {
            currentMissileCount = value;
            if (currentRestoreRecharge == null)
            {
                currentRestoreRecharge = StartCoroutine(RestoreRecharge());
            }
            Recharge.Instance.UpdateMissilesPanel();
        }
    }
    public Weapon Weapon => weapon;
    public float MissileRechargeTime => missileRechargeTime;

    public Sprite MissileSprite => missileSprite;

    private void Awake()
    {
        Instance = this;
        CurrentMissileCount = 0;
        Recharge.Instance.CreateMissilesPanel();
    }
    public float PastMissileRechargeTime
    {
        get => pastMissileRechargeTime;
        private set => pastMissileRechargeTime = value;
    }

    private IEnumerator RestoreRecharge()
    {
        while (CurrentMissileCount < weapon.MissileCount)
        {
            while (PastMissileRechargeTime < MissileRechargeTime)
            {
                if (CurrentMissileCount == weapon.MissileCount)
                {
                    break;
                }
                PastMissileRechargeTime += Time.deltaTime;
                Recharge.Instance.UpdateMissilesPanel();
                yield return null;
            }
            PastMissileRechargeTime = 0f;
            if (CurrentMissileCount < weapon.MissileCount)
            {
                CurrentMissileCount += 1;
            }
            else
            {
                break;
            }
        }
        currentRestoreRecharge = null;
        yield break;
    }
}