using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Image inventoryCellPrefab;
    
    public void ShowInventoryPanel()
    {
        ItemsPannel.instance.CreateItemsPanel("Инвентарь");
        foreach (Item item in Player.instance.InventoryFoods.Select(food => (Item)food).Concat(Player.instance.InventoryProducts.Select(product => (Item)product)).Concat(Player.instance.InventoryWeapons.Select(weapon => (Item)weapon)))
        {
            inventoryCellPrefab.sprite = item.Sprite;
            ItemsPannel.instance.AddItemToPanel(inventoryCellPrefab.gameObject);
        }
    }
}