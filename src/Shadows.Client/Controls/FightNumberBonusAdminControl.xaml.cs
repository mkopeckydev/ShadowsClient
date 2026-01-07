using Shadows.Api.WebApi;
using Shadows.Data.Tools;

namespace Shadows.Client.Controls;

public partial class FightNumberBonusAdminControl : ContentView
{
    public FightNumberBonus Data;

    public FightNumberBonusAdminControl(FightNumberBonus data)
    {
        InitializeComponent();
        Data = data;
        BindingContext = Data;
    }
}
