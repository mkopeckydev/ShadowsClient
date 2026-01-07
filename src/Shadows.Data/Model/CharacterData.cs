using Shadows.Data.Tools;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shadows.Data.Model
{
    [Serializable]
    public class CharacterData
    {
        public const string FILE_EXTENSION = "chr";
        public const string NEW_LABEL = "Přidat postavu";

        public string UserName { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;
        public string DisplayName { get; set; } = String.Empty;
        public string FileName { get; set; } = String.Empty;

        public CharacterData()
        { }

        public CharacterData(string displayName) : base()
        {
            this.DisplayName = displayName;
        }

        public CharacterData(string userName, string password, string displayName) : base()
        {
            this.UserName = userName;
            this.Password = password;
            this.DisplayName = displayName;
        }

        public void GenerateFileName()
        {
            if (String.IsNullOrEmpty(FileName))
            {
                FileName = String.Format("{0}.{1}", DateTime.Now.ToString("yyyyMMddhhmmss"), FILE_EXTENSION);
            }
        }

        public bool IsNew
        {
            get
            {
                return (DisplayName == NEW_LABEL);
            }
        }

        public bool CheckData()
        {
            return !CommonTools.IsEmpty(UserName, Password, DisplayName);
        }

        public static CharacterData GetDemo()
        {
            return new CharacterData("demo", "demo", "Ukázková postava");
        }

        public bool IsDemo
        {
            get
            {
                return (UserName == "demo");
            }
        }
    }
}
