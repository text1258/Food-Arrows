using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Saver : MonoBehaviour
{
    [HideInInspector] public static Saver instance = null;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
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
        File.WriteAllText("Assets/SavingData.json", JsonUtility.ToJson(savingData));
    }

    [ContextMenu("Load")]
    public void Load()
    {
        SavingData savingData = null;
        try
        {
            savingData = JsonUtility.FromJson<SavingData>(File.ReadAllText("Assets/SavingData.json"));
        }
        catch { }
        Player.Instance.SetPlayerInfo(savingData);
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