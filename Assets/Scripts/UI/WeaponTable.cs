using UnityEngine;
using UnityEngine.UI;

public class WeaponTable : ItemsPannel
{
    [SerializeField] private ScrollRect weaponTable;
    [SerializeField] private float weaponSize;
    [SerializeField] private GameObject notPurchasedWeaponBarrierPrefab;
    [SerializeField] private GameObject notAvailableWeaponBarrierPrefab;
    [SerializeField] private SelectWeapon selectWeapon;
    [SerializeField] private AllWeapons allWeapons;

    public override void CreateItemsPanel()
    {
        foreach (Weapon weapon in allWeapons.Weapons)
        {
            AddWeapon(weapon);
        }
    }
    
    private void AddWeapon(Weapon weapon)
    {
        GameObject currentWeapon = Instantiate(weapon.ItemPrefab, parent: weaponTable.content);
        currentWeapon.GetComponent<Weapon3DButton>().cellWeapon = weapon;
        currentWeapon.GetComponent<Weapon3DButtonAvailble>().selectWeapon = this.selectWeapon;
        currentWeapon.transform.localScale = Vector3.one;
        currentWeapon.transform.rotation = Quaternion.identity;
        Vector3 currentWeaponBoundsSize = currentWeapon.GetComponent<MeshRenderer>().bounds.size;
        float maxCurrentWeaponSize = Mathf.Max(currentWeaponBoundsSize.x, currentWeaponBoundsSize.y, currentWeaponBoundsSize.z);
        float ratioChangedCurrentWeaponSizeToNotChanged = weaponSize / maxCurrentWeaponSize;
        currentWeapon.transform.localScale *= ratioChangedCurrentWeaponSizeToNotChanged;
        currentWeapon.GetComponent<RectTransform>().localPosition = Vector3.forward * -weaponSize / 2;
        weaponTable.content.GetComponent<HorizontalLayoutGroup>().SetLayoutHorizontal();
        if (!Player.Instance.InventoryWeapons.Contains(weapon))
        {
            GameObject Barrier;
            if (Player.Instance.AvailableWeapons.Contains(weapon))
            {
                Barrier = Instantiate(notPurchasedWeaponBarrierPrefab, parent: currentWeapon.transform);
            }
            else
            {
                Barrier = Instantiate(notAvailableWeaponBarrierPrefab, parent: currentWeapon.transform);
            }
            Barrier.GetComponent<Weapon3DButton>().cellWeapon = weapon;
            Barrier.transform.localScale = currentWeapon.GetComponent<BoxCollider>().size;
            Barrier.transform.localPosition = currentWeapon.GetComponent<BoxCollider>().center;
        }
    }
}