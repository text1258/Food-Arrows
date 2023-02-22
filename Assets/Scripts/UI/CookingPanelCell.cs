using UI;
using UnityEngine;

public class CookingPanelCell : ItemsPannelCell
{
    [HideInInspector] public Food cellFood;

    public override void OnItemCellClick()
    {
        ConfirmPanel.instance.CreateConfirmPanel($"Вы хотите это приготовить?",
            cellFood.Sprite, onConfirm: CookThisFood);
    }

    private void CookThisFood()
    {
        CookingPanel.instance.Cook(cellFood);
    }
}