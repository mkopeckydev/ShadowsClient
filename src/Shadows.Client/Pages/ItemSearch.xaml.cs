using Shadows.Api;
using Shadows.Api.WebApi;
using Shadows.Data;
using Shadows.Client.Controls;
using Shadows.Data.Tools;
using Shadows.Client.Popups;
using CommunityToolkit.Maui.Extensions;

namespace Shadows.Client.Pages;

public partial class ItemSearch : BaseContentPage
{
    private bool _loading;
    public ItemSearch()
    {
        InitializeComponent();
        InitActivityIndicator(pageHeader);

        _loading = true;
        sSkill.Init("Dovednost", App.SessionData.Skils, this);
        _loading = false;
    }

    public override async Task ReloadDataAsync()
    {
        if (_loading) return;

        string searchText = txtSearch.Text;
        int idSkill = 0;
        if (sSkill.SelectedItem != null)
        {
            idSkill = sSkill.SelectedItem.Id;
        }

        ShowActivityIndicator();

        var list = await ServerApi.ItemSearchSearchAsync(searchText, idSkill, App.CharacterData);

        HideActivityIndicator();

        DirectoryInfo imgDir = AppFiles.GetItemImagesDir();

        foreach (Item i in list)
        {
            i.Init(imgDir.FullName);
        }

        cvSearch.ItemsSource = list;
    }

    private async void pageHeader_SearchClicked(object sender, EventArgs e)
    {
        await ReloadDataAsync();
    }

    private async void Item_Tapped(object sender, TappedEventArgs e)
    {
        if (e.Parameter != null)
        {
            await AppShell.ShellRouteAsync(AppShell.RouteItemDetail, ((Item)e.Parameter).Id);
        }
    }

    private async void pageHeader_DownloadClicked(object sender, EventArgs e)
    {
        bool c = await ShowMessageBoxConfirm("Stažení obrázků", "Stáhnout obrázky všech položek do zařízení?");

        if (!c) return;

        DirectoryInfo imgDir = AppFiles.GetItemImagesDir();

        ShowActivityIndicator();

        var list = await ServerApi.ItemSearchSearchAsync(String.Empty, 0, App.CharacterData);

        int index = 0;
        
        foreach (Item i in list)
        {
            var data = await ServerApi.ItemDetailAsync(i.Id, App.CharacterData);

            if (data.ImageData.Length > 0)
            {
                string fileName = Path.Combine(imgDir.FullName, i.ImageName);
                await File.WriteAllBytesAsync(fileName, data.ImageData);
            }
            index++;

            pageHeader.ShowPercent(Convert.ToInt32((Convert.ToSingle(index) / Convert.ToSingle(list.Count)) * 100));
            ShowActivityIndicator();
        }

        pageHeader.HidePercent();

        HideActivityIndicator();

        await ReloadDataAsync();
    }
}