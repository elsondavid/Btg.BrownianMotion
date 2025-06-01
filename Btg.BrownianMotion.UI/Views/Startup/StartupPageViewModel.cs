using Btg.BrownianMotion.UI.Views.Charts;
using Naveasy;

namespace Btg.BrownianMotion.UI.Views.Startup;

public class StartupPageViewModel(INavigationService navigationService) : IPageLifecycleAware
{
    public void OnAppearing()
    {
        navigationService.NavigateAbsoluteAsync<LinearChartPageViewModel>();
    }

    public void OnDisappearing()
    { }
}