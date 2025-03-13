using Microsoft.Maui;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;
using Maui_Shopping_APP.Views;
using ZXing.Net.Maui.Controls;
using CommunityToolkit.Maui;
using Microsoft.Maui.Handlers;
using Maui_Shopping_APP.ViewModels;
using FFImageLoading.Maui;


namespace Maui_Shopping_APP;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseBarcodeReader()
            .UseMauiCommunityToolkit()
            .UseFFImageLoading()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            })
            .ConfigureMauiHandlers(handlers =>
            {
                handlers.AddHandler<Microsoft.Maui.IImage, IImageHandler>();
            });
        builder.Services.AddSingleton<CartViewModel>();
        builder.Services.AddTransient<MainPage>();

        return builder.Build();
    }
}
