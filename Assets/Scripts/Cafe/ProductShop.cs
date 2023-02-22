using System.Collections.Generic;
using UnityEngine;

public class ProductShop : MonoBehaviour
{
    public static ProductShop instance;
    public List<Product> AvailableToBuyProducts { get; private set; }
    [SerializeField] private AllLevels allLevels;

    private void Awake()
    {
        instance = this;
    }

    public void BuyProduct(Product product)
    {
        if (Player.instance.Money >= product.Price)
        {
            Player.instance.Money -= product.Price;
            Player.instance.InventoryProducts.Add(product);
            Saver.instance.Save();
        }
        else
        {
            MessageText.instance.Message("У вас недостаточно денег(", 2f);
        }
    }
    
    public void UpdateAvailableToBuyProducts()
    {
        AvailableToBuyProducts = new List<Product>();
        for (int i = 0; i <= Player.instance.CurrentLevel.Number - 1; i++)
        {
            foreach (Product product in allLevels.Levels[i].OpenInThisLevelProducts)
            {
                AvailableToBuyProducts.Add(product);
            }
        }
    }
}
