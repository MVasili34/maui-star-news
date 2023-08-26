using Microsoft.EntityFrameworkCore;

namespace Services;

public class ArticleService : IArticleService
{
    private readonly NewsAppDbContext _newsAppContext;

    public ArticleService(NewsAppDbContext newsAppContext)
    {
        _newsAppContext = newsAppContext;
    }

    /// <summary>
    /// Метод получения статьи по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор</param>
    /// <returns>Статья из базы данных</returns>
    public async Task<Article?> RetrieveArticleAsync(Guid id) => await _newsAppContext
        .Articles.FindAsync(id);

    /// <summary>
    /// Метод получения всех статей из базы данных
    /// (для реализации OData сервиса)
    /// </summary>
    /// <returns>Все статьи из базы данных</returns>
    public async Task<IEnumerable<Article>> RetrieveArticlesAsync() => await _newsAppContext.Articles.ToListAsync();

    /// <summary>
    /// Метод добавления статьи в базу данных
    /// </summary>
    /// <param name="article">Объект Article</param>
    /// <returns>Статья, добавленная в базу данных, иначе null</returns>
    public async Task<Article?> AddArticleAsync(Article article)
    {
        await _newsAppContext.Articles.AddAsync(article);
        int affected = await _newsAppContext.SaveChangesAsync();
        if (affected == 1)
        {
            return await RetrieveArticleAsync(article.ArticleId);
        }
        return null;
    }
}