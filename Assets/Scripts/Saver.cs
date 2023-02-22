using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Saver : MonoBehaviour
{
    public static Saver instance = null;

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
        if (Player.instance.CurrentOrder == null)
        {
            savingData = new SavingData(Player.instance.Money, Player.instance.CurrentLevel.Number, Player.instance.Experience,
                GetItemsIDes(new List<Item>(Player.instance.InventoryFoods)), GetItemsIDes(new List<Item>(Player.instance.InventoryProducts)),
                GetItemsIDes(new List<Item>(Player.instance.InventoryWeapons)));
        }
        else
        {
            savingData = new SavingData(Player.instance.Money, Player.instance.CurrentLevel.Number, Player.instance.Experience,
                GetItemsIDes(new List<Item>(Player.instance.InventoryFoods)), GetItemsIDes(new List<Item>(Player.instance.InventoryProducts)),
                GetItemsIDes(new List<Item>(Player.instance.InventoryWeapons)), Player.instance.CurrentOrder.ID, Player.instance.CurrentVisitorIndex);
        }
#if UNITY_EDITOR
        File.WriteAllText("Assets/SavingData.json", JsonUtility.ToJson(savingData));
#elif UNITY_ANDROID
        PlayerPrefs.SetString("SavingData", JsonUtility.ToJson(savingData));
        PlayerPrefs.Save();
#else
        PlayerPrefs.SetString("SavingData", JsonUtility.ToJson(savingData));
        PlayerPrefs.Save();
#endif
    }

    [ContextMenu("Load")]
    public void Load()
    {
        string data = null;
        try
        {
#if UNITY_EDITOR
            data = File.ReadAllText("Assets/SavingData.json");
#elif UNITY_ANDROID
            data = PlayerPrefs.GetString("SavingData");
#endif
        }
        catch { }
        Player.instance.SetPlayerInfo(JsonUtility.FromJson<SavingData>(data));
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