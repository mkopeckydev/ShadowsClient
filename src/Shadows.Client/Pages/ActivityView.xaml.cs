using CommunityToolkit.Maui.Extensions;
using Shadows.Api;
using Shadows.Api.WebApi;
using Shadows.Client.Controls;
using Shadows.Client.Popups;
using Shadows.Data.Tools;

namespace Shadows.Client.Pages;

public partial class ActivityView : BaseContentPage
{
    private bool _loading;

    public ActivityView()
    {
        InitializeComponent();
        InitActivityIndicator(pageHeader);
        _loading = false;
    }

    public override async Task ReloadDataAsync()
    {
        ShowActivityIndicator();

        var list = await ServerApi.GetCharacterActivityListAsync(App.CharacterData);

        HideActivityIndicator();

        _loading = true;

        lwActivities.ItemsSource = list;

        _loading = false;
    }

    private void CheckBox5_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (_loading) return;

        CheckBoxCheckedChanged(5, (CheckBox)sender);
    }

    private void CheckBox4_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (_loading) return;

        CheckBoxCheckedChanged(4, (CheckBox)sender);
    }

    private void CheckBox3_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (_loading) return;

        CheckBoxCheckedChanged(3, (CheckBox)sender);
    }

    private void CheckBox2_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (_loading) return;

        CheckBoxCheckedChanged(2, (CheckBox)sender);
    }

    private void CheckBox1_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (_loading) return;

        CheckBoxCheckedChanged(1, (CheckBox)sender);
    }

    private async void CheckBoxCheckedChanged(int index, CheckBox ch)
    {
        CharacterActivity a = (CharacterActivity)ch.BindingContext;
        
        if (index == 5)
        {
            a.Checked5 = ch.IsChecked;
        }
        else if (index == 4)
        {
            a.Checked4 = ch.IsChecked;
        }
        else if (index == 3)
        {
            a.Checked3 = ch.IsChecked;
        }
        else if (index == 2)
        {
            a.Checked2 = ch.IsChecked;
        }
        else if (index == 1)
        {
            a.Checked1 = ch.IsChecked;
        }

        ShowActivityIndicator();

        await ServerApi.SaveCharacterActivityAsync(a, App.CharacterData);

        HideActivityIndicator();
    }

    private async void pageHeader_RefreshClicked(object sender, EventArgs e)
    {
        bool c = await ShowMessageBoxConfirm("Vynulování činnosti", "Opravdu vynulovat činnost?");

        if (!c) return;

        ShowActivityIndicator();

        var list = await ServerApi.GetCharacterActivityListAsync(App.CharacterData);

        foreach (CharacterActivity i in list)
        {
            i.Checked1 = false;
            i.Checked2 = false;
            i.Checked3 = false;
            i.Checked4 = false;
            i.Checked5 = false;

            await ServerApi.SaveCharacterActivityAsync(i, App.CharacterData);
        }

        HideActivityIndicator();

        await ReloadDataAsync();
    }
}