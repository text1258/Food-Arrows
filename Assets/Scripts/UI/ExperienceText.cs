public class ExperienceText : ResourceText
{
    public override void ShowText()
    {
        if (Player.Instance.CurrentLevel is not LastLevel)
        {
            text.text = $"Опыт: {Player.Instance.Experience}/{((NormalLevel)Player.Instance.CurrentLevel).ExperienceToNextLevel}";
        }
        else
        {
            text.text = $"Опыт: {Player.Instance.Experience}";
        }
    }
}