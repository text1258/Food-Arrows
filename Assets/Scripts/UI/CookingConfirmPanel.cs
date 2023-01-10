using UnityEngine;

public class CookingConfirmPanel : ConfirmPanel
{
    [SerializeField] private CookingPanel cookingPanel;
    [SerializeField] private CookingFoodAnimation cookingFoodAnimation;
    [HideInInspector] public Food cookingFood;
    
    public override void Confirm()
    {
        player.CookFood(cookingFood);
        cookingPanel.UpdateItemsPanel();
        agreeButton.onClick.RemoveListener(Confirm);
        cookingFoodAnimation.cookingFood = cookingFood;
        cookingFoodAnimation.cookingFood = cookingFood;
        StartCoroutine(cookingFoodAnimation.CookingAnimate());
    }
}