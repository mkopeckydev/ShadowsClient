using CommunityToolkit.Maui;
using Plugin.LocalNotification;
using Plugin.MauiMtAdmob;

namespace Shadows.Client;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .UseMauiMTAdmob()
            .UseLocalNotification()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("calibri-bold.ttf", "CalibriBold");
                fonts.AddFont("calibri-bold-italic.ttf", "CalibriBoldItalic");
                fonts.AddFont("calibri-italic.ttf", "CalibriItalic");
                fonts.AddFont("calibri-regular.ttf", "Calibri");
            });

        return builder.Build();
    }
}
