namespace ImagesCloudTool;

public interface IImageCloudTool
{
    /// <summary>
    /// Метод загрузки изображений на облачный сервис - <see href="Imgur"/>
    /// </summary>
    /// <param name="filePath">Полный путь к файлу</param>
    /// <returns><see cref="Uri"/>-адрес изображения, если загрузка прошла успешно, иначе <see href="null"/></returns>
    Task<string?> UploadImageAsync(string filePath);
}
