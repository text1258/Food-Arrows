using UnityEngine;

public class ExperienceText : ResourceText
{
    public override void ShowText()
    {
        if (Player.Instance.CurrentLevel is not LastLevel)
        {
            text.text = $"{phrase}{Player.Instance.Experience}/{((NormalLevel)Player.Instance.CurrentLevel).ExperienceToNextLevel}";
        }
        else
        {
            text.text = $"{phrase}{Player.Instance.Experience}";
        }
    }
}