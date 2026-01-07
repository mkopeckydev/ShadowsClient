using Shadows.Data.Facade;
using Shadows.Data.Model;
using Microsoft.Maui.Graphics;

namespace Shadows.Api.WebApi
{
    public partial class CharacterActivity
    {
        public bool Checked1Visible
        {
            get
            {
                return CheckCount >= 1;
            }
        }

        public bool Checked2Visible
        {
            get
            {
                return CheckCount >= 2;
            }
        }

        public bool Checked3Visible
        {
            get
            {
                return CheckCount >= 3;
            }
        }

        public bool Checked4Visible
        {
            get
            {
                return CheckCount >= 4;
            }
        }

        public bool Checked5Visible
        {
            get
            {
                return CheckCount >= 5;
            }
        }

        public Color ActivityColor
        {
            get
            {
                return ColorFacade.GetSystemColor(this.Color);
            }
        }

        private ListObject? _colorObject;

        public ListObject ColorObject
        {
            get
            {
                if (_colorObject == null)
                {
                    _colorObject = ColorFacade.GetColor(this.Color);
                }

                return _colorObject;
            }
            set
            {
                _colorObject = value;

                if (_colorObject != null)
                {
                    this.Color = _colorObject.Code;
                }
                else
                {
                    this.Color = ColorFacade.COLOR_WHITE;
                }
            }
        }
    }
}
