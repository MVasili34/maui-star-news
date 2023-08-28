using Services;

namespace ServicesTests.Tests;

public class ArticleServiceTests
{
    private readonly IArticleService _articleService;

    public ArticleServiceTests(IArticleService articleService)
    {
        _articleService = articleService;
    }

    [Fact]
    public async Task AddingArticleTest()
    {
        Article article = DataGenerator.GenerateArticle();

        Article? added = await _articleService.AddArticleAsync(article);

        Assert.Equal(article, added);
    }

    [Fact]
    public async Task AddingSectionTest()
    {
        Section section = DataGenerator.GenerateSection();

        Section? added = await _articleService.AddSectionAsync(section);

        Assert.Equal(section, added);
    }

    [Fact]
    public async Task EditingArticleByIdTest()
    {
        Article expected = DataGenerator.GenerateArticle();
        await _articleService.AddArticleAsync(expected);

        expected.Text = DateTime.Now.ToString();
        Article? actual = await _articleService.UpdateArticleAsync(expected.ArticleId, expected);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task EditingSectionByIdTest()
    {
        Section expected = DataGenerator.GenerateSection();
        await _articleService.AddSectionAsync(expected);

        expected.Description = DateTime.Now.ToString();
        Section? actual = await _articleService.UpdateSectionAsync(expected.SectionId, expected);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task DeleteArticleTest()
    {
        Article generated = DataGenerator.GenerateArticle();
        await _articleService.AddArticleAsync(generated);

        bool? condition = await _articleService.DeleteArticleAsync(generated.ArticleId);

        Assert.True(condition);
    }

    [Fact]
    public async Task DeleteSectionTest()
    {
        Section generated = DataGenerator.GenerateSection();
        await _articleService.AddSectionAsync(generated);

        bool? condition = await _articleService.DeleteSectionAsync(generated.SectionId);

        Assert.True(condition);
    }
}
