using Newtonsoft.Json;

namespace NewsMobileApp.Models.ODataModels;

public class DiagramData
{
    [JsonProperty("odata.id")]
    public string Id { get; set; }
    public DateTime PublishTime { get; set; }
    public int Total { get; set; }
}
