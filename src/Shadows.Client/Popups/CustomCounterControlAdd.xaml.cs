using CommunityToolkit.Maui.Views;
using Shadows.Api;
using Shadows.Api.WebApi;
using Shadows.Data.Tools;

namespace Shadows.Client.Popups;

public partial class CustomCounterControlAdd : Popup<bool>
{
    private CustomCounter _data;
	private bool _plus;

    public CustomCounterControlAdd(bool plus, CustomCounter data)
	{
		InitializeComponent();
		
		_data = data;
        _plus = plus;

        lblCaption.Text = _data.Caption + " - " + (_plus ? "Pøièíst" : "Odeèíst");

		if (plus)
		{
            bPopup.Style = App.GetStyle(App.BorderPopup);
        }
        else
        {
            bPopup.Style = App.GetStyle(App.BorderPopupRed);
        }
    }

	private async void btnOk_Clicked(object sender, EventArgs e)
	{
		int coef = _plus ? 1 : -1;

        await AddValue(GetValue(1) * coef);

        await this.CloseAsync(true);
	}

    private async void btnClose_Clicked(object sender, EventArgs e)
    {
		await this.CloseAsync(false);
    }

	public int Value
	{
		get
		{
			return CommonTools.StringToInt(txtValue.Text);
		}
	}

	private int GetValue(int defaultValue)
	{
		if (String.IsNullOrEmpty(txtValue.Text))
		{
			return defaultValue;
		}
		else
		{
			return Value;
		}
	}

    private async Task AddValue(int v)
    {
        _data.ActualValue = _data.ActualValue + v;

        if (_data.ActualValue < -9999) _data.ActualValue = -9999;
        else if (_data.ActualValue > 9999) _data.ActualValue = 9999;

        await ServerApi.SaveCustomCounterAsync(_data, App.CharacterData);
    }
}