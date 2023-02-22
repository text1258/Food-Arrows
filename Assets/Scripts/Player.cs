using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UI;

public class Player : MonoBehaviour
{
    public static Player instance;

    [Header("States")]
    [SerializeField] private uint money;
    [SerializeField] private uint experience;
    [SerializeField] private List<Food> inventoryFoods;
    [SerializeField] private List<Product> inventoryProducts;
    [SerializeField] private List<Weapon> inventoryWeapons;
    [SerializeField] private Level currentLevel;
    [Header("AllItems")]
    [SerializeField] private AllFoods allFoods;
    [SerializeField] private AllProducts allProducts;
    [SerializeField] private AllWeapons allWeapons;
    [SerializeField] private AllLevels allLevels;
    [Header("Save")]
    [SerializeField] private UnityEvent onLoad;

    private Food currentOrder;
    private string currentVisitorIndex;
    public List<Weapon> AvailableWeapons { get; private set; }
    public uint Money
    {
        get => money;
        set
        {
            money = value;
            Saver.instance.Save();
            PlayerStates.instance.UpdateAllStatesUI();
        }
    }
    public uint Experience
    {
        get => experience;
        set
        {
            experience = value;
            Saver.instance.Save();
            if (CurrentLevel is NormalLevel && Experience >= ((NormalLevel)CurrentLevel).ExperienceToNextLevel)
            {
                //Indexing of levels is 1 more than indexing of lists
                CurrentLevel = allLevels.Levels[(int)CurrentLevel.Number];
                InfoPanel.instance.ShowInfoPanel("Новый уровень! Вы открыли:", (CurrentLevel.OpenInThisLevelFoods.Select(x => x.Sprite).Concat(CurrentLevel.OpenInThisLevelProducts.Select(x => x.Sprite)).Concat(CurrentLevel.OpenInThisLevelWeapons.Select(x => x.Sprite))).ToArray());
                Experience = 0;
            }
            PlayerStates.instance.UpdateAllStatesUI();
        }
    }
    public List<Product> InventoryProducts => inventoryProducts;
    public List<Food> InventoryFoods => inventoryFoods;
    public List<Weapon> InventoryWeapons => inventoryWeapons;

    public Level CurrentLevel
    {
        get => currentLevel;
        private set
        {
            currentLevel = value;
            Saver.instance.Save();
            CheckAvailableWeapons();
            PlayerStates.instance.UpdateAllStatesUI();
        }
    }
    public Food CurrentOrder
    {
        get => currentOrder;
        set
        {
            currentOrder = value;
            Saver.instance.Save();
        }
    }

    public string CurrentVisitorIndex
    {
        get => currentVisitorIndex;
        set
        {
            currentVisitorIndex = value;
            Saver.instance.Save();
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            onLoad.Invoke();
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
            StartCoroutine(OnFirstAwake());
        }
    }

    private IEnumerator OnFirstAwake()
    {
        yield return new WaitUntil(() => Saver.instance != null);
        Saver.instance.Load();
        onLoad.Invoke();
    }

    public void SetPlayerInfo(SavingData savingData)
    {
        if (savingData != null)
        {
            currentLevel = allLevels.Levels[(int)(savingData.levelNumber - 1)];
            money = savingData.money;
            experience = savingData.experience;
            inventoryFoods = FindItemsByIDes(savingData.inventoryFoodsIDes, new List<Item>(allFoods.Foods)).ConvertAll(item => (Food)item);
            inventoryProducts = FindItemsByIDes(savingData.inventoryProductsIDes, new List<Item>(allProducts.Products)).ConvertAll(item => (Product)item);
            inventoryWeapons = FindItemsByIDes(savingData.inventoryWeaponsIDes, new List<Item>(allWeapons.Weapons)).ConvertAll(item => (Weapon)item);
            currentOrder = (Food)FindItemByID(savingData.currentOrderID, new List<Item>(allFoods.Foods));
            currentVisitorIndex = savingData.currentVisitorIndex;
        }
        PlayerStates.instance.UpdateAllStatesUI();
        CheckAvailableWeapons();
    }

    public void AddMoneyForAdversiting()
    {
        Money += CurrentLevel.MoneyForAdvertising;
    }

    private void CheckAvailableWeapons()
    {
        AvailableWeapons = new List<Weapon>();
        for (int i = 0; i <= CurrentLevel.Number - 1; i++)
        {
            foreach (Weapon weapon in allLevels.Levels[i].OpenInThisLevelWeapons)
            {
                AvailableWeapons.Add(weapon);
            }
        }
    }

    private static List<Item> FindItemsByIDes(List<string> itemsIDes, List<Item> allItems)
    {
        return itemsIDes.Select(id => FindItemByID(id, allItems)).ToList();
    }

    private static Item FindItemByID(string itemID, List<Item> allItems)
    {
        return allItems.FirstOrDefault(item => item.ID == itemID);
    }
}