using UnityEngine;
using UnityEngine.UI;

public class ProductShopPannel : ItemsPannel
{
    [SerializeField] private ScrollRect shopPanel;
    [SerializeField] private ProductShopCell cellPrefab;

    public override void CreateItemsPanel()
    {
        ProductShop.Instance.UpdateAvailableToBuyProducts();
        foreach (Product product in ProductShop.Instance.AvailableToBuyProducts)
        {
            GameObject currentCell = Instantiate(cellPrefab.gameObject, parent: shopPanel.content.transform);
            currentCell.GetComponent<Image>().sprite = product.Picture;
            currentCell.GetComponent<ProductShopCell>().cellProduct = product;
        }
    }
    
    public override void ClearItemsPanel()
    {
        foreach (Transform child in shopPanel.content)
        {
            Destroy(child.gameObject);
        }
    }
}