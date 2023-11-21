using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace ImagesCloudTool;

public class ImageCloudTool : IImageCloudTool
{
    private const string _url = "https://api.imgur.com/3/image";
    private readonly string _clientId = null!;

    public ImageCloudTool(string clientId) => _clientId = clientId;

    public async Task<string?> UploadImageAsync(string filePath)
    {
        try
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException();
            }
            using HttpClient httpClient = new();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Client-ID", _clientId);

            string? imageUrl = await UploadImgurAsync(httpClient, filePath);

            if (!string.IsNullOrEmpty(imageUrl))
            {
                dynamic? response = JsonConvert.DeserializeObject<dynamic>(imageUrl);
                string url = response!.data.link;
                return url;
            }
            return null;
        }
        catch (FileNotFoundException)
        {
            return null;
        }
        catch (Exception)
        {
            return null;
        }
    }

    private async Task<string?> UploadImgurAsync(HttpClient httpClient, string imagePath)
    {
        byte[] imageData = File.ReadAllBytes(imagePath);

        using MultipartFormDataContent content = new()
        {
            { new ByteArrayContent(imageData), "image", "image.jpg" }
        };

        var response = await httpClient.PostAsync(_url, content);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsStringAsync();
        }
        return null;
    }
}