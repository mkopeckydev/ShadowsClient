using AndroidX.Core.View;
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
    {
        //Padding = new Thickness(0,50,0,0);
    }

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

    #region Popup

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
    #endregion
    /*
    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();

#if ANDROID
        if (Handler?.PlatformView is Android.Views.View view)
        {
            ViewCompat.SetOnApplyWindowInsetsListener(view, (v, insets) =>
            {
                var systemBars = insets.GetInsets(
                    WindowInsetsCompat.Type.SystemBars());

                Padding = new Thickness(
                    systemBars.Left,
                    systemBars.Top,
                    systemBars.Right,
                    systemBars.Bottom);

                return insets; // POVINNÉ
            });
        }
#endif
    }
    */
    
}

