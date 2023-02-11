using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Image inventoryCellPrefab;
    
    public void ShowInventoryPanel()
    {
        ItemsPannel.Instance.CreateItemsPanel("Инвентарь");
        foreach (Item item in Player.Instance.InventoryFoods.Select(food => (Item)food).Concat(Player.Instance.InventoryProducts.Select(product => (Item)product)).Concat(Player.Instance.InventoryWeapons.Select(weapon => (Item)weapon)))
        {
            inventoryCellPrefab.sprite = item.Sprite;
            ItemsPannel.Instance.AddItemToPanel(inventoryCellPrefab.gameObject);
        }
    }
}