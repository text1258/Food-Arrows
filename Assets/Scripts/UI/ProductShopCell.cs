using UnityEngine;

public class ProductShopCell : ItemsPannelCell
{
    [HideInInspector] public ConfirmPurchaseMealPanel confirmMealPanel;
    [HideInInspector] public Product cellProduct;
    
    public override void OnItemCellClick()
    {
        confirmMealPanel.confirmPanelGameObject.gameObject.SetActive(true);
        confirmMealPanel.confirmPanelImage.sprite = cellProduct.Picture;
        confirmMealPanel.CommentText.text = $"{confirmMealPanel.CommentTextTitle}{cellProduct.Price}";
        confirmMealPanel.purchasedProduct = cellProduct;
        confirmMealPanel.agreeButton.onClick.AddListener(confirmMealPanel.Confirm);
    }
}