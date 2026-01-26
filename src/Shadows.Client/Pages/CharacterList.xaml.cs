using AndroidX.Core.View;
using Plugin.MauiMtAdmob.Extra;
using Shadows.Api;
using Shadows.Client.Controls;
using Shadows.Data.Facade;
using Shadows.Data.Model;

namespace Shadows.Client.Pages;

public partial class CharacterList : BaseContentPage
{
    public CharacterList()
    {
        InitializeComponent();
        InitActivityIndicator(aiLoginPage);

        adBanner.AdsId = AdMobData.BANNER_ID;
    }

    public override async Task ReloadDataAsync()
    {
        lblVersion.Text = String.Format("Verze {0}", AppInfo.Current.VersionString);

        List<CharacterData> characters = new List<CharacterData>();

        App.ClearCharacter();
        CharacterDataFacade fLoginData = new CharacterDataFacade();
        characters = fLoginData.GetList();

        stCharacters.Children.Clear();
        foreach (CharacterData ch in characters)
        {
            DataButton b = new DataButton();
            b.Text = ch.DisplayName;
            b.Data = ch;
            b.Style = App.GetStyle(App.MenuButton);
            b.Clicked += btnCharacterClick;
            stCharacters.Children.Add(b);
        }

        adBanner.LoadAd();
    }

    private async void btnCharacterClick(object? sender, EventArgs e)
    {
        if (sender == null) return;

        DataButton b = (DataButton)sender;
        if (b.Data != null)
        {
            CharacterData data = (CharacterData)b.Data;

            if (data.IsNew)
            {
                await AppShell.ShellRouteAsync(AppShell.RouteCharacterDetail);
            }
            else
            {
                Login(data);
            }
        }
    }

    private async void Login(CharacterData data)
    {
        SessionData sessionData = new SessionData();

        ShowActivityIndicator();

        var loginOk = await ServerApi.LoginAsync(data);

        if (loginOk)
        {
            sessionData.Skils = await ServerApi.SkillListAsync(data);
        }

        HideActivityIndicator();

        App.SetCharacter(data, sessionData);

        if (loginOk)
        {
            await AppShell.ShellRouteAsync(AppShell.RouteCharacterMainPage);
        }
        else
        {
            await ShowMessageBoxWarning("Chyba", "Pøihlášení se nezdaøilo, zkontroluj nastavení.");

            await AppShell.ShellRouteAsync(AppShell.RouteCharacterDetail);
        }
    }

    private void adBanner_AdsFailedToLoad(object? sender, MTEventArgs e)
    {
        if (e.ErrorMessage != null)
        {
            LogFacade.Add("adBanner_AdsFailedToLoad", e.ErrorMessage);
        }
    }
}