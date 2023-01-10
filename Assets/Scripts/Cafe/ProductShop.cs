using System.Collections.Generic;
using UnityEngine;

public class ProductShop : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private AllLevels allLevels;

    [field: HideInInspector]
    public List<Product> AvailableProducts { get; private set; }

    private void Awake()
    {
        CheckAvailableFood();
    }

    public void BuyProduct(Product product, Player player)
    {
        if (player.Money >= product.Price)
        {
            player.Money -= product.Price;
            player.InventoryProducts.Add(product);
        }
    }
    
    private void CheckAvailableFood()
    {
        AvailableProducts = new List<Product>();
        for (int i = 0; i <= player.CurrentLevel.Number - 1; i++)
        {
            foreach (Product product in allLevels.Levels[i].OpenInThisLevelProducts)
            {
                AvailableProducts.Add(product);
            }
        }
    }
}
