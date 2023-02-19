using System.Collections.Generic;
using System.IO;
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
#if UNITY_EDITOR || UNITY_ANDROID
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
        Player.Instance.SetPlayerInfo(JsonUtility.FromJson<SavingData>(data));
#else
        LoadDataFromServer();
#endif
        }

    public void SetData(string data)
    {
        Player.Instance.SetPlayerInfo(JsonUtility.FromJson<SavingData>(data));
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