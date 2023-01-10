using UnityEngine;

public class AddMoneyForAdvertisingButton : ResourceText
{
    [SerializeField] private Player player;

    public override void ShowText()
    {
        text.text = $"+ {player.CurrentLevel.MoneyForAdvertisingCount}{phrase}";
    }
}