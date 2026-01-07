using Shadows.Api.WebApi;
using Shadows.Api;
using Shadows.Data.Facade;
using Shadows.Client.Popups;

namespace Shadows.Client.Controls;

public partial class CustomCounterControl : ContentView
{
    CustomCounter _data;
    BaseContentPage _page;

    public CustomCounterControl(CustomCounter data, BaseContentPage page)
    {
        InitializeComponent();
        _data = data;
        _page = page;
        ReloadData();
    }

    public void ReloadData()
    {
        lblCaption.Text = _data.Caption;
        lblValue.Text = String.Format("{0} z {1}", _data.ActualValue, _data.MaxValue);
        pbValue.Progress = Convert.ToSingle(_data.ActualValue) / Convert.ToSingle(_data.MaxValue);

        if ((_data.ActualValue < 0) || (_data.ActualValue > _data.MaxValue))
        {
            lblValue.Style = App.GetStyle(App.LabelRed);
        }
        else
        {
            lblValue.Style = null;
        }

        pbValue.ProgressColor = ColorFacade.GetSystemColor(_data.Color);
    }

    private async void btnPlus_Clicked(object sender, EventArgs e)
    {
        CustomCounterControlAdd i = new CustomCounterControlAdd(true, _data);
        bool c = await _page.ShowPopupBoolAsync(i);
        ReloadData();
    }

    private async void btnMinus_Clicked(object sender, EventArgs e)
    {
        CustomCounterControlAdd i = new CustomCounterControlAdd(false, _data);
        bool c = await _page.ShowPopupBoolAsync(i);
        ReloadData();
    }

    
}