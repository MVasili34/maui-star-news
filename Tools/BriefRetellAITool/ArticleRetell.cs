using System.Text;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using BriefRetellAITool.Models;
using System.Net;

namespace BriefRetellAITool;

public class ArticleRetell
{
    private string _apiKey = null!;

    public ArticleRetell(string apiKey) => _apiKey = apiKey;

    /// <summary>
    /// Метод получения краткого пересказа статьи 
    /// </summary>
    /// <param name="articleText">Не пустой текст статьи типа <see cref="string"/></param>
    /// <returns>Пересказ статьи, иначе <see href="null"/></returns>
    /// <exception cref="ArgumentNullException">Если текст статьи пустой</exception>
    /// <exception cref="KeyNotFoundException">Проблемы авторизации OpenAI</exception>
    /// <exception cref="OverflowException">Проблемы ключа OpenAI</exception>
    /// <exception cref="Exception">Прочие проблемы</exception>
    public async Task<string?> ConvertArticle(string articleText)
    {
        if(string.IsNullOrWhiteSpace(articleText))
        {
            throw new ArgumentNullException(nameof(articleText));
        }

        AIModel aIModel = new()
        {
            Messages = new Messages[] {
                new() { Role = "user", Content = "Преобразуй текст статьи так, чтобы новый текст по пунктам излагал краткую суть статьи: "+
                        articleText
                    }
                }
        };

        var serializeOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };
        string serialized = JsonSerializer.Serialize(aIModel, serializeOptions);

        using (StringContent content = new(serialized, Encoding.UTF8, "application/json"))
        {
            using (HttpClient client = new())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);

                HttpResponseMessage respones = await client.PostAsync("https://api.openai.com/v1/chat/completions", content);

                if (respones.StatusCode == HttpStatusCode.Unauthorized)
                    throw new KeyNotFoundException("Ошибка 401 - OpenAI API key failure");

                if (respones.StatusCode == HttpStatusCode.TooManyRequests)
                    throw new OverflowException("Ошибка 429 - API key expired or limit exceeded");

                if (!respones.IsSuccessStatusCode)
                    throw new Exception(respones.StatusCode.ToString());

                string message = await respones.Content.ReadAsStringAsync();

                try
                {
                    return JToken.Parse(message).SelectToken("choices[0].message.content")!.ToString(); 
                }
                catch (Exception) 
                {
                    return null;
                }
            }
        }
    }
}