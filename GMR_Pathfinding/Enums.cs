using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMR_Pathfinding
{
    public enum CellMoveState
    {
        None,
        Start,
        End,
        Wall,
        NoneWall
    }

    public enum SelectedAlgo
    {
        None,
        BreathFirst,
        DepthFirst,
        Dijkstra,
        AStarManhattan,
        AStarEuclidean
    }

    public enum SelectedHeuristic
    { 
        None,
        Manhattan,
        Euclidean
    }
}
