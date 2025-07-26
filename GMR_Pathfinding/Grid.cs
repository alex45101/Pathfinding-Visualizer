using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.WebSockets;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GMR_Pathfinding
{
    public class Grid : IDrawable
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public int CellSize { get; private set; }
        public int Thickness { get; private set; }

        //TODO fix
        //need to maintain walls too
        Vertex<Cell> startPoint;
        Vertex<Cell> endPoint;
        HashSet<Cell> walls;

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

            SetUpCells();
        }

        public void Update(bool mouseDown, bool prevMouseDown, Point mousePos, Color selectedColor)
        {
            if (prevMoveState == CellMoveState.None && mouseDown && !prevMouseDown)
            {
                //moving start point
                if (selectedColor.RGBEquality(Settings.StartColor))
                {
                    moveState = CellMoveState.Start;
                }
                //moving end point
                else if (selectedColor.RGBEquality(Settings.EndColor))
                {
                    moveState = CellMoveState.End;
                }
                //walls
                else if (prevMoveState != CellMoveState.NoneWall && selectedColor.RGBEquality(Settings.NoneWallColor))
                {
                    moveState = CellMoveState.Wall;
                }
                //nonewalls
                else if (prevMoveState != CellMoveState.Wall && selectedColor.RGBEquality(Settings.WallColor))
                {
                    moveState = CellMoveState.NoneWall;
                }
            }
            else if (!mouseDown && prevMouseDown)
            {
                moveState = CellMoveState.None;
            }

            //debug
            Debug.WriteLine(moveState.ToString());
            //debug

            if (moveState != CellMoveState.None)
            {
                MouseStateAction(mousePos);
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

        public void ResetVisuals()
        {
            for (int i = 0; i < cells.Length; i++)
            {
                if (cells[i] != startPoint
                    && cells[i] != endPoint
                    && !walls.Contains(cells[i].Value))
                {
                    cells[i].Value.FillColor = Settings.DefaultCellColor;
                }
            }
        }

        public Queue<VisualState> BreadthFirstVisual()
        {
            var visualStateWrapper = addToVisualStateWrapper();

            HashSet<Vertex<Cell>> path = graph
                .BreadthFirstPath(startPoint, endPoint, visualStateWrapper.Item1)
                .ToHashSet();

            //no need to include start and end as changes in visual state
            path.Remove(startPoint);
            path.Remove(endPoint);

            visualStateWrapper.Item2.Enqueue(new VisualPath(path));

            return visualStateWrapper.Item2;
        }

        public Queue<VisualState> DepthFirstVisual()
        {
            var visualStateWrapper = addToVisualStateWrapper();

            HashSet<Vertex<Cell>> path = graph
                .DepthFirstPath(startPoint, endPoint, visualStateWrapper.Item1)
                .ToHashSet();

            //no need to include start and end as changes in visual state
            path.Remove(startPoint);
            path.Remove(endPoint);

            visualStateWrapper.Item2.Enqueue(new VisualPath(path));

            return visualStateWrapper.Item2;
        }

        public Queue<VisualState> DijkstraVisual()
        {
            var visualStateWrapper = addToVisualStateWrapper();

            HashSet<Vertex<Cell>> path = graph
                .DijkstraPath(startPoint, endPoint, visualStateWrapper.Item1)
                .ToHashSet();

            //no need to include start and end as changes in visual state
            path.Remove(startPoint);
            path.Remove(endPoint);

            visualStateWrapper.Item2.Enqueue(new VisualPath(path));

            return visualStateWrapper.Item2;
        }

        public Queue<VisualState> AStarVisual()
        {
            return AStarVisual(SelectedHeuristic.Manhattan);
        }

        public Queue<VisualState> AStarVisual(SelectedHeuristic selectedHeuristic)

        {
            var visualStateWrapper = addToVisualStateWrapper();

            var heuristic = GetHeurisitic(selectedHeuristic);

            HashSet<Vertex<Cell>> path = graph
                .AStar(startPoint, endPoint, visualStateWrapper.Item1, heuristic)
                .ToHashSet();

            //no need to include start and end as changes in visual state
            path.Remove(startPoint);
            path.Remove(endPoint);

            visualStateWrapper.Item2.Enqueue(new VisualPath(path));

            return visualStateWrapper.Item2;
        }

        private Point ScaleImageCord(Vertex<Cell> vertex)
        {
            return ScaleImageCord(vertex.Value.Position);
        }

        private Point ScaleImageCord(Point point)
        {
            return new Point(
                        (int)(point.X * (float)Settings.GridSize / Settings.ImageSize.Width),
                        (int)(point.Y * (float)Settings.GridSize / Settings.ImageSize.Height)
                       );
        }

        private Func<Vertex<Cell>, Vertex<Cell>, float> GetHeurisitic(SelectedHeuristic heuristic)
        {
            float d = 1.0f;

            switch (heuristic)
            {
                case SelectedHeuristic.Manhattan:

                    return (Vertex<Cell> curr, Vertex<Cell> end) =>
                    {
                        var currCord = ScaleImageCord(curr);
                        var endCord = ScaleImageCord(end);


                        //var currCord = GetCoordinate(cellIndexMap[curr]);
                        //var endCord = GetCoordinate(cellIndexMap[end]);

                        float dx = Math.Abs(currCord.X - endCord.X);
                        float dy = Math.Abs(currCord.Y - endCord.Y);

                        return d * (dx + dy);
                    };

                case SelectedHeuristic.Euclidean:

                    return (Vertex<Cell> curr, Vertex<Cell> end) =>
                    {
                        var currCord = ScaleImageCord(curr);
                        var endCord = ScaleImageCord(end);


                        //var currCord = GetCoordinate(cellIndexMap[curr]);
                        //var endCord = GetCoordinate(cellIndexMap[end]);

                        float dx = Math.Abs(currCord.X - endCord.X);
                        float dy = Math.Abs(currCord.Y - endCord.Y);

                        return d * (float)Math.Sqrt(dx * dx + dy * dy);
                    };
                default:
                    throw new Exception("Invalid Heuristic Selected!");
            }
        }

        private (Action<Vertex<Cell>, HashSet<Vertex<Cell>>>, Queue<VisualState>) addToVisualStateWrapper()
        {
            Queue<VisualState> visualStates = new Queue<VisualState>();

            Action<Vertex<Cell>, HashSet<Vertex<Cell>>> addToVisualState = (curr, toBeVisisted) =>
            {
                //no need to include start in the visual state
                if (curr == startPoint)
                    curr = null;

                //Remove the endPoint if its in the to be visited
                toBeVisisted.Remove(endPoint);

                VisualState temp = new VisualState(curr, toBeVisisted);

                visualStates.Enqueue(temp);
            };

            return (addToVisualState, visualStates);
        }

        private int GetIndex(int x, int y)
        {
            return y * Width + x;
        }

        private (int X, int Y) GetCoordinate(int index)
        {
            int Y = index / Width;
            int X = index % Width;

            return (X, Y);
        }

        private void SetUpCells()
        {
            graph = new Graph<Cell>();
            walls = new HashSet<Cell>();

            //create vertices
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    var temp = new Vertex<Cell>(new Cell(new Point(x * CellSize, y * CellSize), CellSize, Thickness));
                    graph.AddVertex(temp);
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

        private void MouseStateAction(Point mousePos)
        {
            Point cellPoint = ScaleImageCord(mousePos); 

            int index = GetIndex(cellPoint.X, cellPoint.Y);


            if (index < cells.Length)
            {
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
        }

        private void AddEdges(int x, int y)
        {
            int index = GetIndex(x, y);

            //above curr cell
            if (y > 0)
            {
                graph.AddEdge(cells[index], cells[(y - 1) * Width + x], 1);
                graph.AddEdge(cells[(y - 1) * Width + x], cells[index], 1);
            }
            //left curr cell
            if (x > 0)
            {
                graph.AddEdge(cells[index], cells[y * Width + (x - 1)], 1);
                graph.AddEdge(cells[y * Width + (x - 1)], cells[index], 1);
            }
            //right curr cell
            if (x < Width - 1)
            {
                graph.AddEdge(cells[index], cells[y * Width + (x + 1)], 1);
                graph.AddEdge(cells[y * Width + (x + 1)], cells[index], 1);
            }
            //below curr cell
            if (y < Height - 1)
            {
                graph.AddEdge(cells[index], cells[(y + 1) * Width + x], 1);
                graph.AddEdge(cells[(y + 1) * Width + x], cells[index], 1);
            }
        }

        private void RemoveEdges(int x, int y)
        {
            int index = GetIndex(x, y);

            //above curr cell
            if (y > 0)
            {
                graph.RemoveEdge(cells[index], cells[(y - 1) * Width + x]);
                graph.RemoveEdge(cells[(y - 1) * Width + x], cells[index]);
            }
            //left curr cell
            if (x > 0)
            {
                graph.RemoveEdge(cells[index], cells[y * Width + (x - 1)]);
                graph.RemoveEdge(cells[y * Width + (x - 1)], cells[index]);
            }
            //right curr cell
            if (x < Width - 1)
            {
                graph.RemoveEdge(cells[index], cells[y * Width + (x + 1)]);
                graph.RemoveEdge(cells[y * Width + (x + 1)], cells[index]);
            }
            //below curr cell
            if (y < Height - 1)
            {
                graph.RemoveEdge(cells[index], cells[(y + 1) * Width + x]);
                graph.RemoveEdge(cells[(y + 1) * Width + x], cells[index]);
            }
        }

        private void SetPoint(int index, ref Vertex<Cell> point, Color color)
        {
            if (!walls.Contains(cells[index].Value))
            {
                if (point != null)
                {
                    point.Value.FillColor = Color.White;
                }

                point = cells[index];
                point.Value.FillColor = color;
            }
        }

        private void SetStartPoint(int index)
        {
            if (endPoint != cells[index])
            {
                SetPoint(index, ref startPoint, Settings.StartColor);
            }
        }

        private void SetEndPoint(int index)
        {
            if (startPoint != cells[index])
            {
                SetPoint(index, ref endPoint, Settings.EndColor);
            }
        }

        private void SetWall(int index, int x, int y)
        {
            if (startPoint != cells[index] || endPoint != cells[index])
            {
                cells[index].Value.FillColor = Settings.WallColor;

                RemoveEdges(x, y);

                walls.Add(cells[index].Value);
            }
        }

        private void RemoveWall(int index, int x, int y)
        {
            if (startPoint != cells[index] || endPoint != cells[index])
            {
                cells[index].Value.FillColor = Settings.NoneWallColor;

                AddEdges(x, y);

                walls.Remove(cells[index].Value);
            }
        }
    }
}
