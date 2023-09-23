namespace NewsMobileApp.Models;

public class Article
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string SubTitle { get; set; }
    public string Text { get; set; }
    public string ImageURL { get; set; }
    public string Category { get; set; }
    public DateTime PublishDate { get; set; }
    public Article(int id, string title, string subTitle, string text, string imageURL,
        string category, DateTime publishDate)
    {
        Id = id;
        Title = title;
        SubTitle = subTitle;
        Text = text;
        ImageURL = imageURL;
        Category = category;
        PublishDate = publishDate;
    }
}
