using System.Collections;
using UnityEngine;
using TMPro;

public class MessageText : MonoBehaviour
{
    [SerializeField] private TMP_Text textOfMessage;

    private void Awake()
    {
        textOfMessage.text = "";
    }

    public IEnumerator ShowMessage(string message, float messageTime)
    {
        textOfMessage.text = message;
        yield return new WaitForSeconds(messageTime);
        textOfMessage.text = "";
        yield break;
    }
}