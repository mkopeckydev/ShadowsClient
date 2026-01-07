using System;
using System.Collections.Generic;
using System.Text;

namespace Shadows.Data.Model
{
    [Serializable]
    public class ListObject
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Caption { get; set; }

        public ListObject(string code, string caption)
        {
            Code = code;
            Caption = caption;

            if (String.IsNullOrEmpty(Caption)) Caption = String.Empty;
        }

        public ListObject(int id, string caption)
        {
            Id = id;
            Code = id.ToString();
            Caption = caption;

            if (String.IsNullOrEmpty(Caption)) Caption = String.Empty;
        }

        public override string ToString() 
        {
            return Caption;
        }
    }
}
