namespace GMR_Pathfinding
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            pictureBox1 = new PictureBox();
            timer1 = new System.Windows.Forms.Timer(components);
            visualTimer = new System.Windows.Forms.Timer(components);
            selectedAlgoComboBox = new ComboBox();
            startButton = new Button();
            speedTrackBar = new TrackBar();
            pauseButton = new Button();
            rightButton = new Button();
            leftButton = new Button();
            label1 = new Label();
            clearButton = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)speedTrackBar).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            pictureBox1.Location = new Point(12, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(1678, 1080);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            pictureBox1.MouseDown += pictureBox1_MouseDown;
            pictureBox1.MouseMove += pictureBox1_MouseMove;
            pictureBox1.MouseUp += pictureBox1_MouseUp;
            // 
            // timer1
            // 
            timer1.Enabled = true;
            timer1.Interval = 75;
            timer1.Tick += timer1_Tick;
            // 
            // visualTimer
            // 
            visualTimer.Tick += visualTimer_Tick;
            // 
            // selectedAlgoComboBox
            // 
            selectedAlgoComboBox.FormattingEnabled = true;
            selectedAlgoComboBox.Items.AddRange(new object[] { "Breadth First", "Depth First", "Dijkstra", "A*" });
            selectedAlgoComboBox.Location = new Point(23, 1105);
            selectedAlgoComboBox.Name = "selectedAlgoComboBox";
            selectedAlgoComboBox.Size = new Size(335, 40);
            selectedAlgoComboBox.TabIndex = 1;
            selectedAlgoComboBox.SelectedIndexChanged += selectedAlgoComboBox_SelectedIndexChanged;
            // 
            // startButton
            // 
            startButton.Location = new Point(1558, 1098);
            startButton.Name = "startButton";
            startButton.Size = new Size(127, 46);
            startButton.TabIndex = 2;
            startButton.Text = "Start";
            startButton.UseVisualStyleBackColor = true;
            startButton.Click += startButton_Click;
            // 
            // speedTrackBar
            // 
            speedTrackBar.Location = new Point(364, 1105);
            speedTrackBar.Maximum = 50;
            speedTrackBar.Minimum = 1;
            speedTrackBar.Name = "speedTrackBar";
            speedTrackBar.Size = new Size(703, 90);
            speedTrackBar.TabIndex = 3;
            speedTrackBar.Value = 1;
            speedTrackBar.Scroll += speedTrackBar_Scroll;
            // 
            // pauseButton
            // 
            pauseButton.Location = new Point(1425, 1098);
            pauseButton.Name = "pauseButton";
            pauseButton.Size = new Size(127, 46);
            pauseButton.TabIndex = 4;
            pauseButton.Text = "Pause";
            pauseButton.UseVisualStyleBackColor = true;
            pauseButton.Click += pauseButton_Click;
            // 
            // rightButton
            // 
            rightButton.Location = new Point(1240, 1098);
            rightButton.Name = "rightButton";
            rightButton.Size = new Size(46, 46);
            rightButton.TabIndex = 5;
            rightButton.Text = ">";
            rightButton.UseVisualStyleBackColor = true;
            rightButton.Click += rightButton_Click;
            // 
            // leftButton
            // 
            leftButton.Location = new Point(1073, 1098);
            leftButton.Name = "leftButton";
            leftButton.Size = new Size(43, 46);
            leftButton.TabIndex = 6;
            leftButton.Text = "<";
            leftButton.UseVisualStyleBackColor = true;
            leftButton.Click += leftButton_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(1140, 1105);
            label1.Name = "label1";
            label1.Size = new Size(78, 32);
            label1.TabIndex = 7;
            label1.Text = "label1";
            // 
            // clearButton
            // 
            clearButton.Location = new Point(1292, 1098);
            clearButton.Name = "clearButton";
            clearButton.Size = new Size(127, 46);
            clearButton.TabIndex = 8;
            clearButton.Text = "Reset";
            clearButton.UseVisualStyleBackColor = true;
            clearButton.Click += clearButton_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1697, 1156);
            Controls.Add(clearButton);
            Controls.Add(label1);
            Controls.Add(leftButton);
            Controls.Add(rightButton);
            Controls.Add(pauseButton);
            Controls.Add(speedTrackBar);
            Controls.Add(startButton);
            Controls.Add(selectedAlgoComboBox);
            Controls.Add(pictureBox1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)speedTrackBar).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer visualTimer;
        private ComboBox selectedAlgoComboBox;
        private Button startButton;
        private TrackBar speedTrackBar;
        private Button pauseButton;
        private Button rightButton;
        private Button leftButton;
        private Label label1;
        private Button clearButton;
    }
}
