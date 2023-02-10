using UI;
using UnityEngine;

public class CookingPanelCell : ItemsPannelCell
{
    [HideInInspector] public Food cellFood;

    public override void OnItemCellClick()
    {
        ConfirmPanel.Instance.CreateConfirmPanel($"Вы хотите это приготовить?",
            cellFood.Picture, onConfirm: cellFood.Cook);
    }
}