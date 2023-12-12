using Microsoft.EntityFrameworkCore;

namespace Services;

public class ArticleService : IArticleService
{
    private readonly NewsAppDbContext _newsAppContext;

    public ArticleService(NewsAppDbContext newsAppContext)
    {
        _newsAppContext = newsAppContext;
    }

    public async Task<Article?> RetrieveArticleAsync(Guid id)
    {
        Article? article = await _newsAppContext.Articles.FindAsync(id);
        if(article is not null) 
        {
            article.Views++;
            await _newsAppContext.SaveChangesAsync();
        }
        return article;
    }

    public async Task<IEnumerable<Section>> RetrieveSectionsAsync() => await _newsAppContext.Sections.ToListAsync();

    public async Task<IEnumerable<Article>> RetrieveArticlesAsync() => await _newsAppContext.Articles
        .Include(x => x.Section)
        .ToListAsync();

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

    public async Task<Article?> UpdateArticleAsync(Guid id, Article article)
    {
        Article? existingArticle = await _newsAppContext.Articles.FindAsync(id);
        if (existingArticle is not null)
        {
            existingArticle.Title = article.Title;
            existingArticle.Subtitle = article.Subtitle;
            existingArticle.SectionId = article.SectionId;
            existingArticle.Image = article.Image;
            existingArticle.Text = article.Text;
            existingArticle.PublisherId = article.PublisherId;

            int affected = await _newsAppContext.SaveChangesAsync();
            if (affected == 1) 
            {
                return await RetrieveArticleAsync(id);
            }
        }
        return null;
    }

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

    public async Task<bool?> DeleteSectionAsync(int id)
    {
        Section? section = await _newsAppContext.Sections.FindAsync(id);
        if(section is null) 
        {
            return null;
        }
        _newsAppContext.Sections.Remove(section);
        int affected = await _newsAppContext.SaveChangesAsync();
        if (affected >= 1) 
        {
            return true;
        }
        return false;
    }
}