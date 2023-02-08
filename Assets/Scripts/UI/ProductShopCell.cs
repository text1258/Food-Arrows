using UI;
using UnityEngine;

public class ProductShopCell : ItemsPannelCell
{
    [HideInInspector] public ConfirmPurchaseMealPanel confirmMealPanel;
    [HideInInspector] public Product cellProduct;
    
    public override void OnItemCellClick()
    {
        confirmMealPanel.ConfirmPanelGameObject.gameObject.SetActive(true);
        confirmMealPanel.ConfirmPanelImage.sprite = cellProduct.Picture;
        confirmMealPanel.CommentText.text = $"{confirmMealPanel.CommentTextTitle}{cellProduct.Price}";
        confirmMealPanel.PurchasedProduct = cellProduct;
        confirmMealPanel.AgreeButton.onClick.AddListener(confirmMealPanel.Confirm);
    }
}