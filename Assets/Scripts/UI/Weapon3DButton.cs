using UnityEngine;

public abstract class Weapon3DButton : ItemsPannelCell
{
    [HideInInspector] public Weapon CellWeapon;

    private void OnMouseUpAsButton()
    {
        OnItemCellClick();
    }
}