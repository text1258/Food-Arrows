using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;

public class Saver : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void SaveExtern(string data);
    

    [DllImport("__Internal")]
    private static extern void LoadExtern();
    
    [SerializeField] private Player player;

    private void Awake()
    {
        //player.Load();
        LoadExtern();
    }
    
    private void OnApplicationQuit()
    {
        Save();
    }

    public void Save()
    {
        SavingData savingData;
        if (player.CurrentOrder == null)
        {
            savingData = new SavingData(player.Money, player.CurrentLevel.Number, player.Experience,
                GetItemsIDes(new List<Item>(player.InventoryFoods)), GetItemsIDes(new List<Item>(player.InventoryProducts)),
                GetItemsIDes(new List<Item>(player.InventoryWeapons)));
        }
        else
        {
            savingData = new SavingData(player.Money, player.CurrentLevel.Number, player.Experience,
                GetItemsIDes(new List<Item>(player.InventoryFoods)), GetItemsIDes(new List<Item>(player.InventoryProducts)),
                GetItemsIDes(new List<Item>(player.InventoryWeapons)), player.CurrentOrder.ID, player.CurrentVisitorIndex);
        }
        //File.WriteAllText("Assets/SavingData.json", JsonUtility.ToJson(savingData));
        SaveExtern(JsonUtility.ToJson(savingData));
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