namespace Services;

public interface IAuthUsersService
{
    /// <summary>
    /// Gets paginated collection of registered users asynchronously from database.
    /// </summary>
    /// <param name="offset">Page number (from 0)</param>
    /// <param name="limit">Amount of objects in the page</param>
    /// <returns>
    /// A <see cref="Task"/> that represents the asynchronous operation. 
    /// The <see cref="Task"/> result contains a paginated collection of <see cref="User"/> objects.
    /// </returns>
    Task<IEnumerable<User>> RetrieveUsersAsync(int? offset, int? limit);

    /// <summary>
    /// Gets a user by ID asynchronously from database.
    /// </summary>
    /// <param name="id">User ID</param>
    /// <returns>
    /// A <see cref="Task"/> that represents the asynchronous operation. 
    /// The <see cref="Task"/> result contains found <see cref="User"/> object, 
    /// <see href="null"/> otherwise.
    /// </returns>
    Task<User?> RetrieveUserAsync(Guid id);

    /// <summary>
    /// Authorizes a user and gets him from database asynchronously.
    /// </summary>
    /// <param name="user"><see cref="AuthorizeModel"/> record with valid authorize data</param>
    /// <returns>
    /// A <see cref="Task"/> that represents the asynchronous operation. <br/>
    /// The <see cref="Task"/> result contains authorized <see cref="User"/> 
    /// object or <see href="null"/> - if invalid authorize data or user doesn't exist.
    /// </returns>
    Task<User?> AutorizeUserAsync(AuthorizeModel user);

    /// <summary>
    /// Registers a user and gets him from database asynchronously.
    /// </summary>
    /// <param name="user"><see cref="AuthorizeModel"/> record with valid register data</param>
    /// <returns>
    /// A <see cref="Task"/> that represents the asynchronous operation. 
    /// The <see cref="Task"/> result contains registered <see cref="User"/> object if success, 
    /// <see href="null"/> otherwise.
    /// </returns>
    Task<User?> RegisterUserAsync(RegisterModel user);

    /// <summary>
    /// Changes a user's password asynchronously.
    /// </summary>
    /// <param name="model"><see cref="ChangePasswdModel"/> record with old and new password</param>
    /// <returns>
    /// A <see cref="Task"/> that represents the asynchronous operation. 
    /// The <see cref="Task"/> result contains <see cref="User"/> object if success, 
    /// <see href="null"/> otherwise.
    /// </returns>
    Task<User?> ChangePasswordAsync(ChangePasswdModel model);

    /// <summary>
    /// Changes a user's password by admin claims asynchronously.
    /// </summary>
    /// <param name="model"><see cref="AuthorizeModel"/> record with new password</param>
    /// <returns>
    /// A <see cref="Task"/> that represents the asynchronous operation. 
    /// The <see cref="Task"/> result contains <see cref="User"/> object if success, 
    /// <see href="null"/> otherwise.
    /// </returns>
    Task<User?> ChangePasswordAdminAsync(AuthorizeModel model);

    /// <summary>
    /// Updates a user's data by admin claims asynchronously.
    /// </summary>
    /// <param name="id">User's ID</param>
    /// <param name="user"><see cref="User"/> object with new data</param>
    /// <returns>
    /// A <see cref="Task"/> that represents the asynchronous operation. 
    /// The <see cref="Task"/> result contains updated <see cref="User"/> object if success, 
    /// <see href="null"/> otherwise.
    /// </returns>
    Task<User?> UpdateUserAdminAsync(Guid id, User user);

    /// <summary>
    /// Updates a user's data asynchronously.
    /// </summary>
    /// <param name="id">User's ID</param>
    /// <param name="user"><see cref="User"/> object with new data</param>
    /// <returns>
    /// A <see cref="Task"/> that represents the asynchronous operation. 
    /// The <see cref="Task"/> result contains updated <see cref="User"/> object if success, 
    /// <see href="null"/> otherwise.
    /// </returns>
    Task<User?> UpdateUserAsync(Guid id, User user);

    /// <summary>
    /// Deletes a user asynchronously from database.
    /// </summary>
    /// <param name="id">User's ID</param>
    /// <returns>
    /// A <see cref="Task"/> that represents the asynchronous operation.
    /// The <see cref="Task"/> result is <see href="true"/> or <see href="false"/> 
    /// indicating success or failure respectively. <br/> If user doesn't even exist - <see href="null"/>
    /// </returns>
    Task<bool?> DeleteUserAsync(Guid id);
}
