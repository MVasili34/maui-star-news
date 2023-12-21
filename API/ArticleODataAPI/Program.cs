using EntityModels;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.OData;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Services;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("DbConnection")!;

// Add services to the container.
builder.Services.AddNewsAppDbContext(connectionString);

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
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddScoped<IArticleService, ArticleService>();
builder.Services.AddScoped<IAuthUsersService, AuthUsersService>();

builder.Services.AddAuthentication().AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateAudience = false,
        ValidateIssuer = false,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                builder.Configuration.GetSection("AppSettings:Token").Value!))
    };
});

//Configuring HTTP logging
builder.Services.AddHttpLogging(options =>
{
    options.RequestHeaders.Add("Origin");

    options.LoggingFields = HttpLoggingFields.All;
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpLogging();

app.UseAuthorization();

app.MapControllers();

app.Run();
