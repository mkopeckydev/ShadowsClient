using Shadows.Api;
using Shadows.Api.WebApi;
using Shadows.Client.Controls;

namespace Shadows.Client.Pages;

[QueryProperty(nameof(Data), "Data")]
public partial class FightNumberDetail : BaseContentPage
{
    public FightNumber Data { get; set; }

    public FightNumberDetail()
    {
        InitializeComponent();
        InitActivityIndicator(pageHeader);

        Data = new FightNumber();
        Data.BaseValueCaption1 = "INI";
        Data.BaseValueCaption2 = "ÚČ";
        Data.BaseValueCaption3 = "ZZ";
        Data.BaseValueCaption4 = "OZ";
        Data.BaseValueCaption5 = "OU";
    }

    public override async Task ReloadDataAsync()
    {
        pageHeader.DeleteButton = (Data.Id == 0);
        BindingContext = Data;

        if (Data.Bonuses != null)
        {
            foreach (FightNumberBonus b in Data.Bonuses)
            {
                FightNumberBonusAdminControl c = new FightNumberBonusAdminControl(b);
                slBonus.Children.Add(c);
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
        Data.Bonuses = bonusList;

        if (String.IsNullOrEmpty(Data.Caption))
        {
            await ShowMessageBoxWarning("Uložení bojového čísla", "Název musí být vyplněn.");
            return;
        }

        ShowActivityIndicator();

        await ServerApi.SaveFightNumberAsync(Data, App.CharacterData);

        HideActivityIndicator();

        await AppShell.ShellRoutelBackAsync();
    }

    private async void pageHeader_DeleteClicked(object sender, EventArgs e)
    {
        bool c = await ShowMessageBoxConfirm("Smazání bojového čísla", "Opravdu smazat bojové číslo?");

        if (c)
        {
            ShowActivityIndicator();

            await ServerApi.DeleteFightNumberAsync(Data, App.CharacterData);

            HideActivityIndicator();

            await AppShell.ShellRoutelBackAsync();
        }
    }

    private void btnAddBonus_Clicked(object sender, EventArgs e)
    {
        FightNumberBonusAdminControl c = new FightNumberBonusAdminControl(new FightNumberBonus());
        slBonus.Children.Add(c);
    }
}