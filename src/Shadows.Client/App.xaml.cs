using Shadows.Client.Services;
using Shadows.Data.Model;

namespace Shadows.Client;

public partial class App : Application
{
    private static CharacterData actualCharacter = new();
    private static SessionData sessionData = new();
    private static readonly MessageService messageService = new();

    public App()
    {
        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        var window = new Window();
        if (!Windows.Any())
        {
            window.Page = new AppShell();
        }
        return window;
    }

    #region Login

    public static void SetCharacter(CharacterData chData)
    {
        actualCharacter = chData;
        messageService.Show(chData);
    }

    public static void SetCharacter(CharacterData chData, SessionData sData)
    {
        actualCharacter = chData;
        sessionData = sData;
        messageService.Show(chData);
    }

    public static void ClearCharacter()
    {
        actualCharacter = new CharacterData();
        sessionData = new SessionData();
    }

    public static CharacterData CharacterData
    {
        get
        {
            return actualCharacter;
        }
    }

    public static SessionData SessionData
    {
        get
        {
            return sessionData;
        }
    }

    public static bool ExistCharacter()
    {
        return (!String.IsNullOrEmpty(actualCharacter.UserName)) && (!String.IsNullOrEmpty(actualCharacter.Password));
    }
    #endregion

    #region Styles

    public const string StandardButton = "standardButton";
    public const string MenuButton = "menuButton";

    public const string ThinButton = "thinButton";
    public const string ThinButtonSelected = "thinButtonSelected";

    public const string Text = "text";
    public const string SubCaption = "subCaption";
    public const string LabelRed = "labelRed";
    public const string LabelGreen = "labelGreen";

    public const string FrameText = "frameText";

    public const string FramePopup = "framePopup";
    public const string FramePopupRed = "framePopupRed";

    public const string BorderPopup = "borderPopup";
    public const string BorderPopupRed = "borderPopupRed";

    public static Style? GetStyle(string styleName)
    {
        if (Application.Current != null)
        {
            ResourceDictionary dictionary = Application.Current.Resources.MergedDictionaries.ElementAt(1);

            Style? style = (Style)dictionary[styleName];

            if (style != null) return style;
        }

        return null;
    }
    #endregion
}
