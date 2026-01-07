using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadows.Data.Model
{
    public class SessionData
    {
        public List<ListObject> Skils { get; set; }

        public SessionData()
        {
            Skils = new List<ListObject>();
        }
    }
}
