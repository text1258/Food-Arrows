using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dish", menuName = "ScriptableObjects/Items/Dish")]
public class Food : Item
{
    [SerializeField] private List<Product> cookingProducts;
    public List<Product> CookingProducts => cookingProducts;
}