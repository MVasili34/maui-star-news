namespace Services;

public interface IArticleService
{
    Task<Article?> RetrieveArticleAsync(Guid id);
    Task<IEnumerable<Article>> RetrieveArticlesAsync();
    Task<IEnumerable<Section>> RetrieveSectionsAsync();
    Task<Article?> AddArticleAsync(Article article);
    Task<Section?> AddSectionAsync(Section section);
    Task<Article?> UpdateArticleAsync(Guid id, Article article);
    Task<Section?> UpdateSectionAsync(int id, Section section);
    Task<bool?> DeleteArticleAsync(Guid id);
    Task<bool?> DeleteSectionAsync(int id);
}
