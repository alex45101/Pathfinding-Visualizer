using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMR_Pathfinding
{
    public class Cell : IDrawable, IComparable<Cell>
    {
        public Point Position { get; set; }
        public int Size { get; set; }
        public int Thickness { get; set; }
        public Color BorderColor { get => BorderColor; set => pen = new Pen(value, Thickness); }
        public Color FillColor
        {
            get => ((SolidBrush)brush).Color;
            set
            {
                brush = new SolidBrush(value);
            }
        }

        private Pen pen;
        private Brush brush;

        public Cell(Point position, int size, int thickness)
        {
            Position = position;
            Size = size;
            Thickness = thickness;

            pen = new Pen(Color.Black, thickness);
            brush = Brushes.White;
        }

        public void Draw(Graphics gfx)
        {
            gfx.FillRectangle(brush, Position.X + Thickness, Position.Y + Thickness, Size, Size);
            gfx.DrawRectangle(pen, Position.X + Thickness, Position.Y + Thickness, Size, Size);
        }

        public int CompareTo(Cell? other)
        {
            Point temp = other == null ? new Point(0, 0) : other.Position;

            if (Position.X == temp.X && Position.Y == temp.Y)
            {
                return 0;
            }
            else if (Position.X > temp.X) // current X bigger then other
            {
                if (Position.Y >= temp.Y) // bigger Y means bigger
                {
                    return 1;
                }
                else // smaller Y means smaller
                {
                    return -1;
                }
            }
            else
            {
                if (Position.Y >= temp.Y) // bigger Y means bigger
                {
                    return 1;
                }
                else // smaller Y means smaller
                {
                    return -1;
                }
            }
        }
    }
}
