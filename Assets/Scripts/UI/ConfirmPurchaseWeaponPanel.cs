using UnityEngine;

public class ConfirmPurchaseWeaponPanel : ConfirmPanel
{
    [SerializeField] private MessageText messageText;
    [SerializeField] private string phraseIfNotMoney;
    [HideInInspector] public Weapon cellWeapon;
    [HideInInspector] public Weapon3DButtonNotPurchased pressedWeapon;
    private Coroutine currentShowMessage;
    public override void Confirm()
    {
        if (player.Money >= cellWeapon.Price)
        {
            player.InventoryWeapons.Add(cellWeapon);
            player.Money -= cellWeapon.Price;
            Destroy(pressedWeapon.gameObject);
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