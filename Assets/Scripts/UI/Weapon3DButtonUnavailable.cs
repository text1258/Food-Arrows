using UnityEngine;

public class Weapon3DButtonUnavailable : Weapon3DButton
{
    public override void OnItemCellClick()
    {
        MessageText.Instance.Message("This weapon is unavailable in current level", 1.5f);
    }
}