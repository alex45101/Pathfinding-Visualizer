using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMR_Pathfinding
{
    public class Grid : IDrawable
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public int CellSize { get; private set; }

        Cell[] cells;

        public Grid(int width, int height, int cellSize) 
        { 
            Width = width;
            Height = height;
            CellSize = cellSize;

            CreateCells();
        }

        private void CreateCells()
        {
            cells = new Cell[Width * Height];

            int index = 0;
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++, index++)
                {
                    cells[index] = new Cell(new Point(j * CellSize, i * CellSize), CellSize, 5);
                }
            }

            cells[0].FillColor = Color.Green;
        }

        public void Draw(Graphics gfx)
        {
            for (int i = 0; i < cells.Length; i++)
            {
                cells[i].Draw(gfx);
            }
        }
    }
}
