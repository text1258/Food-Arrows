using UnityEngine;

public class Weapon3DButtonNotPurchased : Weapon3DButton
{
    [HideInInspector] public ConfirmPurchaseWeaponPanel confirmPanel;
    
    public override void OnItemCellClick()
    {
        confirmPanel.confirmPanelGameObject.SetActive(true);
        confirmPanel.confirmPanelImage.sprite = cellWeapon.Picture;
        confirmPanel.CommentText.text = $"{confirmPanel.CommentTextTitle}{cellWeapon.Price}";
        confirmPanel.cellWeapon = cellWeapon;
        confirmPanel.pressedWeapon = this;
        confirmPanel.agreeButton.onClick.AddListener(confirmPanel.Confirm);
    }
}