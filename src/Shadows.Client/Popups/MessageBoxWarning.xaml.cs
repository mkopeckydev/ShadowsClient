using CommunityToolkit.Maui.Views;

namespace Shadows.Client.Popups;

public partial class MessageBoxWarning : Popup
{
	public MessageBoxWarning(string caption, string text)
	{
		InitializeComponent();
        lblCaption.Text = caption;
        lblText.Text = text;
	}

    private async void btnClose_Clicked(object sender, EventArgs e)
    {
        await this.CloseAsync();
    }
}