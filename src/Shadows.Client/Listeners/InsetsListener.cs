using AndroidX.Core.View;

namespace Shadows.Client.Listeners
{
    public class InsetsListener : Java.Lang.Object, AndroidX.Core.View.IOnApplyWindowInsetsListener
    {
        public WindowInsetsCompat? OnApplyWindowInsets(Android.Views.View? v, WindowInsetsCompat? insets)
        {
            if (insets != null)
            {
                var systemBars = insets.GetInsets(WindowInsetsCompat.Type.SystemBars());

                if ((systemBars != null) && (v != null))
                {
                    v.SetPadding(0, systemBars.Top, 0, 0);
                    v.SetBackgroundColor(Android.Graphics.Color.Black); 
                }
            }

            return insets;
        }
    }
}
