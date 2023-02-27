using System.Collections.Generic;

[System.Serializable]
public class PlayerData
{
    public uint money;
    public uint levelNumber;
    public uint experience;
    public List<string> inventoryFoodsIDes;
    public List<string> inventoryProductsIDes;
    public List<string> inventoryWeaponsIDes;
    public string currentOrderID;
    public string currentVisitorIndex;

    public PlayerData(uint money, uint levelNumber, uint experience, List<string> inventoryFoodsIDes, List<string> inventoryProductsIDes,
        List<string> inventoryWeaponsIDes, string currentOrderID, string currentVisitorIndex)
    {
        this.money = money;
        this.levelNumber = levelNumber;
        this.experience = experience;
        this.inventoryFoodsIDes = inventoryFoodsIDes;
        this.inventoryProductsIDes = inventoryProductsIDes;
        this.inventoryWeaponsIDes = inventoryWeaponsIDes;
        this.currentOrderID = currentOrderID;
        this.currentVisitorIndex = currentVisitorIndex;
    }

    public PlayerData(uint money, uint levelNumber, uint experience, List<string> inventoryFoodsIDes, List<string> inventoryProductsIDes,
        List<string> inventoryWeaponsIDes)
    {
        this.money = money;
        this.levelNumber = levelNumber;
        this.experience = experience;
        this.inventoryFoodsIDes = inventoryFoodsIDes;
        this.inventoryProductsIDes = inventoryProductsIDes;
        this.inventoryWeaponsIDes = inventoryWeaponsIDes;
    }
}
