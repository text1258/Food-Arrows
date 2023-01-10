using UnityEngine;

public class InstantiatedProduct : MonoBehaviour
{
    [HideInInspector] public Vector3 startPosition;

    private void Awake()
    {
        startPosition = transform.position;
    }
}