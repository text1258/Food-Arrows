using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class ItemsPannel : MonoBehaviour
{
    [SerializeField] protected TMP_Text title;
    [SerializeField] protected ScrollRect itemsScrollRect;

    public virtual void CreateItemsPanel() {}

    public virtual void ClearItemsPanel()
    {
        foreach (Transform child in itemsScrollRect.content)
        {
            Destroy(child.gameObject);
        }
    }
}