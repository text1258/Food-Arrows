using UnityEngine;

public class TargetRewardMoneyText : ResourceText
{
    [HideInInspector] public Target currentTarget;
    
    public override void ShowText()
    {
        text.text = $"{phrase}{currentTarget.RewardMoney}";
    }

    private void Update()
    {
        transform.position = currentTarget.transform.position;
    }
}