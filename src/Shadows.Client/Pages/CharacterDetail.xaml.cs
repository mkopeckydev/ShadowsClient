using CommunityToolkit.Maui.Extensions;
using Shadows.Client.Controls;
using Shadows.Client.Popups;
using Shadows.Data.Facade;
using Shadows.Data.Model;
using Shadows.Data.Tools;

namespace Shadows.Client.Pages;

public partial class CharacterDetail : BaseContentPage
{
    private CharacterData _data = new();

    public CharacterDetail()
    {
        InitializeComponent();
        InitActivityIndicator(pageHeader);
    }

    public override async Task ReloadDataAsync()
    {
        string caption = String.Empty;

        if (App.ExistCharacter())
        {
            _data = App.CharacterData;
            caption = "Upravit postavu";
            pageHeader.DeleteButton = true;
        }
        else
        {
            _data = new CharacterData();
            caption = "Přidat postavu";
        }

        pageHeader.Caption = caption;
        BindingContext = _data;
    }

    private async void pageHeader_DoneClicked(object sender, EventArgs e)
    {
        CharacterDataFacade fCharacterData = new CharacterDataFacade();

        if (!_data.CheckData())
        {
            await ShowMessageBoxWarning("Uložení postavy", "Musí být vyplněny všechny hodnoty.");
            return;
        }

        fCharacterData.Save(_data);

        App.SetCharacter(_data);

        App.ClearCharacter();

        await AppShell.ShellRoutelBackAsync();
    }

    private async void pageHeader_DeleteClicked(object sender, EventArgs e)
    {
        bool c = await ShowMessageBoxConfirm("Smazání postavy", "Opravdu smazat tuto postavu?");

        if (c)
        {
            CharacterDataFacade fCharacterData = new CharacterDataFacade();

            fCharacterData.Delete(_data);

            App.ClearCharacter();

            await AppShell.ShellRoutelBackAsync();
        }
    }
}