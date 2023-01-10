using UnityEngine;
using UnityEngine.UI;

public class MealInventory : ItemsPannel
{
    [SerializeField] private ScrollRect dishesInventory;
    [SerializeField] private ScrollRect productsInventory;
    [SerializeField] private Image inventoryCellPrefab;
    
    public override void CreateItemsPanel()
    {
        Image currentImage;
        foreach (Food dish in player.InventoryFoods)
        {
            currentImage = Instantiate(inventoryCellPrefab, parent: dishesInventory.content.transform);
            currentImage.sprite = dish.Picture;
        }
        foreach (Product product in player.InventoryProducts)
        {
            currentImage = Instantiate(inventoryCellPrefab, parent: productsInventory.content.transform);
            currentImage.sprite = product.Picture;
        }
    }

    public override void ClearItemsPanel()
    {
        Transform currentTransform;
        for (int i = 0; i < dishesInventory.content.transform.childCount; i++)
        {
            currentTransform = dishesInventory.content.transform.GetChild(i);
            Destroy(currentTransform.gameObject);
        }
        for (int i = 0; i < productsInventory.content.transform.childCount; i++)
        {
            currentTransform = productsInventory.content.transform.GetChild(i);
            Destroy(currentTransform.gameObject);
        }
    }
}