using UnityEngine;

public abstract class ItemsPannel : MonoBehaviour
{
    [SerializeField] protected Player player;
    public virtual void CreateItemsPanel() {}
    public virtual void ClearItemsPanel() {}

    public void UpdateItemsPanel()
    {
        ClearItemsPanel();
        CreateItemsPanel();
    }
}