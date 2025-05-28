namespace GMR_Pathfinding
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        readonly int GridSize = 15;
        readonly int cellSize = 30;
        readonly int thickness = 5;

        Graphics gfx;
        Bitmap bitmap;

        Grid grid;


        Point mousePos = new Point();

        private void Form1_Load(object sender, EventArgs e)
        {
            bitmap = new Bitmap(GridSize * cellSize + thickness * 2, GridSize * cellSize + thickness * 2);
            gfx = Graphics.FromImage(bitmap);

            grid = new Grid(GridSize, GridSize, cellSize, thickness);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Text = $"X:{mousePos.X}, Y:{mousePos.Y}";

            gfx.Clear(BackColor);

            //update


            //draw
            grid.Draw(gfx);

            pictureBox1.Image = bitmap;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            mousePos.X = e.X; 
            mousePos.Y = e.Y;
        }
    }
}
