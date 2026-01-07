using Shadows.Api;
using Shadows.Api.WebApi;
using Shadows.Client.Controls;

namespace Shadows.Client.Pages;

public partial class ActivityList : BaseContentPage
{
    public ActivityList()
    {
        InitializeComponent();
        InitActivityIndicator(pageHeader);
    }

    public override async Task ReloadDataAsync()
    {
        ShowActivityIndicator();
        
        var list = await ServerApi.GetCharacterActivityListAsync(App.CharacterData);

        HideActivityIndicator();

        lvActivities.ItemsSource = list;
    }

    public override void ClearData()
    {
        lvActivities.ItemsSource = null;
    }

    private async void Activity_Tapped(object sender, TappedEventArgs e)
    {
        if (e.Parameter != null)
        {
            CharacterActivity data = (CharacterActivity) e.Parameter;
            await AppShell.ShellRouteAsync(AppShell.RouteActivityDetail, data);
        }
    }

    private async void PageHeader_PlusClicked(object sender, EventArgs e)
    {
        await AppShell.ShellRouteAsync(AppShell.RouteActivityDetail);
    }
}