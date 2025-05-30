using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMR_Pathfinding
{
    public class VisualState
    {
        HashSet<Vertex<Cell>> vistedCells;
        HashSet<Vertex<Cell>> toBeVisitedCells;

        public VisualState()
        { 
        
        }
    }
}
