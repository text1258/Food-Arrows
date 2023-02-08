using TMPro;
using UnityEngine;

public class ResourceText : MonoBehaviour
{
    [SerializeField] protected TMP_Text text;
    [SerializeField] protected string phrase;

    public virtual void ShowText() { }
}