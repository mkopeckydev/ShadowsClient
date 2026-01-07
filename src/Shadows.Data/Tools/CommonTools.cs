using Shadows.Data.Model;
using System;
using System.Collections.Generic;
using Microsoft.Maui.Graphics;

namespace Shadows.Data.Tools
{
    public class CommonTools
    {
        #region String

        public static bool IsEmpty(params string[] textItems)
        {
            bool result = false;

            foreach (string s in textItems)
            {
                if (String.IsNullOrEmpty(s)) result = true;
            }

            return result;
        }

        public static string ClearText(string s)
        {
            if (String.IsNullOrEmpty(s)) return String.Empty;

            s = s.Replace("\t", String.Empty);

            return s.Trim();
        }


        public static int StringToInt(string s)
        {
            if (String.IsNullOrEmpty(s)) return 0;
            try
            {
                return Convert.ToInt32(s);
            }
            catch
            {
                return 0;
            }
        }

        public static string JoinString(string s, string s2, string separator)
        {
            if (String.IsNullOrEmpty(s))
            {
                return s2;
            }
            else
            {
                if (!String.IsNullOrEmpty(s2))
                {
                    return s + separator + s2;
                }
                else
                {
                    return s;
                }
            }
        }

        #endregion

        #region Numeric
        public static List<ListObject> GetListInt(int count)
        {
            List<ListObject> l = new List<ListObject>();
            for (int i = 1; i <= count; i++)
            {
                l.Add(new ListObject(i, i.ToString()));
            }
            return l;
        }
        #endregion

        #region Object
        public static bool ObjectToBool(object o)
        {
            if (o == null) return false;

            try
            {
                return Convert.ToBoolean(o);
            }
            catch
            {
                return false;
            }
        }

        public static string ObjectToString(object o)
        {
            string? s = Convert.ToString(o);

            if (s != null)
            {
                return s;
            }
            else
            {
                return String.Empty;
            }
        }
        #endregion
    }
}
