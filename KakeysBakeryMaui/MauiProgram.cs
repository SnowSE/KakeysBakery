using Microsoft.Extensions.Logging;
using KakeysBakeryClassLib.Services.Interfaces;

namespace KakeysBakeryMaui
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

            builder.Services.AddScoped<ILogger>();
            builder.Services.AddScoped<HttpClient>();


            builder.Services.AddScoped<IAddonService>();
            builder.Services.AddScoped<IBaseGoodService>();
            builder.Services.AddScoped<IProductService>();

#if DEBUG
    		builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
