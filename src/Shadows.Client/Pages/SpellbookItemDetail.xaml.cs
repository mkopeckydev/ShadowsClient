using Shadows.Api;
using Shadows.Api.WebApi;
using Shadows.Client.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadows.Client.Pages;

public partial class SpellbookItemDetail : BaseContentPage
{
    public int _dataId;
    private SpellbookItemData _data = new();

    public SpellbookItemDetail()
    {
        InitializeComponent();
        InitActivityIndicator(pageHeader);
    }

    private async void pageHeader_DoneClicked(object sender, EventArgs e)
    {
        string note = eNote.Text.Trim();

        ShowActivityIndicator();

        await ServerApi.SaveSpellbookItemNoteAsync(_dataId, note, App.CharacterData);

        HideActivityIndicator();

        await AppShell.ShellRoutelBackAsync();
    }

    protected override async Task SetQueryData(object data)
    {
        if (data is int)
        {
            _dataId = (int)data;

            ShowActivityIndicator();

            _data = await ServerApi.SpellbookItemDetailAsync(_dataId, App.CharacterData);

            HideActivityIndicator();

            BindingContext = _data.Item;

            pageHeader.Caption = _data.Item.Caption;
            eNote.Text = String.Format("{0} ", _data.ItemNote);
        }
    }
}