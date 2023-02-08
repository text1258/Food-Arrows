using UI;
using UnityEngine;

public class CookingConfirmPanel : ConfirmPanel
{
    [SerializeField] private CookingPanel cookingPanel;
    [SerializeField] private CookingFoodAnimation cookingFoodAnimation;
    [HideInInspector] public Food cookingFood;
    
    public override void Confirm()
    {
        Player.Instance.CookFood(cookingFood);
        cookingPanel.UpdateItemsPanel();
        AgreeButton.onClick.RemoveListener(Confirm);
        cookingFoodAnimation.cookingFood = cookingFood;
        cookingFoodAnimation.cookingFood = cookingFood;
        StartCoroutine(cookingFoodAnimation.CookingAnimate());
    }
}