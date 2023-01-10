using UnityEngine;
using UnityEngine.UI;

public class SelectWeapon : MonoBehaviour
{
    [SerializeField] private RechargeViewer rechargeViewer;
    [SerializeField] private Button restoreRechargeToFullButton;
    [HideInInspector] private Weapon selectedWeapon;
    [HideInInspector] public GameObject currentWeapon;

    public Weapon SelectedWeapon
    {
        get => selectedWeapon;
        set
        {
            selectedWeapon = value;
            if (currentWeapon != null)
            {
                Destroy(currentWeapon);
            }
            currentWeapon = Instantiate(selectedWeapon.WeaponPrefab, this.transform.position, this.transform.rotation, parent: this.transform);
            currentWeapon.GetComponent<InstantiatedWeapon>().rechargeViewer = rechargeViewer;
            rechargeViewer.CurrentInstantiatedWeapon = currentWeapon.GetComponent<InstantiatedWeapon>();
            rechargeViewer.gameObject.SetActive(true);
            restoreRechargeToFullButton.gameObject.SetActive(true);
            rechargeViewer.CreateMissilesPanel();
            currentWeapon.GetComponent<InstantiatedWeapon>().CurrentMissileCount = 0;
        }
    }

    public void RestoreRechargeToFull()
    {
        currentWeapon.GetComponent<InstantiatedWeapon>().CurrentMissileCount = currentWeapon.GetComponent<InstantiatedWeapon>().weapon.MissileCount;
        currentWeapon.GetComponent<InstantiatedWeapon>().currentRestoreRecharge = null;
    }
}