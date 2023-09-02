using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using EntityModels;
using Services;

namespace ArticleODataAPI.Controllers;

public class ArticlesController : ODataController
{
    private readonly IArticleService _articleService;

    public ArticlesController(IArticleService articleService)
    {
        _articleService = articleService;
    }

    [EnableQuery]
    public async Task<IEnumerable<Article>> GetArticles() => await _articleService.RetrieveArticlesAsync();

    [EnableQuery]
    [ProducesResponseType(200,Type = typeof(Article))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetArticle(Guid key)
    {
        Article? article = await _articleService.RetrieveArticleAsync(key);
        if(article is null)
        {
            return NotFound();
        }
        return Ok(article);
    }

    //POST: api/Articles
    //BODY: Client (JSON)
    [HttpPost]
    [ProducesResponseType(201, Type = typeof(Article))]
    [ProducesResponseType(400)]
    public async Task<IActionResult> PostArticle([FromBody] Article article)
    {
        if (article is null) 
        {
            return BadRequest();
        }
        Article? addedArticle = await _articleService.AddArticleAsync(article);
        if (addedArticle is null) 
        {
            return BadRequest("Сервис не смог добавить статью");
        }
        return Created(addedArticle);
    }

    //PUT: api/Articles/[id]
    //BODY: Article (JSON)
    [HttpPut]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> PutArticle(Guid key, [FromBody] Article article)
    {
        if(article is null || article.ArticleId != key)
        {
            return BadRequest();
        }

        Article? existed = await _articleService.RetrieveArticleAsync(key);
        if(existed is null)
        {
            return NotFound();
        }

        await _articleService.UpdateArticleAsync(key, article);
        return NoContent();
    }

    //DELETE: api/Articles/[key]
    [HttpDelete]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> DeleteArticle(Guid key)
    {
        Article? existed = await _articleService.RetrieveArticleAsync(key);
        if(existed is null)
        {
            return NotFound();
        }

        bool? deleted = await _articleService.DeleteArticleAsync(key);
        if(deleted.HasValue && deleted.Value)
        {
            return NoContent();
        }
        return BadRequest($"При удалении статьи {key} произошла ошибка");

    }
}
