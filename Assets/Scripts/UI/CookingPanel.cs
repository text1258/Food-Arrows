using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CookingPanel : MonoBehaviour
{
    public static CookingPanel instance;

    [SerializeField] private CookingPanelCell cookingPanelCellPrefab;
    [SerializeField] private AllFoods allFoods;

    private List<Food> possibleToCookDishes;

    private void Awake()
    {
        instance = this;
    }

    public void ShowCookingPanel()
    {
        possibleToCookDishes = FindPossibleFoods(allFoods.Foods, Player.instance.InventoryProducts.ToList());
        ItemsPannel.instance.CreateItemsPanel("Выберите, что хотите приготовить");
        foreach (Food food in possibleToCookDishes)
        {
            cookingPanelCellPrefab.GetComponent<Image>().sprite = food.Sprite;
            cookingPanelCellPrefab.GetComponent<CookingPanelCell>().cellFood = food;
            ItemsPannel.instance.AddItemToPanel(cookingPanelCellPrefab.gameObject);
        }
    }

    public void Cook(Food food)
    {
        foreach (Product product in food.CookingProducts)
        {
            Player.instance.InventoryProducts.Remove(product);
            Saver.instance.Save();
        }
        Player.instance.InventoryFoods.Add(food);
        Saver.instance.Save();
        CookingFoodAnimation.instance.StartCookAnimation(food);
        ItemsPannel.instance.ClearItemsPanel();
        ShowCookingPanel();
    }

    private static List<Food> FindPossibleFoods(List<Food> allFoods, List<Product> playerProducts)
    {
        return allFoods.Where(dish => CheckCanCook(dish, playerProducts)).ToList();
    }

    private static bool CheckCanCook(Food cookingFood, List<Product> playerProducts)
    {
        return cookingFood.CookingProducts.All(playerProducts.Contains);
    }
}