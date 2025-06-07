using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMR_Pathfinding
{
    enum CellMoveState
    {
        None,
        Start,
        End,
        Wall,
        NoneWall
    }

    enum SelectedAlgo
    {
        None,
        BreathFirst,
        DepthFirst,
        Dijkstra,
        A
    }
}
