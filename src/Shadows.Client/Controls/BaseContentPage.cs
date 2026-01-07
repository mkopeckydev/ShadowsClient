using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Extensions;
using CommunityToolkit.Maui.Views;
using Shadows.Client.Popups;

namespace Shadows.Client.Controls;

public class BaseContentPage : ContentPage
{
    private ActivityIndicator? _indicator;

    public int ParamId { get; set; }

    public BaseContentPage()
    { }

    public void InitActivityIndicator(ActivityIndicator indicator)
    {
        _indicator = indicator;
    }

    public void InitActivityIndicator(PageHeader header)
    {
        _indicator = header.Indicator;
    }

    public virtual async Task ReloadDataAsync()
    { }

    public virtual void ClearData()
    { }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        //dont use for data loading / changing ui elemnts
    }

    protected void ShowActivityIndicator()
    {
        _indicator?.IsRunning = true;
    }

    protected void HideActivityIndicator()
    {
        _indicator?.IsRunning = false;
    }

    protected override bool OnBackButtonPressed()
    {
        AppShell.ShellRoutelBack();
        return true;
    }

    public async Task<bool> ShowMessageBoxConfirm(string caption, string text)
    {
        IPopupResult<bool> c = await this.ShowPopupAsync<bool>(new MessageBoxConfirm(caption, text), new PopupOptions { Shape = null });

        if (c.WasDismissedByTappingOutsideOfPopup)
        {
            return false;
        }
        else
        {
            return c.Result;
        }
    }

    public async Task ShowMessageBoxWarning(string caption, string text)
    {
        await this.ShowPopupAsync(new MessageBoxWarning(caption, text), new PopupOptions { Shape = null });
    }

    public async Task<bool> ShowPopupBoolAsync(Popup<bool> popup)
    {
        IPopupResult<bool> c = await this.ShowPopupAsync<bool>(popup, new PopupOptions { Shape = null });

        if (c.WasDismissedByTappingOutsideOfPopup)
        {
            return false;
        }
        else
        {
            return c.Result;
        }
    }
}