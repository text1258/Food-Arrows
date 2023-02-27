using UnityEngine;

public abstract class AdVideoRevarder : MonoBehaviour
{

    public void ShowAdVideo()
    {
        Reward();
    }

    private void Reward()
    {
        GiveReward();
        ClearReward();
    }

    private void ClearReward() { }

    protected virtual void GiveReward() { }
}
