using System.Collections;
using System.Collections.Generic;
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
    private bool clicked = false;
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
        if (Input.touchCount > 0)
        {
            if (pastCooldown >= weapon.Cooldown && CurrentMissileCount > 0)
            {
                switch (Input.GetTouch(0).phase)
                {
                    case TouchPhase.Began:
                        if (IsPointerOverUIObject() == false)
                        {
                            OnClcicknputDown();
                            clicked = true;
                        }
                        break;
                    case TouchPhase.Stationary:
                        if (clicked == true)
                        {
                            OnClickInput();
                        }
                        break;
                    case TouchPhase.Moved:
                        if (clicked == true)
                        {
                            OnClickInput();
                        }
                        break;
                    case TouchPhase.Ended:
                        if (clicked == true)
                        {
                            OnClickInputUp();
                            clicked = false;
                        }
                        break;
                }
            }
        }
        if (pastCooldown < weapon.Cooldown)
        {
            pastCooldown += Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if (Input.touchCount > 0)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            if (Physics.Raycast(ray, out hit, 100f))
            {
                if (!hit.transform.gameObject.CompareTag("NonShootingPlace") && IsPointerOverUIObject() == false)
                {
                    shotingPart.transform.LookAt(hit.point);
                }
            }
        }
    }

    protected virtual void OnClickInputUp() { }

    protected virtual void OnClcicknputDown() { }

    protected virtual void OnClickInput() { }

    protected virtual void OnStrike() { }

    public void Strike()
    {
        pastCooldown = 0f;
        CurrentMissileCount -= 1;
        OnStrike();
    }

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

    private static bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}