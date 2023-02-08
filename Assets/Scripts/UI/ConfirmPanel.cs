using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public abstract class ConfirmPanel : MonoBehaviour
    {
        [SerializeField] protected TMP_Text commentText;
        [SerializeField] protected string commentTextTitle;
        [SerializeField] protected GameObject confirmPanelGameObject;
        [SerializeField] protected Image confirmPanelImage;
        [SerializeField] protected Button agreeButton;
        public TMP_Text CommentText => commentText;
        public string CommentTextTitle => commentTextTitle;
        public GameObject ConfirmPanelGameObject => confirmPanelGameObject;
        public Image ConfirmPanelImage => confirmPanelImage;
        public Button AgreeButton => agreeButton;

        public virtual void Confirm() {}
    }
}