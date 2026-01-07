using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Shadows.Data
{
    public class AppFiles
    {
        private const string DIR_CHARACTERS = "characters";
        private const string DIR_ITEMIMAGES = "itemimages";
        private const string DIR_LOG = "log";

        public static DirectoryInfo GetCharactersDir()
        {
            return GetDir(DIR_CHARACTERS);
        }

        public static DirectoryInfo GetItemImagesDir()
        {
            return GetDir(DIR_ITEMIMAGES);
        }

        public static DirectoryInfo GetLogDir()
        {
            return GetDir(DIR_LOG);
        }

        private static DirectoryInfo GetDir(string name)
        {
            string appDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

            string charDir = Path.Combine(appDir, name);

            DirectoryInfo di = new DirectoryInfo(charDir);

            if (!di.Exists) di.Create();

            return di;
        }
    }
}
