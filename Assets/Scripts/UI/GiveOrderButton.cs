using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UI;

public class GiveOrderButton : MonoBehaviour
{
    [SerializeField] private Image orderImage;
    [HideInInspector] public Visitor CurrentVisitor;

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
            MessageText.Instance.Message("Этой еды нет в инвенторе!", 3f);
        }
    }

    public void ShowCurrentFoodInfo()
    {
        InfoPanel.Instance.ShowInfoPanel("Продукты для приготовления этой еды:", CurrentVisitor.order.CookingProducts.Select(x => x.Picture).ToArray());
    }
}