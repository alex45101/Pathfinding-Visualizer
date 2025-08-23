using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMR_Pathfinding
{
    public class Cell : IDrawable, IEqualityComparer<Cell>
    {
        public Point Position { get; set; }
        public int Size { get; set; }
        public int Thickness { get; set; }
        public Color BorderColor 
        { 
            get => pen.Color; 
            set => pen = new Pen(value, Thickness); 
        }
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

        public bool Equals(Cell? x, Cell? y)
        {
            if(x == null)
                throw new ArgumentNullException(nameof(x));

            if (y == null)
                throw new ArgumentNullException(nameof(y));

            return x.Equals(y);
        }

        public int GetHashCode([DisallowNull] Cell obj)
        {
            return obj.GetHashCode();
        }
    }
}
