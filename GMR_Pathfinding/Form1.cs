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

        SelectedAlgo selectedAlgo = SelectedAlgo.None;

        Graphics gfx;
        Bitmap bitmap;

        Grid grid;

        int tempClicks = 0;

        VisualState[] visualStates = new VisualState[0];
        int currentVisualState = 0;

        private void Form1_Load(object sender, EventArgs e)
        {
            //set combo box
            selectedAlgoComboBox.Items.Clear();

            int tempLength = Enum.GetValues(typeof(SelectedAlgo)).Length;

            for (int i = 1; i < tempLength; i++)
            {
                selectedAlgoComboBox.Items.Add(((SelectedAlgo)i).ToString());
            }

            //set picturebox and graphics
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

            bitmap = new Bitmap(Settings.GridSize * Settings.CellSize + Settings.Thickness * 2, Settings.GridSize * Settings.CellSize + Settings.Thickness * 2);
            gfx = Graphics.FromImage(bitmap);

            grid = new Grid(Settings.GridSize, Settings.GridSize, Settings.CellSize, Settings.Thickness);
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

        private void visualTimer_Tick(object sender, EventArgs e)
        {
            if (currentVisualState < visualStates.Length)
            {
                var thing = visualStates[currentVisualState];
                thing.SetColors();

                UpdateVisualState(1);
            }
            else
            {
                visualTimer.Enabled = false;
            }
        }

        private void speedTrackBar_Scroll(object sender, EventArgs e)
        {
            visualTimer.Interval = Settings.VisualTimerDefault / speedTrackBar.Value;
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            //Start Pathfinding
            switch (selectedAlgo)
            {
                case SelectedAlgo.None:
                    break;
                case SelectedAlgo.BreathFirst:
                    visualStates = grid.BreadthFirstVisual().ToArray();
                    break;
                case SelectedAlgo.DepthFirst:
                    visualStates = grid.DepthFirstVisual().ToArray();
                    break;
                case SelectedAlgo.Dijkstra:
                    break;
                case SelectedAlgo.A:
                    break;
                default:
                    break;
            }

            if (selectedAlgo > SelectedAlgo.None && selectedAlgo <= SelectedAlgo.A)
            {
                visualTimer.Enabled = true;
            }
        }

        private void selectedAlgoComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedAlgo = (SelectedAlgo)((ComboBox)sender).SelectedIndex + 1;
        }

        private void pauseButton_Click(object sender, EventArgs e)
        {
            visualTimer.Enabled = false;
        }

        private void rightButton_Click(object sender, EventArgs e)
        {
            var thing = visualStates[currentVisualState];
            thing.SetColors();

            UpdateVisualState(1);
        }

        private void leftButton_Click(object sender, EventArgs e)
        {
            UpdateVisualState(-1);

            var thing = visualStates[currentVisualState];
            thing.ResetColors();
        }

        private void UpdateVisualState(int increment)
        {
            currentVisualState += increment;

            if (currentVisualState >= 0 && currentVisualState <= visualStates.Length)
            {
                label1.Text = $"{currentVisualState} / {visualStates.Length}";
            }
            else
            {
                currentVisualState -= increment;
            }
        }
    }
}
