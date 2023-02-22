using UnityEngine;

[RequireComponent (typeof(RectTransform))]
public class Weapon3DButtonAvailble : Weapon3DButton
{
    private void Reset()
    {
        GetComponent<RectTransform>().sizeDelta = Vector3.zero;
    }

    private void OnValidate()
    {
        GetComponent<RectTransform>().sizeDelta = Vector3.zero;
    }

    public override void OnItemCellClick()
    {
        SelectWeapon.instance.SelectedWeapon = CellWeapon;
    }
}