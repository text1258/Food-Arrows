using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Saver : MonoBehaviour
{
    public static Saver instance;

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
            Load();
        }
        instance = this;
    }

    [ContextMenu("Save")]
    public void Save()
    {
        PlayerData data;
        if (Player.instance.CurrentOrder == null | string.IsNullOrEmpty(Player.instance.CurrentVisitorIndex))
        {
            data = new PlayerData(Player.instance.Money, Player.instance.CurrentLevel.Number, Player.instance.Experience,
                GetItemsIDes(new List<Item>(Player.instance.InventoryFoods)),
                GetItemsIDes(new List<Item>(Player.instance.InventoryProducts)),
                GetItemsIDes(new List<Item>(Player.instance.InventoryWeapons)));
        }
        else
        {
            data = new PlayerData(Player.instance.Money, Player.instance.CurrentLevel.Number, Player.instance.Experience,
                GetItemsIDes(new List<Item>(Player.instance.InventoryFoods)),
                GetItemsIDes(new List<Item>(Player.instance.InventoryProducts)),
                GetItemsIDes(new List<Item>(Player.instance.InventoryWeapons)),
                Player.instance.CurrentOrder.ID, Player.instance.CurrentVisitorIndex);
        }
#if UNITY_EDITOR
        File.WriteAllText("Assets/SavingData.json", JsonUtility.ToJson(data));
#elif UNITY_ANDROID
        PlayerPrefs.SetString("SavingData", JsonUtility.ToJson(data));
        PlayerPrefs.Save();
#endif
    }

    [ContextMenu("Load")]
    public void Load()
    {
        StartCoroutine(SetLoadingData());
    }

    private IEnumerator SetLoadingData()
    {
        string dataJson = null;
        try
        {
#if UNITY_EDITOR
            dataJson = File.ReadAllText("Assets/SavingData.json");
#elif UNITY_ANDROID
            dataJson = PlayerPrefs.GetString("SavingData");
#endif
        }
        catch { }
        yield return new WaitUntil(() => Player.instance != null);
        Player.instance.SetPlayerInfo(JsonUtility.FromJson<PlayerData>(dataJson));
        yield break;
    }

    public static List<string> GetItemsIDes(List<Item> items)
    {
        List<string> ItemsIDes = new List<string>();
        foreach (Item item in items)
        {
            ItemsIDes.Add(item.ID);
        }
        return ItemsIDes;
    }
}