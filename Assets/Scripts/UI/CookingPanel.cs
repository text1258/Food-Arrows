using UnityEngine;
using UnityEngine.UI;

public class CookingPanel : ItemsPannel
{
    [SerializeField] private ScrollRect cookingPanel;
    [SerializeField] private Button cookingPanelCellPrefab;
    [SerializeField] private CookingConfirmPanel confirmPanel;
    
    public override void CreateItemsPanel()
    {
        player.UpdateFindPossibleDishes();
        foreach (Food food in player.possibleToCookDishes)
        {
            Button currentButton = Instantiate(cookingPanelCellPrefab, parent: cookingPanel.content.transform);
            currentButton.image.sprite = food.Picture;
            CookingPanelCell currentCookingPanelCell = currentButton.GetComponent<CookingPanelCell>(); 
            if (currentCookingPanelCell != null)
            {
                currentCookingPanelCell.confirmPanel = confirmPanel;
                currentCookingPanelCell.cellFood = food;
            }
            else
            {
                Debug.LogError("On cell button must be CookingPanelCell");
            }
        }
    }

    public override void ClearItemsPanel()
    {
        for (int i = 0; i < cookingPanel.content.transform.childCount; i++)
        {
            Transform currentTransform = cookingPanel.content.transform.GetChild(i);
            Destroy(currentTransform.gameObject);
        }
    }
}