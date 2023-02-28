using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(GridLayoutGroup))]
public class LayoutElementCostantCount : MonoBehaviour
{
    [SerializeField] private int elemensCount;
    [SerializeField] private int padding;
    [SerializeField] private float spacing;

    private void OnValidate()
    {
        if (elemensCount < 1)
        {
            elemensCount = 1;
        }
        if (padding < 0)
        {
            padding = 0;
        }
        if (spacing < 0)
        {
            spacing = 0;
        }
        ResizeLayoutElemets();
    }

    private void OnEnable()
    {
        ResizeLayoutElemets();
    }

    private void Start()
    {
        ResizeLayoutElemets();
    }

    private void OnRectTransformDimensionsChange()
    {
        ResizeLayoutElemets();
    }

    private void ResizeLayoutElemets()
    {
        GridLayoutGroup gridLayout = GetComponent<GridLayoutGroup>();
        gridLayout.padding.right = padding;
        gridLayout.padding.left = padding;
        gridLayout.padding.top = padding;
        gridLayout.padding.bottom = padding;
        gridLayout.spacing = new Vector2(spacing, spacing);
        float cellSize = ((GetComponent<RectTransform>().rect.width) - (padding * 2) - (spacing * (elemensCount - 1))) / elemensCount;
        gridLayout.cellSize = new Vector2(cellSize, cellSize);
    }
}
