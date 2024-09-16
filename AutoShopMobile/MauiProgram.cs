using AutoShopAppLibrary.Services;
using AutoShopAppLibrary.Shared;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AutoShopMobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });


            builder.Services.AddMauiBlazorWebView();
            //builder.Services.AddSingleton<IConfiguration>();
            builder.Services.AddScoped<IEmailService, EmailService>();
            builder.Services.AddScoped<IDBService, MauiDBService>();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}