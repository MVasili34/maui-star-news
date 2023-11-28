namespace ImagesCloudTool;

public interface IImageCloudTool
{
    /// <summary>
    /// Uploads image to the cloud service - <see href="Imgur"/>
    /// </summary>
    /// <param name="filePath">Full path to the file</param>
    /// <returns><see cref="Uri"/>-address of image if uploaded, <see href="null"/> otherwise.</returns>
    Task<string?> UploadImageAsync(string filePath);
}
