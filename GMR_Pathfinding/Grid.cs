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

        enum CellMoveState
        {
            None,
            Start,
            End,
            Wall,
            NoneWall
        }

        //TODO fix
        //need to maintain walls too
        Vertex<Cell> startPoint;
        Vertex<Cell> endPoint;
        List<Cell> walls;

        Color startColor = Color.Green;
        Color endColor = Color.Red;
        Color noneWallColor = Color.White;
        Color wallColor = Color.Gray;

        CellMoveState moveState = CellMoveState.None;
        CellMoveState prevMoveState;

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

        public void Update(bool mouseDown, bool prevMouseDown, Point mousePos, Color selectedColor, Size imageSize)
        {
            //TODO fix start and end point disappearing
            if (mouseDown && !prevMouseDown)
            {
                //moving start point
                if (selectedColor.R == startColor.R && selectedColor.G == startColor.G && selectedColor.B == startColor.B)
                {
                    moveState = CellMoveState.Start;
                }
                //moving end point
                else if (selectedColor.R == endColor.R && selectedColor.G == endColor.G && selectedColor.B == endColor.B)
                {
                    moveState = CellMoveState.End;
                }
                //walls
                else if (prevMoveState != CellMoveState.NoneWall && selectedColor.R == noneWallColor.R && selectedColor.G == noneWallColor.G && selectedColor.B == noneWallColor.B)
                {
                    moveState = CellMoveState.Wall;
                }
                //nonewalls
                else if (prevMoveState != CellMoveState.Wall && selectedColor.R == wallColor.R && selectedColor.G == wallColor.G && selectedColor.B == wallColor.B)
                {
                    moveState = CellMoveState.NoneWall;
                }
            }
            else
            {
                moveState = CellMoveState.None;
            }

            //debug
            Console.WriteLine(moveState.ToString());

            if (moveState != CellMoveState.None)
            {
                Point cellPoint = new Point(
                    (int)(mousePos.X * (float)Form1.GridSize / imageSize.Width),
                    (int)(mousePos.Y * (float)Form1.GridSize / imageSize.Height)
                    );

                int index = GetIndex(cellPoint.X, cellPoint.Y);

                switch (moveState)
                {
                    case CellMoveState.Start:
                        SetStartPoint(index);
                        break;
                    case CellMoveState.End:
                        SetEndPoint(index);
                        break;
                    case CellMoveState.Wall:
                        SetWall(index, cellPoint.X, cellPoint.Y);
                        break;
                    case CellMoveState.NoneWall:
                        RemoveWall(index, cellPoint.X, cellPoint.Y);
                        break;
                }
            }

            prevMoveState = moveState;
        }

        public void Draw(Graphics gfx)
        {
            for (int i = 0; i < cells.Length; i++)
            {
                cells[i].Value.Draw(gfx);
            }
        }

        private int GetIndex(int x, int y)
        {
            return y * Width + x;
        }

        private void CreateCells()
        {
            graph = new Graph<Cell>();
            walls = new List<Cell>();

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
                    AddEdges(x, y);
                }
            }

            //setup start point
            SetStartPoint(0);
            SetEndPoint(cells.Length - 1);
        }

        private void AddEdges(int x, int y)
        {
            int index = GetIndex(x, y);

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

        private void RemoveEdges(int x, int y)
        {
            int index = GetIndex(x, y);

            //above curr cell
            if (y > 0)
            {
                graph.RemoveEdge(cells[index], cells[(y - 1) * Width + x]);
            }
            //left curr cell
            if (x > 0)
            {
                graph.RemoveEdge(cells[index], cells[y * Width + (x - 1)]);
            }
            //right curr cell
            if (x < Width - 1)
            {
                graph.RemoveEdge(cells[index], cells[y * Width + (x + 1)]);
            }
            //below curr cell
            if (y < Height - 1)
            {
                graph.RemoveEdge(cells[index], cells[(y + 1) * Width + x]);
            }
        }

        private void SetStartPoint(int index)
        {
            if (!walls.Contains(cells[index].Value))
            {
                if (startPoint != null)
                {
                    startPoint.Value.FillColor = Color.White;
                }

                startPoint = cells[index];
                startPoint.Value.FillColor = startColor;
            }
        }

        private void SetEndPoint(int index)
        {
            if (!walls.Contains(cells[index].Value))
            {
                if (endPoint != null)
                {
                    endPoint.Value.FillColor = Color.White;
                }

                endPoint = cells[index];
                endPoint.Value.FillColor = endColor;
            }
        }

        private void SetWall(int index, int x, int y)
        {
            if (startPoint != cells[index] || endPoint != cells[index])
            {
                cells[index].Value.FillColor = wallColor;

                RemoveEdges(x, y);

                walls.Add(cells[index].Value);
            }
        }

        private void RemoveWall(int index, int x, int y)
        {
            if (startPoint != cells[index] || endPoint != cells[index])
            {
                cells[index].Value.FillColor = noneWallColor;

                AddEdges(x, y);

                walls.Remove(cells[index].Value);
            }
        }
    }
}
