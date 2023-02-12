using UI;

public class Weapon3DButtonNotPurchased : Weapon3DButton
{
    public override void OnItemCellClick()
    {
        ConfirmPanel.Instance.CreateConfirmPanel($"Подтвердите покупку. Это будет стоить {CellWeapon.Price}",
            CellWeapon.Sprite, onConfirm: BuyThisWeapon);
    }

    private void BuyThisWeapon()
    {
        if (Player.Instance.Money >= CellWeapon.Price)
        {
            Player.Instance.InventoryWeapons.Add(CellWeapon);
            Player.Instance.Money -= CellWeapon.Price;
            Saver.Instance.Save();
            Destroy(gameObject);
        }
        else
        {
            MessageText.Instance.Message("У вас недостаточно денег(", 2f);
        }
    }
}