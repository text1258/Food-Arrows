using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Saver : MonoBehaviour
{
    public static Saver Instance = null;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
    }

    [ContextMenu("Save")]
    public void Save()
    {
        SavingData savingData;
        if (Player.Instance.CurrentOrder == null)
        {
            savingData = new SavingData(Player.Instance.Money, Player.Instance.CurrentLevel.Number, Player.Instance.Experience,
                GetItemsIDes(new List<Item>(Player.Instance.InventoryFoods)), GetItemsIDes(new List<Item>(Player.Instance.InventoryProducts)),
                GetItemsIDes(new List<Item>(Player.Instance.InventoryWeapons)));
        }
        else
        {
            savingData = new SavingData(Player.Instance.Money, Player.Instance.CurrentLevel.Number, Player.Instance.Experience,
                GetItemsIDes(new List<Item>(Player.Instance.InventoryFoods)), GetItemsIDes(new List<Item>(Player.Instance.InventoryProducts)),
                GetItemsIDes(new List<Item>(Player.Instance.InventoryWeapons)), Player.Instance.CurrentOrder.ID, Player.Instance.CurrentVisitorIndex);
        }
    }

    [ContextMenu("Load")]
    public void Load()
    {
    }

    private static List<string> GetItemsIDes(List<Item> items)
    {
        List<string> ItemsIDes = new List<string>();
        foreach (Item item in items)
        {
            ItemsIDes.Add(item.ID);
        }
        return ItemsIDes;
    }
}