namespace NewsMobileApp.Models;

public class Article
{
    public Guid ArticleId { get; set; }

    public string Title { get; set; }

    public string Subtitle { get; set; }

    public Section Section { get; set; }

    public string Image { get; set; }

    public DateTime PublishTime {  get; set; }
}
