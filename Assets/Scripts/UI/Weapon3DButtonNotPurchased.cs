using UI;

public class Weapon3DButtonNotPurchased : Weapon3DButton
{
    public override void OnItemCellClick()
    {
        ConfirmPanel.instance.CreateConfirmPanel($"Подтвердите покупку. Это будет стоить {CellWeapon.Price}",
            CellWeapon.Sprite, onConfirm: BuyThisWeapon);
    }

    private void BuyThisWeapon()
    {
        if (Player.instance.Money >= CellWeapon.Price)
        {
            Player.instance.InventoryWeapons.Add(CellWeapon);
            Player.instance.Money -= CellWeapon.Price;
            Saver.instance.Save();
            Destroy(gameObject);
        }
        else
        {
            MessageText.instance.Message("У вас недостаточно денег(", 2f);
        }
    }
}