using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AllLevels", menuName = "ScriptableObjects/AllLevels", order = 0)]
public class AllLevels : ScriptableObject
{
    [SerializeField] private List<Level> levels;
    public List<Level> Levels => levels;
}