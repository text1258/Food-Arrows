using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AllFoods", menuName = "ScriptableObjects/AllItems/AllFoods")]
public class AllFoods : AllItems
{
    [SerializeField] private List<Food> foods;

    public List<Food> Foods => foods;
}