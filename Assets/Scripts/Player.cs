using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
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
    [Header("StatesViewers")]
    [SerializeField] private MoneyText moneyText;
    [SerializeField] private ExperienceText experienceText;
    [SerializeField] private LevelText levelText;
    [SerializeField] private AddMoneyForAdvertisingButton addMoneyForAdvertisingButton;
    [Header("Save")]
    [SerializeField] private Saver saver;
    [SerializeField] private UnityEvent onLoadData;

    [HideInInspector] public List<Food> possibleToCookDishes;
    [HideInInspector] private Food currentOrder;
    [HideInInspector] private string currentVisitorIndex;
    [HideInInspector] public List<Food> AvailableFood { get; private set; } = new List<Food>();
    [HideInInspector] public List<Weapon> AvailableWeapons { get; private set; }
    
    public uint Money
    {
        get => money;
        set
        {
            money = value;
            saver.Save();
            moneyText.ShowText();
        }
    }
    public uint Experience
    {
        get => experience;
        set
        {
            experience = value;
            saver.Save();
            if (CurrentLevel is NormalLevel && Experience >= ((NormalLevel)CurrentLevel).ExperienceToNextLevel)
            {
                //Indexing of levels is 1 more than indexing of lists
                CurrentLevel = allLevels.Levels[(int)CurrentLevel.Number];
                Experience = 0;
            }
            experienceText.ShowText();
        }
    }
    public List<Product> InventoryProducts
    {
        get => inventoryProducts;
        set
        {
            inventoryProducts = value;
            saver.Save();
        }
    }
    public List<Food> InventoryFoods
    {
        get => inventoryFoods;
        set
        {
            inventoryFoods = value;
            saver.Save();
        }
    }
    public List<Weapon> InventoryWeapons
    {
        get => inventoryWeapons;
        set
        {
            inventoryWeapons = value;
            saver.Save();
        }
    }
    public Level CurrentLevel
    {
        get => currentLevel;
        private set
        {
            currentLevel = value;
            saver.Save();
            UpdateFindPossibleDishes();
            CheckAvailableFood();
            CheckAvailableWeapons();
            levelText.ShowText();
            addMoneyForAdvertisingButton.ShowText();
        }
    }
    public Food CurrentOrder
    {
        get => currentOrder;
        set
        {
            currentOrder = value;
            saver.Save();
        }
    }

    public string CurrentVisitorIndex
    {
        get => currentVisitorIndex;
        set
        {
            currentVisitorIndex = value;
            saver.Save();
        }
    }

    private static List<Food> FindPossibleFoods(List<Food> allFoods, List<Product> playerProducts)
    {
        List<Food> possibleFoods = new List<Food>();
        foreach (Food dish in allFoods)
        {
            if (CanCook(dish, playerProducts))
            {
                possibleFoods.Add(dish);
            }
        }
        return possibleFoods;
    }
    
    private static bool CanCook(Food cookingFood, List<Product> playerProducts)
    {
        foreach (Product needProduct in cookingFood.CookingProducts)
        {
            if (!playerProducts.Contains(needProduct))
            {
                return false;
            }
        }
        return true;
    }
    
    public void CookFood(Food food)
    {
        foreach (Product product in food.CookingProducts)
        {
            InventoryProducts.Remove(product);
        }
        InventoryFoods.Add(food);
    }

    public void UpdateFindPossibleDishes()
    {
        possibleToCookDishes = FindPossibleFoods(AvailableFood, inventoryProducts);
    }

    private void CheckAvailableFood()
    {
        AvailableFood = new List<Food>();
        for (int i = 0; i <= CurrentLevel.Number - 1; i++)
        {
            foreach (Food food in allLevels.Levels[i].OpenInThisLevelFoods)
            {
                AvailableFood.Add(food);
            }
        }
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
    
    public void Load(
        //string data
        )
    {
        SavingData savingData = null;
        try
        {
            savingData = JsonUtility.FromJson<SavingData>(File.ReadAllText("SavingData.json"));
            //savingData = JsonUtility.FromJson<SavingData>(data);
        }
        catch { }
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
        ShowAllStatesViewers();
        CheckAvailableFood();
        CheckAvailableWeapons();
        OnLoad();
    }

    private static Item FindItemByID(string itemID, List<Item> allItems)
    {
        foreach (Item item in allItems)
        {
            if (item.ID == itemID)
            {
                return item;
            }
        }
        return null;
    }

    private static List<Item> FindItemsByIDes(List<string> itemsIDes, List<Item> allItems)
    {
        List<Item> items = new List<Item>();
        foreach (string ID in itemsIDes)
        {
            Item currentItem = FindItemByID(ID, allItems);
            items.Add(currentItem);
        }
        return items;
    }

    public void AddMoneyForAdvertising()
    {
        Money += CurrentLevel.MoneyForAdvertisingCount;
    }

    public void ShowAllStatesViewers()
    {
        moneyText.ShowText();
        experienceText.ShowText();
        levelText.ShowText();
        addMoneyForAdvertisingButton.ShowText();
    }

    private void OnLoad() => onLoadData.Invoke();
}