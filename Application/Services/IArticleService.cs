namespace Services;

public interface IArticleService
{
    /// <summary>
    /// Метод получения статьи по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор</param>
    /// <returns>Объект <see cref="Article" />, иначе <see langword="null" /></returns>
    Task<Article?> RetrieveArticleAsync(Guid id);

    /// <summary>
    /// Метод получения всех статей из базы данных
    /// (для реализации OData сервиса)
    /// </summary>
    /// <returns>Все статьи из базы данных</returns>
    Task<IEnumerable<Article>> RetrieveArticlesAsync();

    /// <summary>
    /// Метод получения всех категорий новостей
    /// </summary>
    /// <returns>Все категории из базы данных</returns>
    Task<IEnumerable<Section>> RetrieveSectionsAsync();

    /// <summary>
    /// Метод добавления статьи в базу данных
    /// </summary>
    /// <param name="article">Объект Article</param>
    /// <returns>Объект <see cref="Article" />, иначе <see langword="null" /></returns>
    Task<Article?> AddArticleAsync(Article article);

    /// <summary>
    /// Метод добавления категорий новостей в базу данных
    /// </summary>
    /// <param name="section">Объект Section</param>
    /// <returns>Объект <see cref="Section" />, иначе <see langword="null" /></returns>
    Task<Section?> AddSectionAsync(Section section);

    /// <summary>
    /// Метод обновления статьи по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор статьи</param>
    /// <param name="article">Объект Article</param>
    /// <returns>Объект <see cref="Article" />, иначе <see langword="null" /></returns>
    Task<Article?> UpdateArticleAsync(Guid id, Article article);

    /// <summary>
    /// Метод обновления секции по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор секции</param>
    /// <param name="section">Объект Section</param>
    /// <returns>Объект <see cref="Section" />, иначе <see langword="null" /></returns>
    Task<Section?> UpdateSectionAsync(int id, Section section);

    /// <summary>
    /// Метод удаления статьи по идентификатору из базы данных
    /// </summary>
    /// <param name="id">Идентификатор статьи</param>
    /// <returns>Состояние операции, иначе <see langword="null" /></returns>
    Task<bool?> DeleteArticleAsync(Guid id);

    /// <summary>
    /// Метод удаления секции по идентификатору, а также всех связанных с ней статей
    /// </summary>
    /// <param name="id">Идентификатор секции</param>
    /// <returns>Состояние операции, иначе <see langword="null" /></returns>
    Task<bool?> DeleteSectionAsync(int id);
}
