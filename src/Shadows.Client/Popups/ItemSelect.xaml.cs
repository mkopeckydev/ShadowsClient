using CommunityToolkit.Maui.Views;
using Shadows.Data.Model;

namespace Shadows.Client.Popups;

public partial class ItemSelect : Popup<bool>
{
    ListObject? _selectedItem;

    public ItemSelect(string caption, List<ListObject> dataSource)
	{
		InitializeComponent();
        lblCaption.Text = caption;
        cwItems.ItemsSource = dataSource;
    }

    private async void btnClose_Clicked(object sender, EventArgs e)
    {
        await this.CloseAsync(false);
    }

    public ListObject? SelectedItem
    {
        get
        {
            return _selectedItem;
        }
    }

    private async void Selection_Tapped(object sender, TappedEventArgs e)
    {
        if (e.Parameter != null)
        {
            _selectedItem = e.Parameter as ListObject;
            await this.CloseAsync(true);
        }
    }
}