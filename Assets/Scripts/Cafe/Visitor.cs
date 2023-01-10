using UnityEngine;

public class Visitor : MonoBehaviour
{
    [SerializeField] private string visitorName;
    [SerializeField] private float speed;
    [SerializeField] private Animator visitorAnimator;
    [HideInInspector] public bool isSatisfied = false;
    [HideInInspector] public Food order;
    private float visitorSpeed;

    private void Awake()
    {
        visitorSpeed = Speed;
    }

    public string VisitorName => visitorName;
    public float Speed => speed;

    public void StartMove()
    {
        visitorAnimator.SetBool("isMove", true);
    }

    public void Stand()
    {
        visitorAnimator.SetBool("isMove", false);
    }

    public void StopMoving()
    {
        speed = 0f;
    }

    public void ContinueMoving()
    {
        speed = visitorSpeed;
    }
}