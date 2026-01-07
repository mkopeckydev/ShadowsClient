using Shadows.Api;
using Shadows.Api.WebApi;
using Shadows.Client.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadows.Client.Pages;

[QueryProperty(nameof(DataId), "Data")]
public partial class SpellbookItemDetail : BaseContentPage
{
    public int DataId { get; set; }
    public SpellbookItemData Data { get; set; } = new();

    public SpellbookItemDetail()
    {
        InitializeComponent();
        InitActivityIndicator(pageHeader);
    }

    private async void pageHeader_DoneClicked(object sender, EventArgs e)
    {
        string note = eNote.Text.Trim();

        ShowActivityIndicator();

        await ServerApi.SaveSpellbookItemNoteAsync(DataId, note, App.CharacterData);

        HideActivityIndicator();

        await AppShell.ShellRoutelBackAsync();
    }

    public override async Task ReloadDataAsync()
    {
        ShowActivityIndicator();

        Data = await ServerApi.SpellbookItemDetailAsync(DataId, App.CharacterData);

        HideActivityIndicator();

        BindingContext = Data.Item;

        pageHeader.Caption = Data.Item.Caption;
        eNote.Text = String.Format("{0} ", Data.ItemNote);
    }
}