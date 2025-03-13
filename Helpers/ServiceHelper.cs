using Microsoft.Extensions.DependencyInjection;
#if WINDOWS
using Microsoft.UI.Xaml;
#elif ANDROID
using Android.App;
#elif IOS || MACCATALYST
using Foundation;
#endif

namespace Maui_Shopping_APP.Helpers
{
    public static class ServiceHelper
    {
        public static TService GetService<TService>() where TService : class
            => Current?.GetService<TService>() ?? throw new InvalidOperationException("Service Provider not initialized.");

        public static IServiceProvider Current =>
#if WINDOWS
            MauiWinUIApplication.Current.Services;
#elif ANDROID
            MauiApplication.Current.Services;
#elif IOS || MACCATALYST
            MauiUIApplicationDelegate.Current.Services;
#else
            null;
#endif
    }
} 