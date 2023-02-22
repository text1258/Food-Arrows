using UnityEngine;
using UI;

public class ProductShopCell : ItemsPannelCell
{
    [HideInInspector] public Product cellProduct;

    public override void OnItemCellClick()
    {
        ConfirmPanel.instance.CreateConfirmPanel($"Подтвердите покупку. Это будет стоить {cellProduct.Price}",
            cellProduct.Sprite, onConfirm: BuyThisProduct);
    }

    private void BuyThisProduct()
    {
        ProductShop.instance.BuyProduct(cellProduct);
    }
}