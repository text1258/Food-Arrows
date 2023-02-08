using UnityEngine;

public class CookingPanelCell : ItemsPannelCell
{
    [HideInInspector] public CookingConfirmPanel confirmPanel;
    [HideInInspector] public Food cellFood;

    public override void OnItemCellClick()
    {
        confirmPanel.ConfirmPanelGameObject.gameObject.SetActive(true);
        confirmPanel.ConfirmPanelImage.sprite = cellFood.Picture;
        confirmPanel.CommentText.text = confirmPanel.CommentTextTitle;
        confirmPanel.cookingFood = cellFood;
        confirmPanel.AgreeButton.onClick.AddListener(confirmPanel.Confirm);
    }
}