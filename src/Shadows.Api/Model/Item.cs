using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Shadows.Api.WebApi
{
    public partial class Item
    {
        private string _imagesDir = String.Empty;

        public void Init(string imagesDir)
        {
            _imagesDir = imagesDir;
        }
        public string ImagePath
        {
            get
            {
                if (!String.IsNullOrEmpty(ImageName))
                {
                    return Path.Combine(_imagesDir, ImageName);
                }
                else
                {
                    return "scroll.png";
                }
            }
        }
    }
}
