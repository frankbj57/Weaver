using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weaver
{
    public class WarpThread
    {
       public System.Drawing.Color ThreadColor
        {
            get;
            set;
        }

        public WarpThread(System.Drawing.Color color)
        {
            ThreadColor = color;
        }

    }
}
