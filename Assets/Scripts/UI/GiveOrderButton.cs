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
        orderImage.sprite = CurrentVisitor.order.Sprite;
    }

    public void GiveOrder()
    {
        if (Player.instance.InventoryFoods.Contains(CurrentVisitor.order))
        {
            Player.instance.InventoryFoods.Remove(CurrentVisitor.order);
            Player.instance.Experience += 1;
            Player.instance.CurrentOrder = null;
            Player.instance.CurrentVisitorIndex = null;
            CurrentVisitor.isSatisfied = true;
            Saver.instance.Save();
            this.gameObject.SetActive(false);
        }
        else
        {
            MessageText.instance.Message("Этой еды нет в инвентаре!", 3f);
        }
    }

    public void ShowCurrentFoodInfo()
    {
        InfoPanel.instance.ShowInfoPanel("Продукты для приготовления этой еды:", CurrentVisitor.order.CookingProducts.Select(x => x.Sprite).ToArray());
    }
}