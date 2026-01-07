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
        ShowActivityIndicator();

        var list = await ServerApi.GetCustomCounterListAsync(App.CharacterData);

        HideActivityIndicator();

        cvCounters.ItemsSource = list;
    }

    public override void ClearData()
    {
        cvCounters.ItemsSource = null;
    }

    private async void PageHeader_PlusClicked(object sender, EventArgs e)
    {
        await AppShell.ShellRouteAsync(AppShell.RouteCustomCounterDetail);
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