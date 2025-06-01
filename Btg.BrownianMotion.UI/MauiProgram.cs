using Btg.BrownianMotion.Core.Services;
using Btg.BrownianMotion.UI.Views;
using Btg.BrownianMotion.UI.Views.Charts;
using Btg.BrownianMotion.UI.Views.Startup;
using Microsoft.Extensions.Logging;
using Naveasy;
using Naveasy.Core;

namespace Btg.BrownianMotion.UI;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseNaveasy<StartupPageViewModel>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.Services
            .AddSingleton<IBrownianMotionService, BrownianMotionService>()
            .AddTransientForNavigation<LinearChartPage, LinearChartPageViewModel>()
            .AddTransientForNavigation<StartupPage, StartupPageViewModel>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}