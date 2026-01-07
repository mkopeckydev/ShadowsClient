using Shadows.Data.Model;
using Microsoft.Maui.Graphics;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadows.Data.Facade
{
    public class ColorFacade
    {
        public const string COLOR_RED = "R";
        public const string COLOR_GREEN = "G";
        public const string COLOR_BLUE = "B";
        public const string COLOR_YELLOW = "Y";
        public const string COLOR_WHITE = "L";
        public const string COLOR_ORANGE = "O";
        public const string COLOR_BROWN = "H";
        public const string COLOR_PURPLE = "P";
        public const string COLOR_GRAY = "A";

        private static List<ListObject> colors = new List<ListObject>();

        public static List<ListObject> GetColors()
        {
            if (colors.Count == 0)
            {
                colors = new List<ListObject>();
                colors.Add(new ListObject(COLOR_YELLOW, "Žlutá"));
                colors.Add(new ListObject(COLOR_ORANGE, "Oranžová"));
                colors.Add(new ListObject(COLOR_RED, "Červená"));
                colors.Add(new ListObject(COLOR_PURPLE, "Fialová"));
                colors.Add(new ListObject(COLOR_BLUE, "Modrá"));
                colors.Add(new ListObject(COLOR_GREEN, "Zelená"));
                colors.Add(new ListObject(COLOR_GRAY, "Šedá"));
                colors.Add(new ListObject(COLOR_WHITE, "Bílá"));
            }
                
            return colors;
        }

        public static ListObject GetColor(string code)
        {
            ListObject? c = GetColors().Find(x => x.Code == code);
            if (c != null)
            {
                return c;
            }
            else
            {
                return GetColors()[0];
            }
        }

        public static Color GetSystemColor(string code)
        {
            if (code == COLOR_YELLOW) return Colors.Yellow;
            if (code == COLOR_ORANGE) return Colors.Orange;
            if (code == COLOR_RED) return Colors.Red;
            if (code == COLOR_PURPLE) return Colors.Purple;
            if (code == COLOR_BLUE) return Colors.LightBlue;
            if (code == COLOR_GREEN) return Colors.LightGreen;
            if (code == COLOR_GRAY) return Colors.LightGray;
            if (code == COLOR_WHITE) return Colors.White;

            return Colors.White;
        }
    }
}
