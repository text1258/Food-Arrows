using UnityEngine;
using UI;

public class ProductShopCell : ItemsPannelCell
{
    [HideInInspector] public Product cellProduct;
    
    public override void OnItemCellClick()
    {
        ConfirmPanel.Instance.CreateConfirmPanel($"Подтвердите покупку. Это будет стоить {cellProduct.Price}",
            cellProduct.Picture, onConfirm: BuyThisProduct);
    }

    private void BuyThisProduct()
    {
        ProductShop.Instance.BuyProduct(cellProduct);
    }
}