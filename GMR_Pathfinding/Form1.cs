namespace GMR_Pathfinding
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        readonly int GridSize = 15;

        Graphics gfx;
        Bitmap bitmap;

        Grid grid;

        private void Form1_Load(object sender, EventArgs e)
        {
            bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            gfx = Graphics.FromImage(bitmap);

            grid = new Grid(GridSize, GridSize, 30);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            gfx.Clear(BackColor);

            grid.Draw(gfx);

            pictureBox1.Image = bitmap;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            
        }
    }
}
