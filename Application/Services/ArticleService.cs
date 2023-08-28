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
    /// Метод получения всех категорий новостей
    /// </summary>
    /// <returns>Все категории из базы данных</returns>
    public async Task<IEnumerable<Section>> RetrieveSectionsAsync() => await _newsAppContext.Sections.ToListAsync();

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

    /// <summary>
    /// Метод добавления категорий новостей в базу данных
    /// </summary>
    /// <param name="section">Объект Section</param>
    /// <returns>Секция, добавленная в базу данных, иначе null</returns>
    public async Task<Section?> AddSectionAsync(Section section)
    {
        await _newsAppContext.Sections.AddAsync(section);
        int affected = await _newsAppContext.SaveChangesAsync();
        if (affected == 1)
        {
            return await _newsAppContext.Sections.FindAsync(section.SectionId);
        }
        return null;
    }

    /// <summary>
    /// Метод обновления статьи по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор статьи</param>
    /// <param name="article">Объект Article</param>
    /// <returns>Article, иначе null</returns>
    public async Task<Article?> UpdateArticleAsync(Guid id, Article article)
    {
        Article? existingArticle = await _newsAppContext.Articles.FindAsync(id);
        if (existingArticle is not null)
        {
            _newsAppContext.Entry(existingArticle).CurrentValues.SetValues(article);
            int affected = await _newsAppContext.SaveChangesAsync();
            if (affected == 1) 
            {
                return await RetrieveArticleAsync(id);
            }
        }
        return null;
    }

    /// <summary>
    /// Метод обновления секции по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор секции</param>
    /// <param name="section">Объект Section</param>
    /// <returns>Section, иначе null</returns>
    public async Task<Section?> UpdateSectionAsync(int id, Section section)
    {
        Section? existingSection = await _newsAppContext.Sections.FindAsync(id);
        if (existingSection is not null) 
        {
            _newsAppContext.Entry(existingSection).CurrentValues.SetValues(section);
            int affected = await _newsAppContext.SaveChangesAsync();
            if (affected == 1)
            {
                return await _newsAppContext.Sections.FindAsync(id);
            }
        }
        return null;
    }

    /// <summary>
    /// Метод удаления статьи по идентификатору из базы данных
    /// </summary>
    /// <param name="id">Идентификатор статьи</param>
    /// <returns>Состояние операции, иначе null</returns>
    public async Task<bool?> DeleteArticleAsync(Guid id)
    {
        Article? article = await _newsAppContext.Articles.FindAsync(id);
        if (article is null)
        {
            return null;
        }
        _newsAppContext.Articles.Remove(article);
        int affected = await _newsAppContext.SaveChangesAsync();
        if (affected == 1)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Метод удаления секции по идентификатору, а также всех связанных с ней статей
    /// </summary>
    /// <param name="id">Идентификатор секции</param>
    /// <returns>Состояние операции, иначе null</returns>
    public async Task<bool?> DeleteSectionAsync(int id)
    {
        Section? section = await _newsAppContext.Sections.FindAsync(id);
        if(section is null) 
        {
            return null;
        }
        _newsAppContext.Sections.Remove(section);
        int affected = await _newsAppContext.SaveChangesAsync();
        if (affected == 1) 
        {
            return true;
        }
        return false;
    }
}