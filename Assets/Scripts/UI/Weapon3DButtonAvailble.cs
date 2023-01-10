using UnityEngine;

public class Weapon3DButtonAvailble : Weapon3DButton
{
    [HideInInspector] public SelectWeapon selectWeapon;
    
    public override void OnItemCellClick()
    {
        selectWeapon.SelectedWeapon = cellWeapon;
    }
    
}