using System.Collections;
using UnityEngine;
using TMPro;

public class MessageText : MonoBehaviour
{
    public static MessageText instance;

    [SerializeField] private TMP_Text textMessage;

    private Coroutine showMessageCoroutine;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        HideMessage();
    }

    public void Message(string message, float messageTime)
    {
        if (showMessageCoroutine != null) 
        { 
            StopCoroutine(showMessageCoroutine);
            HideMessage();
        }
        showMessageCoroutine = StartCoroutine(ShowMessage(message, messageTime));
    }

    private IEnumerator ShowMessage(string message, float messageTime)
    {
        textMessage.text = message;
        float currentTime = 0f;
        while (currentTime < 1f)
        {
            textMessage.color = new Color(textMessage.color.r, textMessage.color.g, textMessage.color.b, currentTime);
            currentTime += Time.deltaTime;
        }
        currentTime = 0f;
        textMessage.color = new Color(textMessage.color.r, textMessage.color.g, textMessage.color.b, 1f);
        yield return new WaitForSeconds(messageTime);
        while (currentTime < 1f)
        {
            textMessage.color = new Color(textMessage.color.r, textMessage.color.g, textMessage.color.b, 1f - currentTime);
            currentTime += Time.deltaTime;
        }
        HideMessage();
        yield break;
    }

    private void HideMessage()
    {
        textMessage.color = new Color(textMessage.color.r, textMessage.color.g, textMessage.color.b, 0f);
        textMessage.text = "";
    }
}