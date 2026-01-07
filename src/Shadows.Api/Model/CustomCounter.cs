using Shadows.Data.Facade;
using Shadows.Data.Model;

namespace Shadows.Api.WebApi
{
    public partial class CustomCounter
    {
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
