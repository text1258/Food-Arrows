public class ExperienceText : ResourceText
{
    public override void ShowText()
    {
        if (Player.instance.CurrentLevel is not LastLevel)
        {
            text.text = $"Опыт: {Player.instance.Experience}/{((CommonLevel)Player.instance.CurrentLevel).ExperienceToNextLevel}";
        }
        else
        {
            text.text = $"Опыт: {Player.instance.Experience}";
        }
    }
}