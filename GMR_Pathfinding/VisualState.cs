using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMR_Pathfinding
{
    public class VisualState
    {
        protected Vertex<Cell> vistedCell;
        protected HashSet<Vertex<Cell>> toBeVisitedCells;

        public VisualState(Vertex<Cell> VisistedCell, HashSet<Vertex<Cell>> ToBeVisitedCells)
        { 
            vistedCell = VisistedCell;
            toBeVisitedCells = ToBeVisitedCells;
        }

        public virtual void SetColors()
        {
            if (vistedCell != null)
            {
                vistedCell.Value.FillColor = Settings.VisitedColor;
            }

            foreach (var cell in toBeVisitedCells)
            {
                cell.Value.FillColor = Settings.ToBeVisitedColor;
            }
        }

        public void ResetColors()
        {
            if (vistedCell != null)
            {
                vistedCell.Value.FillColor = Settings.ToBeVisitedColor;
            }

            foreach (var cell in toBeVisitedCells)
            {
                cell.Value.FillColor = Settings.DefaultCellColor;
            }
        }
    }

    public class VisualPath : VisualState
    {
        public VisualPath(HashSet<Vertex<Cell>> ToBeVisitedCells) 
            : base(null, ToBeVisitedCells)
        {
        }

        public override void SetColors()
        {
            foreach (var cell in toBeVisitedCells)
            {
                cell.Value.FillColor = Settings.PathColor;
            }
        }
    }
}
