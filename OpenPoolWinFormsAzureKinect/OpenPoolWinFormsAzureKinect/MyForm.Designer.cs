namespace OpenPoolWinFormsAzureKinect
{
    partial class MyForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MyForm));
            pictureBoxKinect0 = new PictureBox();
            pictureBoxCombined = new PictureBox();
            buttonReset = new Button();
            trackBarThreshNear = new TrackBar();
            label1 = new Label();
            label2 = new Label();
            trackBarThreshFar = new TrackBar();
            label3 = new Label();
            trackBarBlobMin = new TrackBar();
            label4 = new Label();
            trackBarBlobMax = new TrackBar();
            label5 = new Label();
            trackBarAntiNoise = new TrackBar();
            labelBinThreshNearValue = new Label();
            labelBinThreshFarValue = new Label();
            labelBlobSizeMinValue = new Label();
            labelBlobSizeMaxValue = new Label();
            labelAntiNoiseValue = new Label();
            menuStripMainMenu = new MenuStrip();
            fileFToolStripMenuItem = new ToolStripMenuItem();
            exitEToolStripMenuItem = new ToolStripMenuItem();
            helpHToolStripMenuItem = new ToolStripMenuItem();
            aboutAToolStripMenuItem = new ToolStripMenuItem();
            Ad = new Label();
            label6 = new Label();
            labelTargetIp = new Label();
            label8 = new Label();
            labelTargetPort = new Label();
            trackBarBallSmoothing = new TrackBar();
            labelBallSmoothing = new Label();
            labelBallSmoothingValue = new Label();
            trackBarPreviousBallDistance = new TrackBar();
            labelPreviousBallDistance = new Label();
            labelPreviousBallDistanceValue = new Label();
            buttonUndo = new Button();
            labelFPS = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBoxKinect0).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxCombined).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarThreshNear).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarThreshFar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarBlobMin).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarBlobMax).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarAntiNoise).BeginInit();
            menuStripMainMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarBallSmoothing).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarPreviousBallDistance).BeginInit();
            SuspendLayout();
            // 
            // pictureBoxKinect0
            // 
            pictureBoxKinect0.BackColor = Color.Gray;
            pictureBoxKinect0.BackgroundImage = (Image)resources.GetObject("pictureBoxKinect0.BackgroundImage");
            pictureBoxKinect0.BackgroundImageLayout = ImageLayout.Zoom;
            pictureBoxKinect0.Location = new Point(14, 33);
            pictureBoxKinect0.Margin = new Padding(4, 3, 4, 3);
            pictureBoxKinect0.Name = "pictureBoxKinect0";
            pictureBoxKinect0.Size = new Size(299, 265);
            pictureBoxKinect0.TabIndex = 0;
            pictureBoxKinect0.TabStop = false;
            // 
            // pictureBoxCombined
            // 
            pictureBoxCombined.BackColor = Color.Gray;
            pictureBoxCombined.BackgroundImage = (Image)resources.GetObject("pictureBoxCombined.BackgroundImage");
            pictureBoxCombined.BackgroundImageLayout = ImageLayout.Center;
            pictureBoxCombined.Location = new Point(14, 303);
            pictureBoxCombined.Margin = new Padding(4, 3, 4, 3);
            pictureBoxCombined.Name = "pictureBoxCombined";
            pictureBoxCombined.Size = new Size(597, 265);
            pictureBoxCombined.TabIndex = 3;
            pictureBoxCombined.TabStop = false;
            // 
            // buttonReset
            // 
            buttonReset.Location = new Point(320, 33);
            buttonReset.Margin = new Padding(4, 3, 4, 3);
            buttonReset.Name = "buttonReset";
            buttonReset.Size = new Size(292, 135);
            buttonReset.TabIndex = 4;
            buttonReset.Text = "Reset All the Ignore Rects";
            buttonReset.UseVisualStyleBackColor = true;
            buttonReset.Click += buttonReset_Click;
            // 
            // trackBarThreshNear
            // 
            trackBarThreshNear.Location = new Point(630, 51);
            trackBarThreshNear.Margin = new Padding(4, 3, 4, 3);
            trackBarThreshNear.Maximum = 255;
            trackBarThreshNear.Name = "trackBarThreshNear";
            trackBarThreshNear.Size = new Size(510, 45);
            trackBarThreshNear.TabIndex = 5;
            trackBarThreshNear.Scroll += NearThresholdTrackbar_Scroll;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(628, 32);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(94, 15);
            label1.TabIndex = 6;
            label1.Text = "Bin thresh (near)";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(628, 92);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(87, 15);
            label2.TabIndex = 8;
            label2.Text = "Bin Thresh (far)";
            // 
            // trackBarThreshFar
            // 
            trackBarThreshFar.Location = new Point(630, 111);
            trackBarThreshFar.Margin = new Padding(4, 3, 4, 3);
            trackBarThreshFar.Maximum = 255;
            trackBarThreshFar.Name = "trackBarThreshFar";
            trackBarThreshFar.Size = new Size(510, 45);
            trackBarThreshFar.TabIndex = 7;
            trackBarThreshFar.Scroll += trackBarThreshFar_Scroll;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(628, 152);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(85, 15);
            label3.TabIndex = 10;
            label3.Text = "Blob size (min)";
            // 
            // trackBarBlobMin
            // 
            trackBarBlobMin.Location = new Point(630, 171);
            trackBarBlobMin.Margin = new Padding(4, 3, 4, 3);
            trackBarBlobMin.Maximum = 100;
            trackBarBlobMin.Name = "trackBarBlobMin";
            trackBarBlobMin.Size = new Size(510, 45);
            trackBarBlobMin.TabIndex = 9;
            trackBarBlobMin.Scroll += trackBarBlobMin_Scroll;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(628, 212);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(87, 15);
            label4.TabIndex = 12;
            label4.Text = "Blob size (max)";
            // 
            // trackBarBlobMax
            // 
            trackBarBlobMax.Location = new Point(630, 231);
            trackBarBlobMax.Margin = new Padding(4, 3, 4, 3);
            trackBarBlobMax.Maximum = 200;
            trackBarBlobMax.Name = "trackBarBlobMax";
            trackBarBlobMax.Size = new Size(510, 45);
            trackBarBlobMax.TabIndex = 11;
            trackBarBlobMax.Scroll += trackBarBlobMax_Scroll;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(628, 272);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(60, 15);
            label5.TabIndex = 14;
            label5.Text = "Anti noise";
            // 
            // trackBarAntiNoise
            // 
            trackBarAntiNoise.LargeChange = 2;
            trackBarAntiNoise.Location = new Point(630, 291);
            trackBarAntiNoise.Margin = new Padding(4, 3, 4, 3);
            trackBarAntiNoise.Maximum = 20;
            trackBarAntiNoise.Name = "trackBarAntiNoise";
            trackBarAntiNoise.Size = new Size(510, 45);
            trackBarAntiNoise.TabIndex = 13;
            trackBarAntiNoise.Scroll += trackBarAntiNoise_Scroll;
            // 
            // labelBinThreshNearValue
            // 
            labelBinThreshNearValue.AutoSize = true;
            labelBinThreshNearValue.Location = new Point(864, 32);
            labelBinThreshNearValue.Margin = new Padding(4, 0, 4, 0);
            labelBinThreshNearValue.Name = "labelBinThreshNearValue";
            labelBinThreshNearValue.Size = new Size(13, 15);
            labelBinThreshNearValue.TabIndex = 15;
            labelBinThreshNearValue.Text = "0";
            // 
            // labelBinThreshFarValue
            // 
            labelBinThreshFarValue.AutoSize = true;
            labelBinThreshFarValue.Location = new Point(864, 92);
            labelBinThreshFarValue.Margin = new Padding(4, 0, 4, 0);
            labelBinThreshFarValue.Name = "labelBinThreshFarValue";
            labelBinThreshFarValue.Size = new Size(13, 15);
            labelBinThreshFarValue.TabIndex = 16;
            labelBinThreshFarValue.Text = "0";
            // 
            // labelBlobSizeMinValue
            // 
            labelBlobSizeMinValue.AutoSize = true;
            labelBlobSizeMinValue.Location = new Point(864, 152);
            labelBlobSizeMinValue.Margin = new Padding(4, 0, 4, 0);
            labelBlobSizeMinValue.Name = "labelBlobSizeMinValue";
            labelBlobSizeMinValue.Size = new Size(13, 15);
            labelBlobSizeMinValue.TabIndex = 17;
            labelBlobSizeMinValue.Text = "0";
            // 
            // labelBlobSizeMaxValue
            // 
            labelBlobSizeMaxValue.AutoSize = true;
            labelBlobSizeMaxValue.Location = new Point(864, 212);
            labelBlobSizeMaxValue.Margin = new Padding(4, 0, 4, 0);
            labelBlobSizeMaxValue.Name = "labelBlobSizeMaxValue";
            labelBlobSizeMaxValue.Size = new Size(13, 15);
            labelBlobSizeMaxValue.TabIndex = 18;
            labelBlobSizeMaxValue.Text = "0";
            // 
            // labelAntiNoiseValue
            // 
            labelAntiNoiseValue.AutoSize = true;
            labelAntiNoiseValue.Location = new Point(864, 272);
            labelAntiNoiseValue.Margin = new Padding(4, 0, 4, 0);
            labelAntiNoiseValue.Name = "labelAntiNoiseValue";
            labelAntiNoiseValue.Size = new Size(13, 15);
            labelAntiNoiseValue.TabIndex = 19;
            labelAntiNoiseValue.Text = "0";
            // 
            // menuStripMainMenu
            // 
            menuStripMainMenu.BackgroundImageLayout = ImageLayout.None;
            menuStripMainMenu.Items.AddRange(new ToolStripItem[] { fileFToolStripMenuItem, helpHToolStripMenuItem });
            menuStripMainMenu.Location = new Point(0, 0);
            menuStripMainMenu.Name = "menuStripMainMenu";
            menuStripMainMenu.Padding = new Padding(7, 2, 0, 2);
            menuStripMainMenu.Size = new Size(1155, 24);
            menuStripMainMenu.TabIndex = 20;
            menuStripMainMenu.Text = "menuStripMainMenu";
            // 
            // fileFToolStripMenuItem
            // 
            fileFToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { exitEToolStripMenuItem });
            fileFToolStripMenuItem.Name = "fileFToolStripMenuItem";
            fileFToolStripMenuItem.Size = new Size(51, 20);
            fileFToolStripMenuItem.Text = "File(&F)";
            // 
            // exitEToolStripMenuItem
            // 
            exitEToolStripMenuItem.Name = "exitEToolStripMenuItem";
            exitEToolStripMenuItem.Size = new Size(108, 22);
            exitEToolStripMenuItem.Text = "Exit(&X)";
            exitEToolStripMenuItem.Click += exitEToolStripMenuItem_Click;
            // 
            // helpHToolStripMenuItem
            // 
            helpHToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { aboutAToolStripMenuItem });
            helpHToolStripMenuItem.Name = "helpHToolStripMenuItem";
            helpHToolStripMenuItem.Size = new Size(61, 20);
            helpHToolStripMenuItem.Text = "Help(&H)";
            // 
            // aboutAToolStripMenuItem
            // 
            aboutAToolStripMenuItem.Name = "aboutAToolStripMenuItem";
            aboutAToolStripMenuItem.Size = new Size(123, 22);
            aboutAToolStripMenuItem.Text = "About(&A)";
            aboutAToolStripMenuItem.Click += aboutAToolStripMenuItem_Click;
            // 
            // Ad
            // 
            Ad.AutoSize = true;
            Ad.Font = new Font("MS UI Gothic", 27.75F, FontStyle.Regular, GraphicsUnit.Point);
            Ad.Location = new Point(757, 523);
            Ad.Margin = new Padding(4, 0, 4, 0);
            Ad.Name = "Ad";
            Ad.Size = new Size(334, 37);
            Ad.TabIndex = 21;
            Ad.Text = "Beta for New Kinect";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(630, 480);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(51, 15);
            label6.TabIndex = 22;
            label6.Text = "target IP";
            // 
            // labelTargetIp
            // 
            labelTargetIp.AutoSize = true;
            labelTargetIp.Location = new Point(729, 480);
            labelTargetIp.Margin = new Padding(4, 0, 4, 0);
            labelTargetIp.Name = "labelTargetIp";
            labelTargetIp.Size = new Size(22, 15);
            labelTargetIp.TabIndex = 23;
            labelTargetIp.Text = "---";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(630, 500);
            label8.Margin = new Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new Size(63, 15);
            label8.TabIndex = 24;
            label8.Text = "target Port";
            // 
            // labelTargetPort
            // 
            labelTargetPort.AutoSize = true;
            labelTargetPort.Location = new Point(729, 500);
            labelTargetPort.Margin = new Padding(4, 0, 4, 0);
            labelTargetPort.Name = "labelTargetPort";
            labelTargetPort.Size = new Size(22, 15);
            labelTargetPort.TabIndex = 25;
            labelTargetPort.Text = "---";
            // 
            // trackBarBallSmoothing
            // 
            trackBarBallSmoothing.LargeChange = 1;
            trackBarBallSmoothing.Location = new Point(630, 357);
            trackBarBallSmoothing.Margin = new Padding(4, 3, 4, 3);
            trackBarBallSmoothing.Maximum = 20;
            trackBarBallSmoothing.Name = "trackBarBallSmoothing";
            trackBarBallSmoothing.Size = new Size(510, 45);
            trackBarBallSmoothing.TabIndex = 26;
            trackBarBallSmoothing.Scroll += trackBarBallSmoothing_Scroll;
            // 
            // labelBallSmoothing
            // 
            labelBallSmoothing.AutoSize = true;
            labelBallSmoothing.Location = new Point(630, 333);
            labelBallSmoothing.Margin = new Padding(4, 0, 4, 0);
            labelBallSmoothing.Name = "labelBallSmoothing";
            labelBallSmoothing.Size = new Size(88, 15);
            labelBallSmoothing.TabIndex = 27;
            labelBallSmoothing.Text = "Ball Smoothing";
            // 
            // labelBallSmoothingValue
            // 
            labelBallSmoothingValue.AutoSize = true;
            labelBallSmoothingValue.Location = new Point(864, 333);
            labelBallSmoothingValue.Margin = new Padding(4, 0, 4, 0);
            labelBallSmoothingValue.Name = "labelBallSmoothingValue";
            labelBallSmoothingValue.Size = new Size(13, 15);
            labelBallSmoothingValue.TabIndex = 28;
            labelBallSmoothingValue.Text = "0";
            // 
            // trackBarPreviousBallDistance
            // 
            trackBarPreviousBallDistance.Location = new Point(628, 420);
            trackBarPreviousBallDistance.Margin = new Padding(4, 3, 4, 3);
            trackBarPreviousBallDistance.Maximum = 100;
            trackBarPreviousBallDistance.Name = "trackBarPreviousBallDistance";
            trackBarPreviousBallDistance.Size = new Size(512, 45);
            trackBarPreviousBallDistance.TabIndex = 29;
            trackBarPreviousBallDistance.Scroll += trackBarPreviousBallDistance_Scroll;
            // 
            // labelPreviousBallDistance
            // 
            labelPreviousBallDistance.AutoSize = true;
            labelPreviousBallDistance.Location = new Point(630, 397);
            labelPreviousBallDistance.Margin = new Padding(4, 0, 4, 0);
            labelPreviousBallDistance.Name = "labelPreviousBallDistance";
            labelPreviousBallDistance.Size = new Size(195, 15);
            labelPreviousBallDistance.TabIndex = 30;
            labelPreviousBallDistance.Text = "Threshold for Previous Ball Distance";
            // 
            // labelPreviousBallDistanceValue
            // 
            labelPreviousBallDistanceValue.AutoSize = true;
            labelPreviousBallDistanceValue.Location = new Point(864, 396);
            labelPreviousBallDistanceValue.Margin = new Padding(4, 0, 4, 0);
            labelPreviousBallDistanceValue.Name = "labelPreviousBallDistanceValue";
            labelPreviousBallDistanceValue.Size = new Size(13, 15);
            labelPreviousBallDistanceValue.TabIndex = 31;
            labelPreviousBallDistanceValue.Text = "0";
            // 
            // buttonUndo
            // 
            buttonUndo.Location = new Point(320, 177);
            buttonUndo.Margin = new Padding(4, 3, 4, 3);
            buttonUndo.Name = "buttonUndo";
            buttonUndo.Size = new Size(292, 122);
            buttonUndo.TabIndex = 32;
            buttonUndo.Text = "Undo Previous Ignore Rect";
            buttonUndo.UseVisualStyleBackColor = true;
            // 
            // labelFPS
            // 
            labelFPS.AutoSize = true;
            labelFPS.Location = new Point(632, 520);
            labelFPS.Margin = new Padding(4, 0, 4, 0);
            labelFPS.Name = "labelFPS";
            labelFPS.Size = new Size(26, 15);
            labelFPS.TabIndex = 33;
            labelFPS.Text = "FPS";
            // 
            // MyForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1155, 583);
            Controls.Add(labelFPS);
            Controls.Add(buttonUndo);
            Controls.Add(labelPreviousBallDistanceValue);
            Controls.Add(labelPreviousBallDistance);
            Controls.Add(trackBarPreviousBallDistance);
            Controls.Add(labelBallSmoothingValue);
            Controls.Add(labelBallSmoothing);
            Controls.Add(trackBarBallSmoothing);
            Controls.Add(labelTargetPort);
            Controls.Add(label8);
            Controls.Add(labelTargetIp);
            Controls.Add(label6);
            Controls.Add(Ad);
            Controls.Add(labelAntiNoiseValue);
            Controls.Add(labelBlobSizeMaxValue);
            Controls.Add(labelBlobSizeMinValue);
            Controls.Add(labelBinThreshFarValue);
            Controls.Add(labelBinThreshNearValue);
            Controls.Add(label5);
            Controls.Add(trackBarAntiNoise);
            Controls.Add(label4);
            Controls.Add(trackBarBlobMax);
            Controls.Add(label3);
            Controls.Add(trackBarBlobMin);
            Controls.Add(label2);
            Controls.Add(trackBarThreshFar);
            Controls.Add(label1);
            Controls.Add(trackBarThreshNear);
            Controls.Add(buttonReset);
            Controls.Add(pictureBoxCombined);
            Controls.Add(pictureBoxKinect0);
            Controls.Add(menuStripMainMenu);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStripMainMenu;
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            Name = "MyForm";
            Text = "OpenPoolCoreGUI Ver.0.10";
            Load += MyForm_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBoxKinect0).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxCombined).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarThreshNear).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarThreshFar).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarBlobMin).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarBlobMax).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarAntiNoise).EndInit();
            menuStripMainMenu.ResumeLayout(false);
            menuStripMainMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarBallSmoothing).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarPreviousBallDistance).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBoxKinect0;
        private Button buttonReset;
        private Button buttonUndo;
        private MenuStrip menuStripMainMenu;
        private ToolStripMenuItem fileFToolStripMenuItem;
        private ToolStripMenuItem exitEToolStripMenuItem;
        private ToolStripMenuItem helpHToolStripMenuItem;
        private ToolStripMenuItem aboutAToolStripMenuItem;
        private PictureBox pictureBoxCombined;
        private TrackBar trackBarThreshNear;
        private TrackBar trackBarThreshFar;
        private TrackBar trackBarBlobMin;
        private TrackBar trackBarBlobMax;
        private TrackBar trackBarAntiNoise;
        private TrackBar trackBarBallSmoothing;
        private TrackBar trackBarPreviousBallDistance;
        private Label label1;
        private Label labelBinThreshNearValue;
        private Label label2;
        private Label labelBinThreshFarValue;
        private Label label3;
        private Label labelBlobSizeMinValue;
        private Label label4;
        private Label labelBlobSizeMaxValue;
        private Label label5;
        private Label labelAntiNoiseValue;
        private Label labelBallSmoothing;
        private Label labelBallSmoothingValue;
        private Label labelPreviousBallDistance;
        private Label labelPreviousBallDistanceValue;
        private Label label6;
        private Label label8;
        private Label labelFPS;
        private Label Ad;
        public Label labelTargetPort;
        public Label labelTargetIp;
    }
}