using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CookingPanel : MonoBehaviour
{
    public static CookingPanel Instance;

    [SerializeField] private CookingPanelCell cookingPanelCellPrefab;
    [SerializeField] private AllFoods allFoods;

    private List<Food> possibleToCookDishes;

    private void Awake()
    {
        Instance = this;
    }

    public void ShowCookingPanel()
    {
        possibleToCookDishes = FindPossibleFoods(allFoods.Foods, Player.Instance.InventoryProducts);
        ItemsPannel.Instance.CreateItemsPanel("Выберите, что хотите приготовить");
        foreach (Food food in possibleToCookDishes)
        {
            cookingPanelCellPrefab.GetComponent<Image>().sprite = food.Sprite;
            cookingPanelCellPrefab.GetComponent<CookingPanelCell>().cellFood = food;
            ItemsPannel.Instance.AddItemToPanel(cookingPanelCellPrefab.gameObject);
        }
    }

    public void Cook(Food food)
    {
        foreach (Product product in food.CookingProducts)
        {
            Player.Instance.InventoryProducts.Remove(product);
            Saver.Instance.Save();
        }
        Player.Instance.InventoryFoods.Add(food);
        Saver.Instance.Save();
        CookingFoodAnimation.Instance.StartCookAnimation(food);
        ItemsPannel.Instance.ClearItemsPanel();
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