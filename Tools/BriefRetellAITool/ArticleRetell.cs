using System.Text;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using BriefRetellAITool.Models;

namespace BriefRetellAITool;

public class ArticleRetell
{
    private string _apiKey = null!;

    public ArticleRetell(string apiKey) => _apiKey = apiKey;

    public async Task<string> ConvertArticle(string articleText)
    {
        if(string.IsNullOrEmpty(articleText))
        {
            throw new ArgumentNullException(nameof(articleText));
        }

        try
        {
            AIModel aIModel = new()
            {
                Messages = new Messages[] {
                new() { Role = "user", Content = "Преобразуй текст статьи так, чтобы новый текст по пунктам излагал краткую суть статьи: "+articleText
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
                    Console.WriteLine(respones.StatusCode);

                    string message = await respones.Content.ReadAsStringAsync();
                    string responsePrompt = JToken.Parse(message).SelectToken("choices[0].message.content")!.ToString();
                    Console.WriteLine(responsePrompt);
                }
            }
        }
        catch (Exception ex) 
        {
            //throw new Exception()
        }

        return await Task.FromResult(string.Empty);
    }
}