using UnityEngine;

public class CookingPanelCell : ItemsPannelCell
{
    [HideInInspector] public CookingConfirmPanel confirmPanel;
    [HideInInspector] public Food cellFood;

    public override void OnItemCellClick()
    {
        confirmPanel.confirmPanelGameObject.gameObject.SetActive(true);
        confirmPanel.confirmPanelImage.sprite = cellFood.Picture;
        confirmPanel.CommentText.text = confirmPanel.CommentTextTitle;
        confirmPanel.cookingFood = cellFood;
        confirmPanel.agreeButton.onClick.AddListener(confirmPanel.Confirm);
    }
}