using UnityEngine;
using UnityEngine.UI;

public class GiveOrderButton : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Image orderImage;
    [SerializeField] private MessageText messageText;
    [SerializeField] private string phraseIfNotFood;
    [HideInInspector] public Visitor CurrentVisitor;
    [HideInInspector] private Coroutine currentShowMessage;

    private void OnEnable()
    {
        orderImage.sprite = CurrentVisitor.order.Picture;
    }

    public void ConfirmGiveOrder()
    {
        if (player.InventoryFoods.Contains(CurrentVisitor.order))
        {
            GiveOrder(CurrentVisitor);
            this.gameObject.SetActive(false);
        }
        else
        {
            if (currentShowMessage != null)
            {
                StopCoroutine(currentShowMessage);
            }
            currentShowMessage = StartCoroutine(messageText.ShowMessage(phraseIfNotFood, 3f));
        }
        player.ShowAllStatesViewers();
    }
    
    private void GiveOrder(Visitor visitor)
    {
        player.InventoryFoods.Remove(visitor.order);
        player.Experience += 1;
        player.CurrentOrder = null;
        player.CurrentVisitorIndex = null;
        visitor.isSatisfied = true;
    }
}