using UnityEngine;

public abstract class Weapon3DButton : ItemsPannelCell
{
    [HideInInspector] public Weapon cellWeapon;

    private void OnMouseUpAsButton()
    {
        OnItemCellClick();
    }
}