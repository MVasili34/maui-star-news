namespace Services;

public interface IArticleService
{
    /// <summary>
    /// Gets required article by id asynchronously from database.
    /// </summary>
    /// <param name="id">Article ID</param>
    /// <returns>
    /// A <see cref="Task"/> that represents the asynchronous operation. 
    /// The <see cref="Task"/> result contains found <see cref="Article"/> object, 
    /// <see href="null"/> otherwise.
    /// </returns>
    Task<Article?> RetrieveArticleAsync(Guid id);

    /// <summary>
    /// Gets all articles from database asynchronously.
    /// </summary>
    /// <returns>Collection of <see cref="Article"/> objects.</returns>
    Task<IEnumerable<Article>> RetrieveArticlesAsync();

    /// <summary>
    /// Gets all sections from database asynchronously.
    /// </summary>
    /// <returns>Collection of <see cref="Section"/> objects.</returns>
    Task<IEnumerable<Section>> RetrieveSectionsAsync();

    /// <summary>
    /// Publishes an article and get's this article from database asynchronously.
    /// </summary>
    /// <param name="article"><see cref="Article"/> object needs to pe published</param>
    /// <returns>
    /// A <see cref="Task"/> that represents the asynchronous operation. 
    /// The <see cref="Task"/> result contains published <see cref="Article"/> object, 
    /// <see href="null"/> otherwise.
    /// </returns>
    Task<Article?> AddArticleAsync(Article article);

    /// <summary>
    /// Adds new section to database asynchronously.
    /// </summary>
    /// <param name="section"><see cref="Section"/> object</param>
    /// <returns>
    /// A <see cref="Task"/> that represents the asynchronous operation. 
    /// The <see cref="Task"/> result contains added <see cref="Section"/> object, 
    /// <see href="null"/> otherwise.
    /// </returns>
    Task<Section?> AddSectionAsync(Section section);

    /// <summary>
    /// Updates article by ID in database asynchronously.
    /// </summary>
    /// <param name="id">Article ID</param>
    /// <param name="article">Article object</param>
    /// <returns>
    /// A <see cref="Task"/> that represents the asynchronous operation. 
    /// The <see cref="Task"/> result contains updated <see cref="Article"/> object, 
    /// <see href="null"/> otherwise.
    /// </returns>
    Task<Article?> UpdateArticleAsync(Guid id, Article article);

    /// <summary>
    /// Updates section by ID in database asynchronously.
    /// </summary>
    /// <param name="id">Section ID</param>
    /// <param name="section">Section object</param>
    /// <returns>
    /// A <see cref="Task"/> that represents the asynchronous operation. 
    /// The <see cref="Task"/> result contains updated <see cref="Section"/> object, 
    /// <see href="null"/> otherwise.
    /// </returns>
    Task<Section?> UpdateSectionAsync(int id, Section section);

    /// <summary>
    /// Deletes an article asynchronously from database.
    /// </summary>
    /// <param name="id">Article ID</param>
    /// <returns>
    /// A <see cref="Task"/> that represents the asynchronous operation.
    /// The <see cref="Task"/> result is <see href="true"/> or <see href="false"/> 
    /// indicating success or failure respectively.
    /// </returns>
    Task<bool?> DeleteArticleAsync(Guid id);

    /// <summary>
    /// Deletes a section asynchronously from database.
    /// </summary>
    /// <param name="id">Section ID</param>
    /// <returns>
    /// A <see cref="Task"/> that represents the asynchronous operation.
    /// The <see cref="Task"/> result is <see href="true"/> or <see href="false"/> 
    /// indicating success or failure respectively.
    /// </returns>
    Task<bool?> DeleteSectionAsync(int id);
}
