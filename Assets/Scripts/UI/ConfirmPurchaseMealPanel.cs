using UnityEngine;

namespace UI
{
    public class ConfirmPurchaseMealPanel : ConfirmPanel
    {
        [SerializeField] private ProductShop productShop;
        [SerializeField] private MessageText messageText;
        [SerializeField] private string phraseIfNotMoney;
        public Product PurchasedProduct { get; set; }
        private Coroutine currentShowMessage;

        public override void Confirm()
        {
            if (Player.Instance.Money >= PurchasedProduct.Price)
            {
                productShop.BuyProduct(PurchasedProduct);
            }
            else
            {
                if (currentShowMessage != null)
                {
                    StopCoroutine(currentShowMessage);
                }
                currentShowMessage = StartCoroutine(messageText.ShowMessage(phraseIfNotMoney, 2f));
            }
            AgreeButton.onClick.RemoveListener(Confirm);
        }
    }
}
