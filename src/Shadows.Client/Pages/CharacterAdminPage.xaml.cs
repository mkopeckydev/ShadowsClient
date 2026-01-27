using CommunityToolkit.Maui.Extensions;
using Shadows.Client.Controls;
using Shadows.Client.Popups;
using Shadows.Data.Facade;
using Shadows.Data.Model;

namespace Shadows.Client.Pages;

public partial class CharacterAdminPage : BaseContentPage
{
    public CharacterAdminPage()
    {
        InitializeComponent();
        InitActivityIndicator(pageHeader);

        CharacterData d = App.CharacterData;
        lblCharacterName.Text = d.DisplayName;
    }

    private async void btnCharacterList_Clicked(object sender, EventArgs e)
    {
        App.ClearCharacter();

        await AppShell.ShellRouteAsync(AppShell.RouteCharacterList);
    }

    private async void btnEditCharacter_Clicked(object sender, EventArgs e)
    {
        await AppShell.ShellRouteAsync(AppShell.RouteCharacterDetail);
    }

    private async void btnCounters_Clicked(object sender, EventArgs e)
    {
        await AppShell.ShellRouteAsync(AppShell.RouteCustomCounterList);
    }

    private async void btnFightNumbers_Clicked(object sender, EventArgs e)
    {
        await AppShell.ShellRouteAsync(AppShell.RouteFightNumberList);
    }

    private async void btnCharacterActivity_Clicked(object sender, EventArgs e)
    {
        await AppShell.ShellRouteAsync(AppShell.RouteActivityList);
    }

    private async void btnShowLog_Clicked(object sender, EventArgs e)
    {
        string log = LogFacade.Get();

        await ShowMessageBoxWarning("Poslední log", log);
    }

    private async void btnAd_Clicked(object sender, EventArgs e)
    {
        try
        {
            btnAd.IsEnabled = false;

            if (MainActivity.Activity != null)
            {
                MainActivity.Activity.ShowInterstitial();
            }

            await Task.Delay(500);
        }
        finally
        {
            btnAd.IsEnabled = true;
        }
    }
}