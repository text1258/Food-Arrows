using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UI
{
    public class InfoPanel : MonoBehaviour
    {
        public static InfoPanel Instance;
        [SerializeField] private ScrollRect InfoPanelScrollRect;
        [SerializeField] private Image imagePrefab;
        [SerializeField] private TMP_Text title;

        private void Awake()
        {
            Instance = this;
        }

        public void ShowInfoPanel(string description, params Sprite[] itemsSprites)
        {
            title.text = description;
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }
            foreach (Sprite currentSprite in itemsSprites)
            {
                Instantiate(imagePrefab, parent: InfoPanelScrollRect.content.transform).GetComponent<Image>().sprite = currentSprite;
            }
        }

        public void ClearInfoPanel()
        {
            foreach (Transform child in InfoPanelScrollRect.content.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}