using System.Collections.Generic;

namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        public uint money = 500;
        public uint levelNumber = 1;
        public uint experience = 0;
        public List<string> inventoryFoodsIDes = new List<string>() { "2" };
        public List<string> inventoryProductsIDes = new List<string>() { "5", "6", "9" };
        public List<string> inventoryWeaponsIDes = new List<string> { "1" };
        public string currentOrderID = "";
        public string currentVisitorIndex = "";
    }
}
