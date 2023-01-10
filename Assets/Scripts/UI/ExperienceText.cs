using UnityEngine;

public class ExperienceText : ResourceText
{ 
    [SerializeField] private Player player;
    public override void ShowText()
    {
        if (player.CurrentLevel is not LastLevel)
        {
            text.text = $"{phrase}{player.Experience}/{((NormalLevel)player.CurrentLevel).ExperienceToNextLevel}";
        }
        else
        {
            text.text = $"{phrase}{player.Experience}";
        }
    }
}