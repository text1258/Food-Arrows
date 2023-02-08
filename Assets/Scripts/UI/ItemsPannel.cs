using UnityEngine;

public abstract class ItemsPannel : MonoBehaviour
{
    public virtual void CreateItemsPanel() {}
    public virtual void ClearItemsPanel() {}

    public void UpdateItemsPanel()
    {
        ClearItemsPanel();
        CreateItemsPanel();
    }
}