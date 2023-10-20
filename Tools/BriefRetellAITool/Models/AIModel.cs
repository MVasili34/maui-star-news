namespace BriefRetellAITool.Models;

internal class AIModel
{
    public string Model { get; set; } = "gpt-3.5-turbo";

    public Messages[] Messages { get; set; } = null!;

    public string Stop { get; set; } = "[\n]";
}
