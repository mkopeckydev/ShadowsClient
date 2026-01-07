using Shadows.Api.WebApi;

namespace Shadows.Client.Controls;

public class FightNumberBonusButton : Button
{
    public FightNumberBonus Data { get; set; }

    public FightNumberBonusButton(FightNumberBonus data)
    {
        Data = data;
        Text = Data.Caption;
        RefreshStyle();
    }

    public void RefreshStyle()
    {
        if (Data.IsSelected)
        {
            Style = App.GetStyle(App.ThinButtonSelected);
        }
        else
        {
            Style = App.GetStyle(App.ThinButton);
        }
    }

    public void Switch()
    {
        Data.IsSelected = !Data.IsSelected;
        RefreshStyle();
    }
}
