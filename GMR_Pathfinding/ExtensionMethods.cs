using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMR_Pathfinding
{
    public static class ExtensionMethods
    {
        public static bool RGBEquality(this Color a, Color b)
        { 
            return a.R == b.R && a.G == b.G && a.B == b.B;
        }
    }
}
