using Shadows.Data.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadows.Api.WebApi
{
    public partial class SpellbookItem
    {
        public string AttributesCleared
        {
            get
            {
                return CommonTools.ClearText(Attributes);
            }
        }

        public string DescriptionCleared
        {
            get
            {
                return CommonTools.ClearText(Description);
            }
        }

        public string ModificationCleared
        {
            get
            {
                return CommonTools.ClearText(Modification);
            }
        }

        public string ModificationRuneMageCleared
        {
            get
            {
                return CommonTools.ClearText(ModificationRuneMage);
            }
        }
    }
}
