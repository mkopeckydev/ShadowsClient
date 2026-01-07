using Shadows.Api;
using Shadows.Api.WebApi;
using Shadows.Client.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        var list = await ServerApi.GetFightNumberListAsync(App.CharacterData);

        lwFightNumbers.ItemsSource = list;
    }

    public override void ClearData()
    {
        lwFightNumbers.ItemsSource = null;
    }

    private async void PageHeader_PlusClicked(object sender, EventArgs e)
    {
        await AppShell.ShellRouteAsync(AppShell.RouteFightNumberDetail);
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