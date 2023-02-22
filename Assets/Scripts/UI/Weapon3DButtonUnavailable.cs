using UnityEngine;

public class Weapon3DButtonUnavailable : Weapon3DButton
{
    public override void OnItemCellClick()
    {
        MessageText.instance.Message("Это ещё не доступно на вашем уровне", 1.5f);
    }
}