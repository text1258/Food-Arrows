using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class InstantiatedWeapon : MonoBehaviour
{
    public static InstantiatedWeapon Instance;

    [SerializeField] public Weapon weapon;
    [SerializeField] public Vector3 SpawnPosition;
    [SerializeField] protected GameObject missilePrefab;
    [SerializeField] protected GameObject shotingPart;
    [HideInInspector] private uint currentMissileCount;
    [HideInInspector] protected float pastCooldown;
    [HideInInspector] private float pastMissileRechargeTime;

    private RaycastHit hit;

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

    public float PastMissileRechargeTime
    {
        get => pastMissileRechargeTime;
        private set => pastMissileRechargeTime = value;
    }

    private void Awake()
    {
        Instance = this;
        CurrentMissileCount = 0;
        Recharge.Instance.CreateMissilesPanel();
    }
    private void Start()
    {
        pastCooldown = weapon.Cooldown;
    }

    private void Update()
    {
        if (pastCooldown >= weapon.Cooldown && CurrentMissileCount > 0 && IsUIPressed() == false)
        {
            if (Input.GetMouseButtonUp(0))
            {
                OnMouseInputUp();
            }
            if (Input.GetMouseButtonDown(0))
            {
                OnMouseInputDown();
            }
            if (Input.GetMouseButton(0))
            {
                OnMouseInput();
            }
        }
        if (pastCooldown < weapon.Cooldown)
        {
            pastCooldown += Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100f))
        {
            if (!hit.transform.gameObject.CompareTag("NonShootingPlace") && IsUIPressed() == false)
            {
                shotingPart.transform.LookAt(hit.point);
            }
        }
    }

    public void Strike()
    {
        pastCooldown = 0f;
        CurrentMissileCount -= 1;
        OnStrike();
    }

    protected virtual void OnMouseInputUp() { }

    protected virtual void OnMouseInputDown() { }

    protected virtual void OnMouseInput() { }

    protected virtual void OnStrike() { }

    private IEnumerator RestoreRecharge()
    {
        while (CurrentMissileCount < weapon.MissileCount)
        {
            while (PastMissileRechargeTime < Weapon.MissileRechargeTime)
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

    private static bool IsUIPressed()
    {
#if UNITY_EDITOR
        return EventSystem.current.IsPointerOverGameObject();
#else
        foreach (Touch touch in Input.touches)
        {
            if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
            {
                return true;
            }
        }
        return false;
#endif
    }
}