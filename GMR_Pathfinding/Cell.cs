using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMR_Pathfinding
{
    public class Cell : IDrawable
    {
        public Point Position { get; set; }
        public int Size { get; set; }
        public int Thickness { get; set; }
        public Color BorderColor { get => BorderColor; set => pen = new Pen(value, Thickness); } 
        public Color FillColor { get => FillColor; set => brush = new SolidBrush(value); }
        
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
    }
}
