using UnityEngine;
using UnityEngine.UI;

public class ProductShopPannel : MonoBehaviour
{
    [SerializeField] private ProductShopCell productShopCellPrefab;

    public void ShowProductShopPannel()
    {
        ProductShop.instance.UpdateAvailableToBuyProducts();
        ItemsPannel.instance.CreateItemsPanel("Продуктовый магазин");
        foreach (Product product in ProductShop.instance.AvailableToBuyProducts)
        {
            productShopCellPrefab.GetComponent<Image>().sprite = product.Sprite;
            productShopCellPrefab.cellProduct = product;
            ItemsPannel.instance.AddItemToPanel(productShopCellPrefab.gameObject);
        }
    }
}