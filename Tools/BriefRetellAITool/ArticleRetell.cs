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
        }
        catch (Exception ex) 
        {
            //throw new Exception()
        }

        return await Task.FromResult(string.Empty);
    }
}