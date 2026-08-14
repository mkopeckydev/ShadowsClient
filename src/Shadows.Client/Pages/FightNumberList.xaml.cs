using Shadows.Api;
using Shadows.Api.WebApi;
using Shadows.Client.Controls;

namespace Shadows.Client.Pages;

public partial class FightNumberList : BaseContentPage
{
    public FightNumberList()
    {
        InitializeComponent();
        InitActivityIndicator(pageHeader);
    }

    public async override Task ReloadDataAsync()
    {
        lwFightNumbers.ItemsSource = await RunBusyAsync(() => ServerApi.GetFightNumberListAsync(App.CharacterData));
    }

    private async void PageHeader_PlusClicked(object sender, EventArgs e)
    {
        await AppShell.ShellRouteAsync(AppShell.RouteFightNumberDetail, new FightNumber());
    }

    private async void FightNumber_Tapped(object sender, TappedEventArgs e)
    {
        if (e.Parameter != null)
        {
            FightNumber data = (FightNumber)e.Parameter;
            await AppShell.ShellRouteAsync(AppShell.RouteFightNumberDetail, data);
        }
    }
}