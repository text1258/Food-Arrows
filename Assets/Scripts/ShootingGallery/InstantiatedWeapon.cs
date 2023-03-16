using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class InstantiatedWeapon : MonoBehaviour
{
    public static InstantiatedWeapon instance;

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
            Recharger.instance.UpdateMissilesPanel();
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
        instance = this;
        CurrentMissileCount = 0;
        Recharger.instance.CreateMissilesPanel();
    }
    private void Start()
    {
        pastCooldown = weapon.Cooldown;
    }

    private void Update()
    {
        if (pastCooldown >= weapon.Cooldown && CurrentMissileCount > 0)
        {
            if (Input.GetMouseButtonDown(0) && IsPointerOverUIObject() == false)
            {
                OnClicknputDown();
                clicked = true;
            }
            if (Input.GetMouseButton(0) && clicked == true)
            {
                OnClickInput();
            }
            if (Input.GetMouseButtonUp(0) && clicked == true)
            {
                OnClickInputUp();
                clicked = false;
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
            if (hit.transform.gameObject.CompareTag("NonShootingPlace") == false && IsPointerOverUIObject() == false && hit.transform.GetComponent<Weapon3DButton>() == null)
            {
                shotingPart.transform.LookAt(hit.point);
            }
        }
    }

    protected virtual void OnClickInputUp() { }

    protected virtual void OnClicknputDown() { }

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
                PastMissileRechargeTime += Time.deltaTime * Recharger.instance.SpeedUp;
                Recharger.instance.UpdateMissilesPanel();
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