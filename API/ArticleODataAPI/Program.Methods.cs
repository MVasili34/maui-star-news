using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using EntityModels;

partial class Program
{
    static IEdmModel GetEdmModelForArticles()
    {
        ODataConventionModelBuilder modelBuilder = new();
        modelBuilder.EntitySet<Article>("Articles");
        modelBuilder.EntitySet<Section>("Sections");
        return modelBuilder.GetEdmModel();
    }
}
