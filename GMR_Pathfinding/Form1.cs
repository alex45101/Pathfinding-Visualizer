namespace GMR_Pathfinding
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static readonly int GridSize = 15;
        readonly int cellSize = 30;
        readonly int thickness = 3;

        bool mouseDown = false;
        Point mousePos = new Point();
        Color selectedColor = Color.Black;

        Graphics gfx;
        Bitmap bitmap;

        Grid grid;

        int tempClicks = 0;

        private void Form1_Load(object sender, EventArgs e)
        {
            bitmap = new Bitmap(GridSize * cellSize + thickness * 2, GridSize * cellSize + thickness * 2);
            gfx = Graphics.FromImage(bitmap);

            grid = new Grid(GridSize, GridSize, cellSize, thickness);

            Size = bitmap.Size;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            gfx.Clear(BackColor);

            //update
            this.Text = $"Clicks: {tempClicks} X:{mousePos.X}, Y:{mousePos.Y}, Color: {selectedColor}";

            grid.Update(mouseDown, mousePos, selectedColor, bitmap.Size);

            //draw
            grid.Draw(gfx);

            pictureBox1.Image = bitmap;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

            if (mouseDown)
            {
                selectedColor = bitmap.GetPixel(mousePos.X, mousePos.Y);
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //TODO:no work when the mouse leaves the form
                if (mousePos.X > 0 && mousePos.X < bitmap.Size.Width && mousePos.Y > 0 && mousePos.Y < bitmap.Size.Height)
                {
                    mouseDown = true;
                }
                else
                {
                    mouseDown = false;
                }
            }
        }
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseDown = false;
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            mousePos.X = (int)(e.X * ((float)bitmap.Size.Width / pictureBox1.Size.Width));
            mousePos.Y = (int)(e.Y * ((float)bitmap.Size.Height / pictureBox1.Size.Height));
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            
        }
    }
}
