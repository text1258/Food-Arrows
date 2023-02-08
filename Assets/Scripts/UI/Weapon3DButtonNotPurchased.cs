using UnityEngine;

public class Weapon3DButtonNotPurchased : Weapon3DButton
{
    [HideInInspector] public ConfirmPurchaseWeaponPanel confirmPanel;
    
    public override void OnItemCellClick()
    {
        confirmPanel.ConfirmPanelGameObject.SetActive(true);
        confirmPanel.ConfirmPanelImage.sprite = cellWeapon.Picture;
        confirmPanel.CommentText.text = $"{confirmPanel.CommentTextTitle}{cellWeapon.Price}";
        confirmPanel.cellWeapon = cellWeapon;
        confirmPanel.pressedWeapon = this;
        confirmPanel.AgreeButton.onClick.AddListener(confirmPanel.Confirm);
    }
}