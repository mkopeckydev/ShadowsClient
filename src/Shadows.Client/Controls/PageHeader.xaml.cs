namespace Shadows.Client.Controls;

public partial class PageHeader : ContentView
{
    public PageHeader()
    {
        InitializeComponent();
    }

    public event EventHandler? DoneClicked;
    public event EventHandler? DeleteClicked;
    public event EventHandler? RefreshClicked;
    public event EventHandler? DownloadClicked;
    public event EventHandler? PlusClicked;
    public event EventHandler? SearchClicked;

    public string Caption
    {
        get { return lblCaption.Text; }
        set { lblCaption.Text = value; }
    }

    public bool BackButton
    {
        get { return btnBack.IsVisible; }
        set { btnBack.IsVisible = value; }
    }

    public bool DoneButton
    {
        get { return btnDone.IsVisible; }
        set { btnDone.IsVisible = value; }
    }

    public bool DeleteButton
    {
        get { return btnDelete.IsVisible; }
        set { btnDelete.IsVisible = value; }
    }

    public bool RefreshButton
    {
        get { return btnRefresh.IsVisible; }
        set { btnRefresh.IsVisible = value; }
    }

    public bool DownloadButton
    {
        get { return btnDownload.IsVisible; }
        set { btnDownload.IsVisible = value; }
    }

    public bool PlusButton
    {
        get { return btnPlus.IsVisible; }
        set { btnPlus.IsVisible = value; }
    }

    public bool SearchButton
    {
        get { return btnSearch.IsVisible; }
        set { btnSearch.IsVisible = value; }
    }

    public ActivityIndicator Indicator
    {
        get { return aIndicator; }
    }

    private async void btnBack_Clicked(object sender, EventArgs e)
    {
        await AppShell.ShellRoutelBackAsync();
    }

    private void btnDone_Clicked(object sender, EventArgs e)
    {
        if (DoneClicked != null)
        {
            DoneClicked(this, EventArgs.Empty);
        }
    }

    private void btnDelete_Clicked(object sender, EventArgs e)
    {
        if (DeleteClicked != null)
        {
            DeleteClicked(this, EventArgs.Empty);
        }
    }

    private void btnRefresh_Clicked(object sender, EventArgs e)
    {
        if (RefreshClicked != null)
        {
            RefreshClicked(this, EventArgs.Empty);
        }
    }

    private void btnDownload_Clicked(object sender, EventArgs e)
    {
        if (DownloadClicked != null)
        {
            DownloadClicked(this, EventArgs.Empty);
        }
    }

    private void btnPlus_Clicked(object sender, EventArgs e)
    {
        if (PlusClicked != null)
        {
            PlusClicked(this, EventArgs.Empty);
        }
    }

    private void btnSearch_Clicked(object sender, EventArgs e)
    {
        if (SearchClicked != null)
        {
            SearchClicked(this, EventArgs.Empty);
        }
    }

    public void ShowPercent(int percent)
    {
        lblPercent.IsVisible = true;
        lblPercent.Text = $"{percent}%";
    }

    public void HidePercent()
    {
        lblPercent.IsVisible = false;
    }
}