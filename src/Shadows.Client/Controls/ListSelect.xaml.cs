using CommunityToolkit.Maui.Extensions;
using Shadows.Client.Popups;
using Shadows.Data.Model;
using Shadows.Data.Tools;

namespace Shadows.Client.Controls;

public partial class ListSelect : ContentView
{

    BaseContentPage? _page;
    List<ListObject> _dataSource = new List<ListObject>();
    ListObject? _selectedItem = null;
    string _caption = String.Empty;

    public event EventHandler<EventArgs>? SelectedChanged;

    public ListSelect()
    {
        InitializeComponent();
    }

    public void Init(string caption, List<ListObject> dataSource, BaseContentPage page)
    {
        _caption = caption;
        _dataSource = dataSource;
        _page = page;

        if ((_dataSource == null) || (_dataSource.Count == 0))
        {
            SelectedItem = null;
            btnSelect.IsEnabled = false;
        }
        else
        {
            SelectedItem = _dataSource[0];
            btnSelect.IsEnabled = true;
        }
    }

    private async void btnSelect_Clicked(object sender, EventArgs e)
    {
        if (_page == null) return;

        ItemSelect p = new ItemSelect(_caption, _dataSource);
        bool selected = await _page.ShowPopupBoolAsync(p);

        if (selected)
        {
            SelectedItem = p.SelectedItem;

            if (SelectedChanged != null) { SelectedChanged(this, EventArgs.Empty); }
        }
    }

    public ListObject? SelectedItem
    {
        get
        {
            return _selectedItem;
        }
        set
        {
            _selectedItem = value;

            if (_selectedItem != null)
            {
                lblCaption.Text = _selectedItem.Caption;
            }
            else
            {
                lblCaption.Text = String.Empty;
            }
        }
    }

    public int SelectedId
    {
        get
        {
            return (_selectedItem != null) ? _selectedItem.Id : -1; 
        }
        set
        {
            SelectedItem = _dataSource.Find(x => x.Id == value);
        }
    }

}