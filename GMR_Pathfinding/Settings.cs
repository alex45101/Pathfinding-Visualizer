using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMR_Pathfinding
{
    public static class Settings
    {
        public static readonly int GridSize = 15;
        public static readonly int CellSize = 30;
        public static readonly int Thickness = 3;

        public static readonly Color StartColor = Color.Green;
        public static readonly Color EndColor = Color.Red;
        public static readonly Color NoneWallColor = Color.White;
        public static readonly Color WallColor = Color.Gray;
        public static readonly Color VisitedColor = Color.CornflowerBlue;
        public static readonly Color ToBeVisitedColor = Color.Yellow;
        public static readonly Color PathColor = Color.DarkOrange;
        public static readonly Color DefaultCellColor = Color.White;
    }
}
