using EntityModels;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.OData;
using Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddNewsAppDbContext();

builder.Services.AddControllers();

builder.Services.AddControllers()
    .AddOData(options => options
    .AddRouteComponents(routePrefix: "api",
        GetEdmModelForArticles())
        .Select()
        .Expand()
        .Filter()
        .OrderBy()
        .SetMaxTop(25)
        .Count()
    );

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IArticleService, ArticleService>();
builder.Services.AddScoped<IAutorizeService, AutorizeService>();

//Configuring HTTP logging
builder.Services.AddHttpLogging(options =>
{
    options.RequestHeaders.Add("Origin");

    options.LoggingFields = HttpLoggingFields.All;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
