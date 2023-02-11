using UI;

public class Weapon3DButtonNotPurchased : Weapon3DButton
{
    public override void OnItemCellClick()
    {
        ConfirmPanel.Instance.CreateConfirmPanel($"Подтвердите покупку. Это будет стоить {cellWeapon.Price}",
            cellWeapon.Picture, onConfirm: BuyThisWeapon);
    }

    private void BuyThisWeapon()
    {
        if (Player.Instance.Money >= cellWeapon.Price)
        {
            Player.Instance.InventoryWeapons.Add(cellWeapon);
            Player.Instance.Money -= cellWeapon.Price;
            Saver.Instance.Save();
            Destroy(gameObject);
        }
        else
        {
            MessageText.Instance.Message("У вас недостаточно денег(", 2f);
        }
    }
}