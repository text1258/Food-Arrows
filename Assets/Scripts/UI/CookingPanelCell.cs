using UI;
using UnityEngine;

public class CookingPanelCell : ItemsPannelCell
{
    [HideInInspector] public Food cellFood;

    public override void OnItemCellClick()
    {
        ConfirmPanel.Instance.CreateConfirmPanel($"Do you want to cook it?",
            cellFood.Picture, onConfirm: cellFood.Cook);
    }
}