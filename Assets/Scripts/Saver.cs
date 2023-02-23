using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class Saver : MonoBehaviour
{
    public static Saver instance;

    private void OnEnable() => YandexGame.GetDataEvent += Load;

    private void OnDisable() => YandexGame.GetDataEvent -= Load;

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
            if (YandexGame.SDKEnabled == true)
            {
                Load();
            }
        }
        instance = this;
    }

    [ContextMenu("Save")]
    public void Save()
    {
        YandexGame.savesData.money = Player.instance.Money;
        YandexGame.savesData.levelNumber = Player.instance.CurrentLevel.Number;
        YandexGame.savesData.experience = Player.instance.Experience;
        YandexGame.savesData.inventoryFoodsIDes = GetItemsIDes(new List<Item>(Player.instance.InventoryFoods));
        YandexGame.savesData.inventoryProductsIDes = GetItemsIDes(new List<Item>(Player.instance.InventoryProducts));
        YandexGame.savesData.inventoryWeaponsIDes = GetItemsIDes(new List<Item>(Player.instance.InventoryWeapons));
        if (Player.instance.CurrentOrder != null)
        {
            YandexGame.savesData.currentOrderID = Player.instance.CurrentOrder.ID;
        }
        if (Player.instance.CurrentVisitorIndex != null & Player.instance.CurrentVisitorIndex != "")
        {
            YandexGame.savesData.currentVisitorIndex = Player.instance.CurrentVisitorIndex;
        }
        YandexGame.SaveProgress();
    }

    [ContextMenu("Load")]
    public void Load()
    {
        StartCoroutine(SetLoadingData());
    }

    private IEnumerator SetLoadingData()
    {
        yield return new WaitUntil(() => Player.instance != null);
        Player.instance.SetPlayerInfo();
        yield break;
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