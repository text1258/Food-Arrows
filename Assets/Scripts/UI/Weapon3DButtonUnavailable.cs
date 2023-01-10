using UnityEngine;

public class Weapon3DButtonUnavailable : Weapon3DButton
{
    [HideInInspector] public MessageText messageText;
    private Coroutine currentShowMessage;
    
    public override void OnItemCellClick()
    {
        if (currentShowMessage != null)
        {
            StopCoroutine(currentShowMessage);
        }
        currentShowMessage = StartCoroutine(messageText.ShowMessage("This weapon is unavailable in current level", 1.5f));
    }
}