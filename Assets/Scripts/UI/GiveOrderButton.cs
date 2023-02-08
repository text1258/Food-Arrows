using System.Linq;
using UI;
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

    public void GiveOrder()
    {
        if (Player.Instance.InventoryFoods.Contains(CurrentVisitor.order))
        {
            Player.Instance.InventoryFoods.Remove(CurrentVisitor.order);
            Player.Instance.Experience += 1;
            Player.Instance.CurrentOrder = null;
            Player.Instance.CurrentVisitorIndex = null;
            CurrentVisitor.isSatisfied = true;
            Saver.instance.Save();
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

    public void ShowCurrentFoodInfo()
    {
        InfoPanel.Instance.ShowInfoPanel("Products for cooking this food:", CurrentVisitor.order.CookingProducts.Select(x => x.Picture).ToArray());
    }
}