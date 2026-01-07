using Shadows.Api;
using Shadows.Api.WebApi;
using Shadows.Client.Controls;
using Shadows.Data.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadows.Client.Pages;

[QueryProperty(nameof(DataId), "Data")]
public partial class ItemDetail : BaseContentPage
{
    public int DataId { get; set; }
    ItemData Data = new ItemData();

    public ItemDetail()
    {
        InitializeComponent();
        InitActivityIndicator(pageHeader);
    }

    private async void pageHeader_DoneClicked(object sender, EventArgs e)
    {
        string note = eNote.Text.Trim();

        ShowActivityIndicator();

        await ServerApi.SaveItemNoteAsync(DataId, note, App.CharacterData);

        HideActivityIndicator();

        await AppShell.ShellRoutelBackAsync();
    }

    public override async Task ReloadDataAsync()
    {
        ShowActivityIndicator();

        Data = await ServerApi.ItemDetailAsync(DataId, App.CharacterData);

        HideActivityIndicator();

        BindingContext = Data;

        pageHeader.Caption = Data.Item.Caption;

        if (Data.ImageData.Length > 0)
        {
            imgMainImage.Source = ImageSource.FromStream(() => new MemoryStream(Data.ImageData));
        }
        else
        {
            bMainImage.IsVisible = false;
        }
        
        eNote.Text = String.Format("{0} ", Data.ItemNote);

        cvDetail.ItemsSource = Data.List;
    }
}