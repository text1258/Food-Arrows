public class LevelText : ResourceText
{
    public override void ShowText()
    {
        text.text = $"Уровень: {Player.instance.CurrentLevel.Number}";
    }
}