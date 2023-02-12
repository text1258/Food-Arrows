public class Weapon3DButtonAvailble : Weapon3DButton
{
    public override void OnItemCellClick()
    {
        SelectWeapon.Instance.SelectedWeapon = CellWeapon;
    }
}