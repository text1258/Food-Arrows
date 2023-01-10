using TMPro;
using UnityEngine;

public abstract class ResourceText : MonoBehaviour
{
    [SerializeField] protected TMP_Text text;
    [SerializeField] protected string phrase;

    public virtual void ShowText() { }
}