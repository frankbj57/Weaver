using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weaver;

namespace Weaver
{
    public class WaftThread
    {
        public System.Drawing.Color ThreadColor
        {
            get;
            set;
        }

        public Shaft Shaft
        {
            get;
            set;
        }

        public WaftThread(System.Drawing.Color color, Shaft shaft)
        {
            ThreadColor = color;
            Shaft = shaft;
        }
    }
}
