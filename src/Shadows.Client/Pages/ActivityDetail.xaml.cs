using CommunityToolkit.Maui.Extensions;
using Shadows.Api;
using Shadows.Api.WebApi;
using Shadows.Client.Controls;
using Shadows.Client.Popups;
using Shadows.Data.Facade;
using Shadows.Data.Tools;

namespace Shadows.Client.Pages;

[QueryProperty(nameof(Data), "Data")]
public partial class ActivityDetail : BaseContentPage
{
    public CharacterActivity Data { get; set; } = new CharacterActivity() { CheckCount = 1 };

    public ActivityDetail()
    {
        InitializeComponent();
        InitActivityIndicator(pageHeader);

        sColor.Init("Barva", ColorFacade.GetColors(), this);
        sCount.Init("Počet", CommonTools.GetListInt(5), this);
    }

    public override async Task ReloadDataAsync()
    {
        BindingContext = Data;
        pageHeader.DeleteButton = (Data.Id == 0);
        sColor.SelectedItem = Data.ColorObject;
        sCount.SelectedId = Data.CheckCount;
    }

    private async void pageHeader_DoneClicked(object sender, EventArgs e)
    {
        if (Data.CheckCount == 0)
        {
            await ShowMessageBoxWarning("Uložení činnosti", "Počet musí být v rozmezí 1 - 5.");
            return;
        }

        if (String.IsNullOrEmpty(Data.Caption))
        {
            await ShowMessageBoxWarning("Uložení činnosti", "Název musí být vyplněn.");
            return;
        }

        ShowActivityIndicator();

        await ServerApi.SaveCharacterActivityAsync(Data, App.CharacterData);

        HideActivityIndicator();

        await AppShell.ShellRoutelBackAsync();
    }

    private async void pageHeader_DeleteClicked(object sender, EventArgs e)
    {
        bool c = await ShowMessageBoxConfirm("Smazání činnosti", "Opravdu smazat činnost?");

        if (c)
        {
            ShowActivityIndicator();

            await ServerApi.DeleteCharacterActivityAsync(Data, App.CharacterData);

            HideActivityIndicator();

            await AppShell.ShellRoutelBackAsync();
        }
    }

    private void sCount_SelectedChanged(object sender, EventArgs e)
    {
        if (sCount.SelectedItem != null)
        {
            Data.CheckCount = sCount.SelectedItem.Id;
        }
    }

    private void sColor_SelectedChanged(object sender, EventArgs e)
    {
        if (sColor.SelectedItem != null)
        {
            Data.ColorObject = sColor.SelectedItem;
        }
    }
}