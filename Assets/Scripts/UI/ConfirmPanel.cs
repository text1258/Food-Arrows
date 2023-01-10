using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class ConfirmPanel : MonoBehaviour
{
    [SerializeField] protected TMP_Text commentText;
    [SerializeField] protected string commentTextTitle;
    [SerializeField] protected Player player;
    [SerializeField] public GameObject confirmPanelGameObject;
    [SerializeField] public Image confirmPanelImage;
    [SerializeField] public Button agreeButton;

    public TMP_Text CommentText => commentText;
    public string CommentTextTitle => commentTextTitle;

    public virtual void Confirm() {}
    
}