using System.Collections.Generic;
using UnityEngine;

public class ProductShop : MonoBehaviour
{
    public static ProductShop Instance;
    public List<Product> AvailableToBuyProducts { get; private set; }
    [SerializeField] private AllLevels allLevels;

    private void Awake()
    {
        Instance = this;
    }

    public void BuyProduct(Product product)
    {
        if (Player.Instance.Money >= product.Price)
        {
            Player.Instance.Money -= product.Price;
            Player.Instance.InventoryProducts.Add(product);
            Saver.instance.Save();
        }
        else
        {
            MessageText.Instance.Message("You don't have enough money!", 2f);
        }
    }
    
    public void UpdateAvailableToBuyProducts()
    {
        AvailableToBuyProducts = new List<Product>();
        for (int i = 0; i <= Player.Instance.CurrentLevel.Number - 1; i++)
        {
            foreach (Product product in allLevels.Levels[i].OpenInThisLevelProducts)
            {
                AvailableToBuyProducts.Add(product);
            }
        }
    }
}
