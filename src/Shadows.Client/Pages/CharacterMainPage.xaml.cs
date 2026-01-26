using Shadows.Api;
using Shadows.Api.WebApi;
using Shadows.Client.Controls;
using Shadows.Data.Model;

namespace Shadows.Client.Pages;

public partial class CharacterMainPage : BaseContentPage
{
    public CharacterMainPage()
    {
        InitializeComponent();
        InitActivityIndicator(pageHeader);

        CharacterData d = App.CharacterData;
        pageHeader.Caption = d.DisplayName;
    }

    public override async Task ReloadDataAsync()
    {
        ShowActivityIndicator();

        var customCounters = await ServerApi.GetCustomCounterListAsync(App.CharacterData);

        var fightNumbers = await ServerApi.GetFightNumberListAsync(App.CharacterData);

        HideActivityIndicator();

        slCounters.Children.Clear();

        foreach (CustomCounter cc in customCounters)
        {
            CustomCounterControl control = new CustomCounterControl(cc, this);
            slCounters.Children.Add(control);
        }

        slFightNumbers.Children.Clear();

        foreach (FightNumber fn in fightNumbers)
        {
            FightNumberBonusControl control = new FightNumberBonusControl(fn);
            slFightNumbers.Children.Add(control);
        }
    }
}