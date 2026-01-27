using Shadows.Client.Controls;
using Shadows.Client.Pages;

namespace Shadows.Client;

public partial class AppShell : Shell
{
    public const string RouteCharacterList = "//CharacterList";
    public const string RouteCharacterDetail = "//CharacterList/CharacterDetail";

    public const string RouteCharacterMainPage = "//MainPage/CharacterMainPage";

    public const string RouteItemDetail = "//MainPage/ItemSearch/ItemDetail";

    public const string RouteSpellbookItemDetail = "//MainPage/SpellbookItemSearch/SpellbookItemDetail";

    public const string RouteCustomCounterList = "//MainPage/CharacterAdminPage/CustomCounterList";
    public const string RouteCustomCounterDetail = "//MainPage/CharacterAdminPage/CustomCounterList/CustomCounterDetail";

    public const string RouteActivityList = "//MainPage/CharacterAdminPage/ActivityList";
    public const string RouteActivityDetail = "//MainPage/CharacterAdminPage/ActivityList/ActivityDetail";
    
    public const string RouteFightNumberList = "//MainPage/CharacterAdminPage/FightNumberList";
    public const string RouteFightNumberDetail = "//MainPage/CharacterAdminPage/FightNumberList/FightNumberDetail";

    public const string Data = "Data";

    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(RouteCharacterDetail, typeof(CharacterDetail));
        Routing.RegisterRoute(RouteCustomCounterList, typeof(CustomCounterList));
        Routing.RegisterRoute(RouteCustomCounterDetail, typeof(CustomCounterDetail));
        Routing.RegisterRoute(RouteActivityList, typeof(ActivityList));
        Routing.RegisterRoute(RouteActivityDetail, typeof(ActivityDetail));
        Routing.RegisterRoute(RouteFightNumberList, typeof(FightNumberList));
        Routing.RegisterRoute(RouteFightNumberDetail, typeof(FightNumberDetail));
        Routing.RegisterRoute(RouteItemDetail, typeof(ItemDetail));
        Routing.RegisterRoute(RouteSpellbookItemDetail, typeof(SpellbookItemDetail));
    }

    public static async Task ShellRoutelBackAsync()
    {
        await Shell.Current.GoToAsync("..");
    }

    public static void ShellRoutelBack()
    {
        Shell.Current.GoToAsync("..");
    }

    public static async Task ShellRouteAsync(String route)
    {
        await Shell.Current.GoToAsync(route);
    }

    public static async Task ShellRouteAsync(String route, object data)
    {
        var navigationParameter = new Dictionary<string, object>{ { Data, data } };
        await Shell.Current.GoToAsync(route, navigationParameter);
    }

    protected override async void OnNavigated(ShellNavigatedEventArgs args)
    {
        base.OnNavigated(args);

        if (Shell.Current.CurrentPage is BaseContentPage)
        {
            BaseContentPage page = (BaseContentPage)Shell.Current.CurrentPage;

            await page.ReloadDataAsync();
        }
    }
}
