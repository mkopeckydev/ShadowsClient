using Shadows.Api;
using Shadows.Api.WebApi;
using Shadows.Client.Controls;

namespace Shadows.Client.Pages;

public partial class FightNumberDetail : BaseContentPage
{
    public FightNumber _data { get; set; } = new();

    public FightNumberDetail()
    {
        InitializeComponent();
        InitActivityIndicator(pageHeader);
    }

    protected override async Task SetQueryData(object data)
    {
        if (data is FightNumber)
        {
            _data = (FightNumber)data;

            _data.BaseValueCaption1 = "INI";
            _data.BaseValueCaption2 = "ÚČ";
            _data.BaseValueCaption3 = "ZZ";
            _data.BaseValueCaption4 = "OZ";
            _data.BaseValueCaption5 = "OU";

            BindingContext = _data;

            pageHeader.DeleteButton = (_data.Id != 0);

            if (_data.Bonuses != null)
            {
                foreach (FightNumberBonus b in _data.Bonuses)
                {
                    FightNumberBonusAdminControl c = new FightNumberBonusAdminControl(b);
                    slBonus.Children.Add(c);
                }
            }
        }
    }

    private async void pageHeader_DoneClicked(object sender, EventArgs e)
    {
        List<FightNumberBonus> bonusList = new List<FightNumberBonus>();
        foreach (FightNumberBonusAdminControl b in slBonus.Children)
        {
            bonusList.Add(b.Data);
        }
        _data.Bonuses = bonusList;

        if (String.IsNullOrEmpty(_data.Caption))
        {
            await ShowMessageBoxWarning("Uložení bojového čísla", "Název musí být vyplněn.");
            return;
        }

        await RunBusyAsync(() => ServerApi.SaveFightNumberAsync(_data, App.CharacterData));

        await AppShell.ShellRoutelBackAsync();
    }

    private async void pageHeader_DeleteClicked(object sender, EventArgs e)
    {
        bool c = await ShowMessageBoxConfirm("Smazání bojového čísla", "Opravdu smazat bojové číslo?");

        if (c)
        {
            await RunBusyAsync(() => ServerApi.DeleteFightNumberAsync(_data, App.CharacterData));

            await AppShell.ShellRoutelBackAsync();
        }
    }

    private void btnAddBonus_Clicked(object sender, EventArgs e)
    {
        FightNumberBonusAdminControl c = new FightNumberBonusAdminControl(new FightNumberBonus());
        slBonus.Children.Add(c);
    }
}