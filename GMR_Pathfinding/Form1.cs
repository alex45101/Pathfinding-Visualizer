namespace GMR_Pathfinding
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        bool mouseDown = false;
        bool prevMouseDown = false;
        Point mousePos = new Point();
        Color selectedColor = Color.Black;

        Graphics gfx;
        Bitmap bitmap;

        Grid grid;

        int tempClicks = 0;

        Queue<VisualState> temp = new Queue<VisualState>();

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

            bitmap = new Bitmap(Settings.GridSize * Settings.CellSize + Settings.Thickness * 2, Settings.GridSize * Settings.CellSize + Settings.Thickness * 2);
            gfx = Graphics.FromImage(bitmap);

            grid = new Grid(Settings.GridSize, Settings.GridSize, Settings.CellSize, Settings.Thickness);

            Size = bitmap.Size;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            gfx.Clear(BackColor);

            //update
            this.Text = $"Clicks: {tempClicks} X:{mousePos.X}, Y:{mousePos.Y}, Color: {selectedColor}";

            grid.Update(mouseDown, prevMouseDown, mousePos, selectedColor, bitmap.Size);

            //draw
            grid.Draw(gfx);

            pictureBox1.Image = bitmap;

            if (mousePos.X >= 0 && mousePos.X <= bitmap.Width && mousePos.Y >= 0 && mousePos.Y <= bitmap.Height)
            {
                selectedColor = bitmap.GetPixel(mousePos.X, mousePos.Y);
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                prevMouseDown = mouseDown;
                mouseDown = true;
            }
        }
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                prevMouseDown = mouseDown;
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

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Enter Key
            if (e.KeyChar == '\r')
            {
                //Start Pathfinding
                temp = grid.BreadthFirstVisual();
                visualTimer.Enabled = true;
            }
        }

        private void visualTimer_Tick(object sender, EventArgs e)
        {
            if (temp.Count > 0)
            {
                var thing = temp.Dequeue();
                thing.SetColors();
            }
            else
            {
                visualTimer.Enabled = false;
            }
        }
    }
}
