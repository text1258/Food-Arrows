using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/Level/Level")]
public class NormalLevel : Level
{
    [SerializeField] private uint experienceToNextLevel;
    public uint ExperienceToNextLevel => experienceToNextLevel;
}