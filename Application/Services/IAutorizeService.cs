namespace Services;

public interface IAutorizeService
{
    /// <summary>
    /// Метод постраничного получения пользователей из базы данных
    /// </summary>
    /// <param name="offset">Номер страницы (начиная с 0)</param>
    /// <param name="limit">Число пользователей на странице</param>
    /// <returns><see langword="limit" /> пользователей с <see langword="offset" /> страницы</returns>
    Task<IEnumerable<User>> RetrieveUsersAsync(int? offset, int? limit);

    /// <summary>
    /// Метод получения пользователя по идентификатору из базы данных
    /// </summary>
    /// <param name="id">Идентификатор пользователя</param>
    /// <returns>Объект <see cref="User" />, иначе <see langword="null" /></returns>
    Task<User?> RetrieveUserAsync(Guid id);

    /// <summary>
    /// Метод авторизации пользователя
    /// </summary>
    /// <param name="user">Объект <see cref="User"/> с вложенным паролем</param>
    /// <returns>Если данные верны <see langword="true"/>, иначе <see langword="false"/>.
    /// Если же пользователя не существуюет вовсе, то <see langword="null"/></returns>
    Task<bool?> AutorizeUserAsync(AuthorizeModel user);

    /// <summary>
    /// Метод регистрации пользователя по предоставленным данным
    /// </summary>
    /// <param name="user">Запись <see cref="RegisterModel"/> с вложенным паролем</param>
    /// <returns>Объект <see cref="User"/>, если регистрация успешно, иначе <see langword="null"/></returns>
    Task<User?> RegisterUserAsync(RegisterModel user);

    /// <summary>
    /// Метод обновления данных пользователя
    /// </summary>
    /// <param name="id">Идентификатор пользователя</param>
    /// <param name="user">Объект <see cref="User"/></param>
    /// <returns><see cref="User"/>, если данные обновлены, иначе <see langword="null"/></returns>
    Task<User?> UpdateUserAsync(Guid id, User user);

    /// <summary>
    /// Метод удаления пользователя по идентификатору 
    /// </summary>
    /// <param name="id">Идентификатор пользователя</param>
    /// <returns>Если пользователь удалён, то <see langword="true"/>, иначе <see langword="false"/>.
    /// Если же пользователя не существуюет вовсе, то <see langword="null"/></returns>
    Task<bool?> DeleteUserAsync(Guid id);
}
