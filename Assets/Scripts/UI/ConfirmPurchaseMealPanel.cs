using UnityEngine;

public class ConfirmPurchaseMealPanel : ConfirmPanel
{
    [SerializeField] private ProductShop productShop;
    [SerializeField] private MessageText messageText;
    [SerializeField] private string phraseIfNotMoney;
    [HideInInspector] public Product purchasedProduct;
    private Coroutine currentShowMessage;
    
    public override void Confirm()
    {
        if (player.Money >= purchasedProduct.Price)
        {
            productShop.BuyProduct(purchasedProduct, player);
        }
        else
        {
            if (currentShowMessage != null)
            {
                StopCoroutine(currentShowMessage);
            }
            currentShowMessage = StartCoroutine(messageText.ShowMessage(phraseIfNotMoney, 2f));
        }
        agreeButton.onClick.RemoveListener(Confirm);
    }
}
