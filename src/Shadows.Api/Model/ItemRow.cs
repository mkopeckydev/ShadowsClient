using Shadows.Data.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadows.Api.WebApi
{
    public partial class ItemRow
    {
        public bool InfoCaptionVisible
        {
            get
            {
                return !String.IsNullOrEmpty(InfoCaptionCleared);
            }
        }

        public bool InfoTextVisible
        {
            get
            {
                return !String.IsNullOrEmpty(InfoTextCleared);
            }
        }

        public bool Visible
        {
            get
            {
                return InfoCaptionVisible || InfoTextVisible;
            }
        }

        public string InfoCaptionCleared
        {
            get
            {
                return CommonTools.ClearText(InfoCaption);
            }
        }

        public string InfoTextCleared
        {
            get
            {
                return CommonTools.ClearText(InfoText);
            }
        }
    }
}
