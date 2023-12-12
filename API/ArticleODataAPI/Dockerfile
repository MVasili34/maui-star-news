FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["API/ArticleODataAPI/ArticleODataAPI.csproj", "API/ArticleODataAPI/"]
COPY ["AppServices/EntityModels/EntityModels.csproj", "AppServices/EntityModels/"]
COPY ["AppServices/Services/Services.csproj", "AppServices/Services/"]
COPY ["Tools/CryptographyTool/CryptographyTool.csproj", "/Tools/CryptographyTool/"]
RUN dotnet restore "API/ArticleODataAPI/ArticleODataAPI.csproj"
COPY . .
WORKDIR "/src/API/ArticleODataAPI"
RUN dotnet build "ArticleODataAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ArticleODataAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ArticleODataAPI.dll"]