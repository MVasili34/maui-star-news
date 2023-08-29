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
}
