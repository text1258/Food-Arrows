using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemsPannel : MonoBehaviour
{
    public static ItemsPannel Instance;

    [SerializeField] protected TMP_Text titleText;
    [SerializeField] protected ScrollRect itemsScrollRect;

    private void Awake()
    {
        Instance = this;
    }

    public void CreateItemsPanel(string title) 
    {
        foreach (Transform child in transform) 
        { 
            child.gameObject.SetActive(true);
        }
        titleText.text = title;
    }

    public void AddItemToPanel(GameObject item)
    {
        Instantiate(item, parent: itemsScrollRect.content);
    }

    public void ClearItemsPanel()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        foreach (Transform child in itemsScrollRect.content)
        {
            Destroy(child.gameObject);
        }
    }
}