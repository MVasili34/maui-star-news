using Newtonsoft.Json;

namespace NewsMobileApp.Models.ODataModels;

public class ODataModel<T>
{
    [JsonProperty("odata.metadata")]
    public string Metadata { get; set; }
    public IEnumerable<T> Value { get; set; }
}
