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
        public int Thickness { get; private set; }

        Vertex<Cell> startPoint;
        Vertex<Cell> endPoint;

        

        Graph<Cell> graph;
        Vertex<Cell>[] cells;

        public Grid(int width, int height, int cellSize, int thickness) 
        { 
            Width = width;
            Height = height;
            CellSize = cellSize;
            Thickness = thickness;

            CreateCells();
        }

        private void CreateCells()
        {
            graph = new Graph<Cell>();

            //create vertices
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    graph.AddVertex(new Vertex<Cell>(new Cell(new Point(x * CellSize, y * CellSize), CellSize, Thickness)));
                }
            }

            //have a quick reference to the vertices in an array format
            cells = graph.Vertices.ToArray();

            //create edge connections
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    int index = y * Width + x;

                    //above curr cell
                    if (y > 0)
                    {
                        graph.AddEdge(cells[index], cells[(y - 1) * Width + x], 1);
                    }
                    //left curr cell
                    if (x > 0)
                    {
                        graph.AddEdge(cells[index], cells[y * Width + (x - 1)], 1);
                    }
                    //right curr cell
                    if (x < Width - 1)
                    {
                        graph.AddEdge(cells[index], cells[y * Width + (x + 1)], 1);
                    }
                    //below curr cell
                    if (y < Height - 1)
                    {
                        graph.AddEdge(cells[index], cells[(y + 1) * Width + x], 1);
                    }
                }
            }

            //setup start point
            startPoint = cells[0];
            endPoint = cells[cells.Length - 1];

            startPoint.Value.FillColor = Color.Green;
            endPoint.Value.FillColor = Color.Red;
        }

        public void Update(bool mouseClick, Color selectedColor)
        { 
        
        }

        public void Draw(Graphics gfx)
        {
            for (int i = 0; i < cells.Length; i++)
            {
                cells[i].Value.Draw(gfx);
            }
        }
    }
}
