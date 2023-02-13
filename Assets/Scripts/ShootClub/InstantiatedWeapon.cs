using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class InstantiatedWeapon : MonoBehaviour
{
    public static InstantiatedWeapon Instance;

    [SerializeField] public Weapon weapon;
    [SerializeField] public Vector3 SpawnPosition;
    [SerializeField] protected GameObject missilePrefab;
    [SerializeField] protected Sprite missileSprite;
    [SerializeField] protected float cooldown;
    [SerializeField] protected float missileRechargeTime;
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
    public float MissileRechargeTime => missileRechargeTime;

    public Sprite MissileSprite => missileSprite;

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
        pastCooldown = cooldown;
    }

    private void Update()
    {
        if (pastCooldown >= cooldown && CurrentMissileCount > 0 &&
#if UNITY_EDITOR
            !EventSystem.current.IsPointerOverGameObject())
#else
            !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
#endif
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
        if (pastCooldown < cooldown)
        {
            pastCooldown += Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100f))
        {
            if (!hit.transform.gameObject.CompareTag("NonShootingPlace") &&
#if UNITY_EDITOR
            !EventSystem.current.IsPointerOverGameObject())
#else
            !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
#endif
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