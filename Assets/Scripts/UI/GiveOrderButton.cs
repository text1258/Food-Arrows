using UnityEngine;
using UnityEngine.UI;

public class GiveOrderButton : MonoBehaviour
{
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
        if (Player.Instance.InventoryFoods.Contains(CurrentVisitor.order))
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
    }
    
    private void GiveOrder(Visitor visitor)
    {
        Player.Instance.InventoryFoods.Remove(visitor.order);
        Player.Instance.Experience += 1;
        Player.Instance.CurrentOrder = null;
        Player.Instance.CurrentVisitorIndex = null;
        visitor.isSatisfied = true;
        Saver.instance.Save();
    }
}