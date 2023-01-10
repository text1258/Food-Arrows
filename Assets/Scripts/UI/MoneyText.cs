using UnityEngine;

public class MoneyText : ResourceText
{
    [SerializeField] private Player player;
    public override void ShowText()
    {
        text.text = $"{phrase}{player.Money}";
    }
}