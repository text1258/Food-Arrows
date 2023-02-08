using UnityEngine;

public class LevelText : ResourceText
{
    public override void ShowText()
    {
        text.text = $"{phrase}{Player.Instance.CurrentLevel.Number}";
    }
}