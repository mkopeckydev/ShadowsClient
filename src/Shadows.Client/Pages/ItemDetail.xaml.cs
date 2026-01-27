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

public partial class ItemDetail : BaseContentPage
{
    public int _dataId;
    private ItemData _data = new ItemData();

    public ItemDetail()
    {
        InitializeComponent();
        InitActivityIndicator(pageHeader);
    }

    private async void pageHeader_DoneClicked(object sender, EventArgs e)
    {
        string note = eNote.Text.Trim();

        ShowActivityIndicator();

        await ServerApi.SaveItemNoteAsync(_dataId, note, App.CharacterData);

        HideActivityIndicator();

        await AppShell.ShellRoutelBackAsync();
    }

    protected override async Task SetQueryData(object data)
    {
        if (data is int)
        {
            _dataId = (int)data;

            ShowActivityIndicator();

            _data = await ServerApi.ItemDetailAsync(_dataId, App.CharacterData);

            HideActivityIndicator();

            BindingContext = _data;

            pageHeader.Caption = _data.Item.Caption;

            if (_data.ImageData.Length > 0)
            {
                imgMainImage.Source = ImageSource.FromStream(() => new MemoryStream(_data.ImageData));
            }
            else
            {
                bMainImage.IsVisible = false;
            }

            eNote.Text = String.Format("{0} ", _data.ItemNote);

            cvDetail.ItemsSource = _data.List;
        }
    }
}