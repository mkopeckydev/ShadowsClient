using Shadows.Api;
using Shadows.Api.WebApi;

namespace Shadows.Client.Controls;

public partial class FightNumberBonusControl : ContentView
{
    private FightNumber _data;
    public FightNumberBonusControl(FightNumber data)
    {
        InitializeComponent();
        _data = data;

        lblCaption.Text = _data.Caption;

        ReloadData();
    }

    private void ReloadData()
    {
        CalculateValue(1, lblValue1);
        CalculateValue(2, lblValue2);
        CalculateValue(3, lblValue3);
        CalculateValue(4, lblValue4);
        CalculateValue(5, lblValue5);

        grdBonus.Clear();

        int row = 0;
        int col = 0;

        foreach (FightNumberBonus b in _data.Bonuses)
        {
            FightNumberBonusButton btn = new FightNumberBonusButton(b);
            btn.Clicked += btnBonus_Clicked;
            grdBonus.Add(btn, col, row);

            if (col < 4)
            {
                col++;
            }
            else
            {
                col = 0;
                row++;
            }
        }
    }

    private void CalculateValue(int valueIndex, Label lbl)
    {
        int value = 0;
        int baseValue = 0;
        string caption = String.Empty;
        
        if (valueIndex == 1) baseValue = _data.BaseValue1;
        else if (valueIndex == 2) baseValue = _data.BaseValue2;
        else if (valueIndex == 3) baseValue = _data.BaseValue3;
        else if (valueIndex == 4) baseValue = _data.BaseValue4;
        else if (valueIndex == 5) baseValue = _data.BaseValue5;

        value = baseValue;

        if (valueIndex == 1) caption = _data.BaseValueCaption1;
        else if (valueIndex == 2) caption = _data.BaseValueCaption2;
        else if (valueIndex == 3) caption = _data.BaseValueCaption3;
        else if (valueIndex == 4) caption = _data.BaseValueCaption4;
        else if (valueIndex == 5) caption = _data.BaseValueCaption5;

        foreach (FightNumberBonus b in _data.Bonuses)
        {
            int bonus = 0;

            if (valueIndex == 1) bonus = b.Bonus1;
            else if (valueIndex == 2) bonus = b.Bonus2;
            else if (valueIndex == 3) bonus = b.Bonus3;
            else if (valueIndex == 4) bonus = b.Bonus4;
            else if (valueIndex == 5) bonus = b.Bonus5;

            if (b.IsSelected)
            {
                value = value + bonus;
            }
        }

        lbl.Text = String.Format("{0}: {1}", caption, value);

        if (value < baseValue)
        {
            lbl.Style = App.GetStyle(App.LabelRed);
        }
        else if (value > baseValue)
        {
            lbl.Style = App.GetStyle(App.LabelGreen);
        }
        else
        {
            lbl.Style = null;
        }
    }

    private async void btnBonus_Clicked(object? sender, EventArgs e)
    {
        if (sender == null) return;

        FightNumberBonusButton b = (FightNumberBonusButton)sender;

        b.Switch();

        await ServerApi.SaveFightNumberBonusSelectedAsync(b.Data, App.CharacterData);

        ReloadData();
    }
}