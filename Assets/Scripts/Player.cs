using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UI;

public class Player : MonoBehaviour
{
    public static Player Instance;
    [HideInInspector] public List<Food> PossibleToCookDishes;

    [Header("States")]
    [SerializeField] private uint money;
    [SerializeField] private uint experience;
    [SerializeField] private List<Food> inventoryFoods;
    [SerializeField] private List<Product> inventoryProducts;
    [SerializeField] private List<Weapon> inventoryWeapons;
    [SerializeField] private Level currentLevel;
    [Header("Save")]
    [SerializeField] private UnityEvent onLoad;

    private Food currentOrder;
    private string currentVisitorIndex;
    public List<Food> AvailableFood { get; private set; }
    public List<Weapon> AvailableWeapons { get; private set; }
    public uint Money
    {
        get => money;
        set
        {
            money = value;
            Saver.instance.Save();
            PlayerStates.Instance.UpdateAllStatesUI();
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
                CurrentLevel = AllScriptableObjects.GetAllScriptableObjects<Level>()[(int)CurrentLevel.Number];
                InfoPanel.Instance.ShowInfoPanel("New Level! You Open:", (CurrentLevel.OpenInThisLevelFoods.Select(x => x.Picture).Concat(CurrentLevel.OpenInThisLevelProducts.Select(x => x.Picture)).Concat(CurrentLevel.OpenInThisLevelWeapons.Select(x => x.Picture))).ToArray());
                Experience = 0;
            }
            PlayerStates.Instance.UpdateAllStatesUI();
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
            UpdatePossibleDishes();
            CheckAvailableFood();
            CheckAvailableWeapons();
            PlayerStates.Instance.UpdateAllStatesUI();
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
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            onLoad.Invoke();
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
            StartCoroutine(OnFirstAwake());
        }
    }

    private IEnumerator OnFirstAwake()
    {
        yield return new WaitUntil(() => Saver.instance != null);
        Saver.instance.Load();
        onLoad.Invoke();
    }

    private static List<Food> FindPossibleFoods(List<Food> allFoods, List<Product> playerProducts)
    {
        return allFoods.Where(dish => CanCook(dish, playerProducts)).ToList();
    }

    private static bool CanCook(Food cookingFood, List<Product> playerProducts)
    {
        return cookingFood.CookingProducts.All(playerProducts.Contains);
    }

    public void CookFood(Food food)
    {
        foreach (Product product in food.CookingProducts)
        {
            InventoryProducts.Remove(product);
            Saver.instance.Save();
        }
        InventoryFoods.Add(food);
        Saver.instance.Save();
    }

    public void UpdatePossibleDishes()
    {
        PossibleToCookDishes = FindPossibleFoods(AvailableFood, inventoryProducts);
    }

    private void CheckAvailableFood()
    {
        AvailableFood = new List<Food>();
        for (int i = 0; i <= CurrentLevel.Number - 1; i++)
        {
            foreach (Food food in AllScriptableObjects.GetAllScriptableObjects<Level>()[i].OpenInThisLevelFoods)
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
            foreach (Weapon weapon in AllScriptableObjects.GetAllScriptableObjects<Level>()[i].OpenInThisLevelWeapons)
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

    public void SetPlayerInfo(SavingData savingData)
    {
        if (savingData != null)
        {
            currentLevel = AllScriptableObjects.GetAllScriptableObjects<Level>()[(int)(savingData.levelNumber - 1)];
            money = savingData.money;
            experience = savingData.experience;
            inventoryFoods = FindItemsByIDes(savingData.inventoryFoodsIDes, new List<Item>(AllScriptableObjects.GetAllScriptableObjects<Food>())).ConvertAll(item => (Food)item);
            inventoryProducts = FindItemsByIDes(savingData.inventoryProductsIDes, new List<Item>(AllScriptableObjects.GetAllScriptableObjects<Product>())).ConvertAll(item => (Product)item);
            inventoryWeapons = FindItemsByIDes(savingData.inventoryWeaponsIDes, new List<Item>(AllScriptableObjects.GetAllScriptableObjects<Weapon>())).ConvertAll(item => (Weapon)item);
            currentOrder = (Food)FindItemByID(savingData.currentOrderID, new List<Item>(AllScriptableObjects.GetAllScriptableObjects<Food>()));
            currentVisitorIndex = savingData.currentVisitorIndex;
        }
        PlayerStates.Instance.UpdateAllStatesUI();
        CheckAvailableFood();
        CheckAvailableWeapons();
    }
}