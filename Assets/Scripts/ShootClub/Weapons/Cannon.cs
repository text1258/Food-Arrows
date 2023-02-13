using UnityEngine;

public class Cannon : InstantiatedWeapon
{
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private float shotForce;
    [SerializeField] private Animator cannonAnimator;

    protected override void OnMouseInputUp()
    {
        cannonAnimator.SetTrigger("isStriking");
    }

    protected override void OnStrike()
    {
        GameObject missile = Instantiate(missilePrefab, bulletSpawnPoint.position, shotingPart.transform.rotation);
        missile.GetComponent<Rigidbody>().AddForce(missile.transform.forward * shotForce);
    }
}