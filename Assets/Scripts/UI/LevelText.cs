using UnityEngine;

public class LevelText : ResourceText
{ 
    [SerializeField] private Player player;
    public override void ShowText()
    {
        text.text = $"{phrase}{player.CurrentLevel.Number}";
    }
}