using UI;
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
        if (Player.Instance.Money >= cellWeapon.Price)
        {
            Player.Instance.InventoryWeapons.Add(cellWeapon);
            Player.Instance.Money -= cellWeapon.Price;
            Destroy(pressedWeapon.gameObject);
            Saver.instance.Save();
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