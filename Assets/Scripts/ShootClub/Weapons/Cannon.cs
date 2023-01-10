using UnityEngine;
using UnityEngine.EventSystems;

public class Cannon : InstantiatedWeapon
{
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private float shotForce;
    private RaycastHit hit;

    private void Start()
    {
        pastCooldown = cooldown;
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0) && pastCooldown >= cooldown && CurrentMissileCount > 0 && !EventSystem.current.IsPointerOverGameObject())
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
            if (!hit.transform.gameObject.CompareTag("NonShootingPlace") && !EventSystem.current.IsPointerOverGameObject())
            {
                this.transform.LookAt(hit.point);
            }
        }
    }

    public void Strike()
    {
        GameObject missile = Instantiate(missilePrefab, bulletSpawnPoint.position, this.transform.rotation);
        missile.GetComponent<Rigidbody>().AddForce(missile.transform.forward * shotForce);
    }
}