using AndroidX.Core.View;
using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Extensions;
using CommunityToolkit.Maui.Views;
using Shadows.Client.Popups;
using System.Threading;

namespace Shadows.Client.Controls;

public class BaseContentPage : ContentPage, IQueryAttributable
{
    public int ParamId { get; set; }

    public BaseContentPage()
    { }

    #region Common

    public virtual async Task ReloadDataAsync()
    { }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
    }

    protected override bool OnBackButtonPressed()
    {
        AppShell.ShellRoutelBack();
        return true;
    }

    #endregion

    #region ActivityIndicator

    private ActivityIndicator? _indicator;
    private int _busyCount;

    public void InitActivityIndicator(ActivityIndicator indicator)
    {
        _indicator = indicator;
    }

    public void InitActivityIndicator(PageHeader header)
    {
        _indicator = header.Indicator;
    }

    private void ShowActivityIndicator()
    {
        if (Interlocked.Increment(ref _busyCount) == 1)
        {
            _indicator?.IsRunning = true;
        }
    }

    private void HideActivityIndicator()
    {
        if (Interlocked.Decrement(ref _busyCount) <= 0)
        {
            Interlocked.Exchange(ref _busyCount, 0);
            _indicator?.IsRunning = false;
        }
    }

    protected async Task RunBusyAsync(Func<Task> action)
    {
        ShowActivityIndicator();

        try
        {
            await action();
        }
        finally
        {
            HideActivityIndicator();
        }
    }

    protected async Task<T> RunBusyAsync<T>(Func<Task<T>> action)
    {
        ShowActivityIndicator();

        try
        {
            return await action();
        }
        finally
        {
            HideActivityIndicator();
        }
    }

    #endregion

    #region IQueryAttributable

    private bool _queryAttributed = false;

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (_queryAttributed) return;

        if (query.TryGetValue(AppShell.Data, out var item))
        {
            if (item != null)
            {
                _queryAttributed = true;

                await SetQueryData(item);
            }
        }
    }

    protected virtual async Task SetQueryData(object data)
    { }

    #endregion

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
}

