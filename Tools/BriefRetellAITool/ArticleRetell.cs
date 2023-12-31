﻿using System.Text;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using BriefRetellAITool.Models;
using System.Net;

namespace BriefRetellAITool;

public class ArticleRetell
{
    private readonly string _apiKey = null!;

    public ArticleRetell(string apiKey) => _apiKey = apiKey;

    /// <summary>
    /// Method for getting brief retell of the text
    /// </summary>
    /// <param name="articleText">Non empty text of <see cref="string"/> type</param>
    /// <returns>Short text version, else <see href="null"/></returns>
    /// <exception cref="ArgumentNullException">If article text is empty</exception>
    /// <exception cref="KeyNotFoundException">Authorization problems with OpenAI</exception>
    /// <exception cref="OverflowException">Invalid OpenAI key</exception>
    /// <exception cref="Exception">Other problems</exception>
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