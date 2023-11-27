namespace NewsMobileApp.Models;

public class Section
{
    public int SectionId { get; set; }
    public string Name { get; set; }
    public string MaterialIcon { get; set; }
    public string Description { get; set; } = string.Empty;
}
