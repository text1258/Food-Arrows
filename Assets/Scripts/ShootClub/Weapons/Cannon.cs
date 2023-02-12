using UnityEngine;
using UnityEngine.EventSystems;

public class Cannon : InstantiatedWeapon
{
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private float shotForce;
    [SerializeField] private GameObject cannon;

    private RaycastHit hit;

    private void Start()
    {
        pastCooldown = cooldown;
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0) && pastCooldown >= cooldown && CurrentMissileCount > 0 &&
#if UNITY_EDITOR
            !EventSystem.current.IsPointerOverGameObject())
#else
            !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)))
#endif
        {
            weaponAnimator.SetTrigger("isStriking");
            pastCooldown = 0f;
            CurrentMissileCount -= 1;
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
            !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)))
#endif
            {
                cannon.transform.LookAt(hit.point);
            }
        }
    }

    public void Strike()
    {
        GameObject missile = Instantiate(missilePrefab, bulletSpawnPoint.position, cannon.transform.rotation);
        missile.GetComponent<Rigidbody>().AddForce(missile.transform.forward * shotForce);
    }
}