using Shadows.Api;
using Shadows.Api.WebApi;
using Shadows.Client.Controls;

namespace Shadows.Client.Pages;

public partial class SpellbookItemSearch : BaseContentPage
{
    public SpellbookItemSearch()
    {
        InitializeComponent();
        InitActivityIndicator(pageHeader);
    }

    public override async Task ReloadDataAsync()
    {
        string searchText = txtSearch.Text;

        ShowActivityIndicator();

        cvSearch.ItemsSource = await ServerApi.SpellbookItemSearchSearchAsync(searchText, App.CharacterData);

        HideActivityIndicator();
    }

    private async void PageHeader_SearchClicked(object sender, EventArgs e)
    {
        await ReloadDataAsync();
    }

    private async void Spellbook_Tapped(object sender, TappedEventArgs e)
    {
        if (e.Parameter != null)
        {
            SpellbookItem data = (SpellbookItem)e.Parameter;
            await AppShell.ShellRouteAsync(AppShell.RouteSpellbookItemDetail, data.Id);
        }
    }
}