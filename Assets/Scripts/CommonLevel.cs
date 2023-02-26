using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/Level/Level")]
public class CommonLevel : Level
{
    [SerializeField] private uint experienceToNextLevel;
    public uint ExperienceToNextLevel => experienceToNextLevel;
}