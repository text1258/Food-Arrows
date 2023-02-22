using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public class ConfirmPanel : MonoBehaviour
    {
        public static ConfirmPanel instance;

        [SerializeField] private TMP_Text titleText;
        [SerializeField] private Image illustration;
        [SerializeField] private Button confirmButton;
        [SerializeField] private Button disconfirmButton;

        private void Awake()
        {
            instance = this;
        }

        public void CreateConfirmPanel(string title, Sprite confirmImage, UnityAction onConfirm = null, UnityAction onDisConfirm = null)
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }
            titleText.text = title;
            illustration.sprite = confirmImage;
            confirmButton.onClick.RemoveAllListeners();
            disconfirmButton.onClick.RemoveAllListeners();
            if (onConfirm != null)
            {
                confirmButton.onClick.AddListener(onConfirm);
            }
            if (onDisConfirm != null)
            {
                disconfirmButton.onClick.AddListener(onDisConfirm);
            }
            confirmButton.onClick.AddListener(ClearConfirmPanel);
            disconfirmButton.onClick.AddListener(ClearConfirmPanel);
        }

        public void ClearConfirmPanel()
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
        }
    }
}