using Shadows.Api;
using Shadows.Api.WebApi;
using Shadows.Client.Controls;
using Shadows.Data.Facade;

namespace Shadows.Client.Pages;

[QueryProperty(nameof(Data), "Data")]
public partial class CustomCounterDetail : BaseContentPage
{
    public CustomCounter Data { get; set; } = new();

    public CustomCounterDetail()
    {
        InitializeComponent();
        InitActivityIndicator(pageHeader);

        sColor.Init("Barva", ColorFacade.GetColors(), this);
    }

    public override async Task ReloadDataAsync()
    {
        BindingContext = Data;
        pageHeader.DeleteButton = (Data.Id == 0);
        sColor.SelectedItem = Data.ColorObject;
    }

    private async void pageHeader_DoneClicked(object sender, EventArgs e)
    {
        if (Data.MaxValue == 0)
        {
            await ShowMessageBoxWarning("Uložení počítadla", "Maximální hodnota nesmí být nulová.");
            return;
        }

        if (String.IsNullOrEmpty(Data.Caption))
        {
            await ShowMessageBoxWarning("Uložení počítadla", "Název musí být vyplněn.");
            return;
        }

        ShowActivityIndicator();

        await ServerApi.SaveCustomCounterAsync(Data, App.CharacterData);

        HideActivityIndicator();

        await AppShell.ShellRoutelBackAsync();
    }

    private async void pageHeader_DeleteClicked(object sender, EventArgs e)
    {
        bool c = await ShowMessageBoxConfirm("Smazání počítadla", "Opravdu smazat počítadlo?");

        if (c)
        {
            ShowActivityIndicator();

            await ServerApi.DeleteCustomCounterAsync(Data, App.CharacterData);

            HideActivityIndicator();

            await AppShell.ShellRoutelBackAsync();
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