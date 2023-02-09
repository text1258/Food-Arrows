using System.Collections.Generic;
using UnityEngine;

public class ProductShop : MonoBehaviour
{
    [field: HideInInspector]
    public List<Product> AvailableProducts { get; private set; }

    private void Start()
    {
        CheckAvailableFood();
    }

    public void BuyProduct(Product product)
    {
        if (Player.Instance.Money >= product.Price)
        {
            Player.Instance.Money -= product.Price;
            Player.Instance.InventoryProducts.Add(product);
            Saver.instance.Save();
        }
    }
    
    private void CheckAvailableFood()
    {
        AvailableProducts = new List<Product>();
        for (int i = 0; i <= Player.Instance.CurrentLevel.Number - 1; i++)
        {
            foreach (Product product in AllScriptableObjects.GetAllScriptableObjects<Level>()[i].OpenInThisLevelProducts)
            {
                AvailableProducts.Add(product);
            }
        }
    }
}
