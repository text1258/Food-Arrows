using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CookingPanel : ItemsPannel
{
    [SerializeField] private ScrollRect cookingPanel;
    [SerializeField] private CookingPanelCell cookingPanelCellPrefab;
    [SerializeField] private AllFoods allFoods;

    private List<Food> possibleToCookDishes;

    public override void CreateItemsPanel()
    {
        possibleToCookDishes = FindPossibleFoods(allFoods.Foods, Player.Instance.InventoryProducts);
        foreach (Food food in possibleToCookDishes)
        {
            GameObject currentCell = Instantiate(cookingPanelCellPrefab.gameObject, parent: cookingPanel.content.transform);
            currentCell.GetComponent<Image>().sprite = food.Picture;
            currentCell.GetComponent<CookingPanelCell>().cellFood = food;
        }
    }

    public override void ClearItemsPanel()
    {
        foreach (Transform child in cookingPanel.content.transform)
        { 
            Destroy(child.gameObject);
        }
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