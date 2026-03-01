using Shadows.Api;
using Shadows.Api.WebApi;
using Shadows.Client.Controls;
using Shadows.Data.Facade;

namespace Shadows.Client.Pages;

public partial class CustomCounterDetail : BaseContentPage
{
    private CustomCounter _data { get; set; } = new();

    public CustomCounterDetail()
    {
        InitializeComponent();
        InitActivityIndicator(pageHeader);

        sColor.Init("Barva", ColorFacade.GetColors(), this);
    }

    protected override async Task SetQueryData(object data)
    {
        if (data is CustomCounter)
        {
            _data = (CustomCounter)data;

            BindingContext = _data;
            pageHeader.DeleteButton = (_data.Id != 0);
            sColor.SelectedItem = _data.ColorObject;
        }
    }

    private async void pageHeader_DoneClicked(object sender, EventArgs e)
    {
        if (_data.MaxValue == 0)
        {
            await ShowMessageBoxWarning("Uložení počítadla", "Maximální hodnota nesmí být nulová.");
            return;
        }

        if (String.IsNullOrEmpty(_data.Caption))
        {
            await ShowMessageBoxWarning("Uložení počítadla", "Název musí být vyplněn.");
            return;
        }

        ShowActivityIndicator();

        await ServerApi.SaveCustomCounterAsync(_data, App.CharacterData);

        HideActivityIndicator();

        await AppShell.ShellRoutelBackAsync();
    }

    private async void pageHeader_DeleteClicked(object sender, EventArgs e)
    {
        bool c = await ShowMessageBoxConfirm("Smazání počítadla", "Opravdu smazat počítadlo?");

        if (c)
        {
            ShowActivityIndicator();

            await ServerApi.DeleteCustomCounterAsync(_data, App.CharacterData);

            HideActivityIndicator();

            await AppShell.ShellRoutelBackAsync();
        }
    }

    private void sColor_SelectedChanged(object sender, EventArgs e)
    {
        if (sColor.SelectedItem != null)
        {
            _data.ColorObject = sColor.SelectedItem;
        }
    }
}