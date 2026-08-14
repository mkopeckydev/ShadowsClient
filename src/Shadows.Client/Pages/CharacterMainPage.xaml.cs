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
        List<CustomCounter> customCounters = [];
        List<FightNumber> fightNumbers = [];

        await RunBusyAsync(async () =>
        {
            customCounters = await ServerApi.GetCustomCounterListAsync(App.CharacterData);
            fightNumbers = await ServerApi.GetFightNumberListAsync(App.CharacterData);
        });

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