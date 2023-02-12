using UnityEngine;
using UnityEngine.UI;

public class SelectWeapon : MonoBehaviour
{
    [SerializeField] private Button restoreRechargeToFullButton;

    private Weapon selectedWeapon;
    private GameObject currentWeapon;

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
            currentWeapon = Instantiate(selectedWeapon.WeaponPrefab, transform.position, transform.rotation, parent: transform);
            restoreRechargeToFullButton.gameObject.SetActive(true);
        }
    }
}