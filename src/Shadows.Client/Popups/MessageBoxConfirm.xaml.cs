using CommunityToolkit.Maui.Views;

namespace Shadows.Client.Popups;

public partial class MessageBoxConfirm : Popup<bool>
{
	public MessageBoxConfirm(string caption, string text)
	{
		InitializeComponent();
		lblCaption.Text = caption;
		lblText.Text = text;
	}

    private async void btnYes_Clicked(object sender, EventArgs e)
    {
        await this.CloseAsync(true);
    }

    private async void btnNo_Clicked(object sender, EventArgs e)
    {
        await this.CloseAsync(false);
    }
}