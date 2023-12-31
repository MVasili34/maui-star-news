﻿using SkiaSharp.Views.Maui.Controls.Hosting;
using Microsoft.Extensions.Logging;
using NewsMobileApp.TempServices;
using The49.Maui.BottomSheet;
using NewsMobileApp.ViewsNative;
using ImagesCloudTool;
using NewsMobileApp.ViewModels;
using System.Net.Http.Headers;

namespace NewsMobileApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseSkiaSharp(true)
                .UseBottomSheet()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("MaterialIconsOutlined-Regular.otf", "MaterialIcons");
                    fonts.AddFont("Poppins-Bold.ttf", "PopsBold");
                    fonts.AddFont("Poppins-Regular.ttf", "PopsRegular");
                    fonts.AddFont("Poppins-SemiBold.ttf", "PopsSBold");
                    fonts.AddFont("NotoSerif-Bold.ttf", "NotoSerif");
                });
            builder.Services.AddHttpClient("NewsAPI", configureClient: options =>
            {
                options.BaseAddress = new(DeviceInfo.Platform == DevicePlatform.Android ? "http://192.168.0.106:8080/" :
                    "http://localhost:8080/");
                options.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json", 1.0));
            });
            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddScoped<IRequestsService, RequestsService>();
            builder.Services.AddScoped<IImageCloudTool, ImageCloudTool>(_ =>
                new ImageCloudTool("fc0f801a94e39de"));
            builder.Services.AddScoped<SectionsPage>();
            builder.Services.AddScoped<TrendsPage>();
            builder.Services.AddScoped<ThrendsViewModel>();
            builder.Services.AddScoped<SectionViewModel>();
            builder.Services.AddScoped<ArticlesBySectionPage>();
            builder.Services.AddScoped<ArticlesBySectionViewModel>();
            builder.Services.AddScoped<SupportPage>();
            builder.Services.AddScoped<MainEventsPage>();
            builder.Services.AddScoped<MainPageViewModel>();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}