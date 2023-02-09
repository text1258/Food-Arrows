using UI;

public class Weapon3DButtonNotPurchased : Weapon3DButton
{
    public override void OnItemCellClick()
    {
        ConfirmPanel.Instance.CreateConfirmPanel($"Are you sure to buy it? It wil be cost {cellWeapon.Price}",
            cellWeapon.Picture, onConfirm: BuyThisWeapon);
    }

    private void BuyThisWeapon()
    {
        if (Player.Instance.Money >= cellWeapon.Price)
        {
            Player.Instance.InventoryWeapons.Add(cellWeapon);
            Player.Instance.Money -= cellWeapon.Price;
            Saver.instance.Save();
            Destroy(gameObject);
        }
        else
        {
            MessageText.Instance.Message("You don't have enough money!", 2f);
        }
    }
}