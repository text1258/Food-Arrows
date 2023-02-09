using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dish", menuName = "ScriptableObjects/Items/Dish")]
public class Food : Item
{
    [SerializeField] private List<Product> cookingProducts;
    public List<Product> CookingProducts => cookingProducts;

    public void Cook()
    {
        foreach (Product product in CookingProducts)
        {
            Player.Instance.InventoryProducts.Remove(product);
            Saver.instance.Save();
        }
        Player.Instance.InventoryFoods.Add(this);
        CookingFoodAnimation.Instance.StartCookAnimation(this);
        Saver.instance.Save();
    }
}