using UI;
using UnityEngine;
using UnityEngine.UI;

public class ProductShopPannel : ItemsPannel
{
    [SerializeField] private ProductShop productShop;
    [SerializeField] private ScrollRect shopPanel;
    [SerializeField] private Button cellPrefab;
    [SerializeField] protected ConfirmPurchaseMealPanel confirmMealPanel;

    public override void CreateItemsPanel()
    {
        foreach (Product product in productShop.AvailableProducts)
        {
            Button currentButton = Instantiate(cellPrefab, parent: shopPanel.content.transform);
            currentButton.image.sprite = product.Picture;
            ProductShopCell currentProductShopCell = currentButton.GetComponent<ProductShopCell>();
            if (currentProductShopCell != null)
            {
                currentProductShopCell.confirmMealPanel = confirmMealPanel;
                currentProductShopCell.cellProduct = product;
            }
            else
            {
                Debug.LogError("On cell button must be ProductShopCell");
            }
        }
    }
    
    public override void ClearItemsPanel()
    {
        for (int i = 0; i < shopPanel.content.transform.childCount; i++)
        {
            Transform currentTransform = shopPanel.content.transform.GetChild(i);
            Destroy(currentTransform.gameObject);
        }
    }
}