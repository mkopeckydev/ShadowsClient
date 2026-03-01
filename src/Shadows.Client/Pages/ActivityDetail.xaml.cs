using CommunityToolkit.Maui.Extensions;
using Shadows.Api;
using Shadows.Api.WebApi;
using Shadows.Client.Controls;
using Shadows.Client.Popups;
using Shadows.Data.Facade;
using Shadows.Data.Tools;

namespace Shadows.Client.Pages;

public partial class ActivityDetail : BaseContentPage
{
    private CharacterActivity _data = new CharacterActivity() { CheckCount = 1 };

    public ActivityDetail()
    {
        InitializeComponent();
        InitActivityIndicator(pageHeader);

        sColor.Init("Barva", ColorFacade.GetColors(), this);
        sCount.Init("Počet", CommonTools.GetListInt(5), this);
    }

    protected override async Task SetQueryData(object data)
    {
        if (data is CharacterActivity)
        {
            _data = (CharacterActivity)data;

            BindingContext = _data;

            pageHeader.DeleteButton = (_data.Id != 0);
            sColor.SelectedItem = _data.ColorObject;
            sCount.SelectedId = _data.CheckCount;
        }
    }

    private async void pageHeader_DoneClicked(object sender, EventArgs e)
    {
        if (_data.CheckCount == 0)
        {
            await ShowMessageBoxWarning("Uložení činnosti", "Počet musí být v rozmezí 1 - 5.");
            return;
        }

        if (String.IsNullOrEmpty(_data.Caption))
        {
            await ShowMessageBoxWarning("Uložení činnosti", "Název musí být vyplněn.");
            return;
        }

        ShowActivityIndicator();

        await ServerApi.SaveCharacterActivityAsync(_data, App.CharacterData);

        HideActivityIndicator();

        await AppShell.ShellRoutelBackAsync();
    }

    private async void pageHeader_DeleteClicked(object sender, EventArgs e)
    {
        bool c = await ShowMessageBoxConfirm("Smazání činnosti", "Opravdu smazat činnost?");

        if (c)
        {
            ShowActivityIndicator();

            await ServerApi.DeleteCharacterActivityAsync(_data, App.CharacterData);

            HideActivityIndicator();

            await AppShell.ShellRoutelBackAsync();
        }
    }

    private void sCount_SelectedChanged(object sender, EventArgs e)
    {
        if (sCount.SelectedItem != null)
        {
            _data.CheckCount = sCount.SelectedItem.Id;
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