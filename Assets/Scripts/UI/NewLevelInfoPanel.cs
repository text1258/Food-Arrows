using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class NewLevelInfoPanel : MonoBehaviour
    {
        public static NewLevelInfoPanel Instance;
        [SerializeField] private ScrollRect levelInfoPanelScrollRect;
        [SerializeField] private Image imagePrefab;

        private void Awake()
        {
            Instance = this;
        }

        public void ShowLevelInfoPanel()
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }
            foreach (Food currentFood in Player.Instance.CurrentLevel.OpenInThisLevelFoods)
            {
                Instantiate(imagePrefab, parent:levelInfoPanelScrollRect.content.transform).GetComponent<Image>().sprite = currentFood.Picture;
            }
            foreach (Product currentProduct in Player.Instance.CurrentLevel.OpenInThisLevelProducts)
            {
                Instantiate(imagePrefab, parent:levelInfoPanelScrollRect.content.transform).GetComponent<Image>().sprite = currentProduct.Picture;
            }
            foreach (Weapon currentWeapon in Player.Instance.CurrentLevel.OpenInThisLevelWeapons)
            {
                Instantiate(imagePrefab, parent:levelInfoPanelScrollRect.content.transform).GetComponent<Image>().sprite = currentWeapon.Picture;
            }
        }
    }
}