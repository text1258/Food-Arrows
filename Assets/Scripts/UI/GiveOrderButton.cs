using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UI;

public class GiveOrderButton : MonoBehaviour
{
    [SerializeField] private Image orderImage;
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
            MessageText.Instance.Message(phraseIfNotFood, 3f);
        }
    }

    public void ShowCurrentFoodInfo()
    {
        InfoPanel.Instance.ShowInfoPanel("Products for cooking this food:", CurrentVisitor.order.CookingProducts.Select(x => x.Picture).ToArray());
    }
}