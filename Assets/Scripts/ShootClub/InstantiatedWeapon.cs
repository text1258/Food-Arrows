using System.Collections;
using UnityEngine;

public abstract class InstantiatedWeapon : MonoBehaviour
{
    [SerializeField] public Weapon weapon;
    [SerializeField] protected Animator weaponAnimator;
    [SerializeField] protected GameObject missilePrefab;
    [SerializeField] protected Sprite missileSprite;
    [SerializeField] protected float cooldown;
    [SerializeField] protected float missileRechargeTime;
    [HideInInspector] public RechargeViewer rechargeViewer;
    [HideInInspector] private uint currentMissileCount;
    [HideInInspector] protected float pastCooldown;
    [HideInInspector] public Coroutine currentRestoreRecharge;
    [HideInInspector] private float pastMissileRechargeTime;

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
            if (rechargeViewer != null)
            {
                rechargeViewer.UpdateMissilesPanel();
            }
        }
    }
    public Weapon Weapon => weapon;
    public float MissileRechargeTime => missileRechargeTime;

    public Sprite MissileSprite => missileSprite;

    private void Awake()
    {
        CurrentMissileCount = weapon.MissileCount;
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
                PastMissileRechargeTime += Time.deltaTime;
                rechargeViewer.UpdateMissilesPanel();
                yield return null;
            }
            PastMissileRechargeTime = 0f;
            CurrentMissileCount += 1;
        }
        currentRestoreRecharge = null;
        yield break;
    }
}