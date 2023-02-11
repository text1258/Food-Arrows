using UnityEngine;
using UnityEngine.UI;

public class ProductShopPannel : MonoBehaviour
{
    [SerializeField] private ProductShopCell productShopCellPrefab;

    public void ShowProductShopPannel()
    {
        ProductShop.Instance.UpdateAvailableToBuyProducts();
        ItemsPannel.Instance.CreateItemsPanel("Продуктовый магазин");
        foreach (Product product in ProductShop.Instance.AvailableToBuyProducts)
        {
            productShopCellPrefab.GetComponent<Image>().sprite = product.Sprite;
            productShopCellPrefab.cellProduct = product;
            ItemsPannel.Instance.AddItemToPanel(productShopCellPrefab.gameObject);
        }
    }
}