using Shadows.Api;
using Shadows.Api.WebApi;
using Shadows.Client.Controls;

namespace Shadows.Client.Pages;

public partial class CustomCounterList : BaseContentPage
{
    public CustomCounterList()
    {
        InitializeComponent();
        InitActivityIndicator(pageHeader);
    }

    public override async Task ReloadDataAsync()
    {
        cvCounters.ItemsSource = await RunBusyAsync(() => ServerApi.GetCustomCounterListAsync(App.CharacterData));
    }

    private async void PageHeader_PlusClicked(object sender, EventArgs e)
    {
        await AppShell.ShellRouteAsync(AppShell.RouteCustomCounterDetail, new CustomCounter());
    }

    private async void Counter_Tapped(object sender, TappedEventArgs e)
    {
        if (e.Parameter != null)
        {
            CustomCounter data = (CustomCounter)e.Parameter;
            await AppShell.ShellRouteAsync(AppShell.RouteCustomCounterDetail, data);
        }
    }
}