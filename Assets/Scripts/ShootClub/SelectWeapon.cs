using UnityEngine;
using UnityEngine.UI;

public class SelectWeapon : MonoBehaviour
{
    public static SelectWeapon Instance;

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
            currentWeapon = Instantiate(selectedWeapon.WeaponPrefab, selectedWeapon.WeaponPrefab.GetComponent<InstantiatedWeapon>().SpawnPosition, transform.rotation, parent: transform);
            restoreRechargeToFullButton.gameObject.SetActive(true);
        }
    }

    private void Awake()
    {
        Instance = this;
    }
}