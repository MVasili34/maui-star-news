using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace ImagesCloudTool;

public class ImageCloudTool
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
            using (HttpClient httpClient = new())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Client-ID", _clientId);
                //httpClient.DefaultRequestHeaders.Add("Authorization", $"Client-ID {_clientId}");

                string? imageUrl = await UploadImgurAsync(httpClient, filePath);

                if (!string.IsNullOrEmpty(imageUrl))
                {
                    Console.WriteLine("Ответ: " + imageUrl);
                    dynamic? response = JsonConvert.DeserializeObject<dynamic>(imageUrl);
                    string url = response!.data.link;
                    return url;
                }
                else
                {
                    return null;
                }
            }
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

    static async Task<string?> UploadImgurAsync(HttpClient httpClient, string imagePath)
    {
        try
        {
            byte[] imageData = File.ReadAllBytes(imagePath);

            using (var content = new MultipartFormDataContent())
            {
                content.Add(new ByteArrayContent(imageData), "image", "image.jpg");

                var response = await httpClient.PostAsync(_url, content);

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    return responseContent;
                }
                else
                {
                    Console.WriteLine("Ошибка при загрузке изображения. Код ответа: " + response.StatusCode);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка: " + ex.Message);
        }

        return null;
    }
}