using OpenCvSharp;

namespace OpenPoolWinFormsAzureKinect
{
    public partial class MyForm : Form
    {
        public ManualResetEvent TargetSetEvent = new ManualResetEvent(false);

        public int ThreshNearValue { get; private set; }
        public int ThreshFarValue { get; private set; }
        public int BlobMinValue { get; private set; }
        public int BlobMaxValue { get; private set; }
        public int AntiNoiseValue { get; private set; }
        public int BallSmoothingValue { get; private set; }
        public int PreviousBallDistanceThresholdValue { get; private set; }

        public int rect0x { get; private set; }
        public int rect0y { get; private set; }
        public int rect0width { get; private set; }
        public int rect0height { get; private set; }

        private int rectcx;
        private int rectcy;
        private int rectcwidth;
        private int rectcheight;

        private List<Rect> ignoreRects = new List<Rect>();
        public List<Rect> GetIgnoreRects => ignoreRects;

        private Thread kinectManagerThread;

        private delegate void delegateUpdateKinectImage(Bitmap bitmap);
        private delegate void delegateUpdateFPSLabel(string fps);
        private delegate void UpdateKinectImageDelegate(Bitmap bmp);

        public MyForm()
        {
            ThreshNearValue = 0;
            ThreshFarValue = 0;
            BlobMinValue = 0;
            BlobMaxValue = 0;
            AntiNoiseValue = 0;
            BallSmoothingValue = 0;
            PreviousBallDistanceThresholdValue = 0;

            rect0x = 0;
            rect0y = 0;
            rect0width = 100;
            rect0height = 100;

            rectcx = 0;
            rectcy = 0;
            rectcwidth = 0;
            rectcheight = 0;

            ignoreRects = new();

            InitializeComponent();

            try
            {
                string configFilePath = Path.Combine(Directory.GetCurrentDirectory(), "config.xml");
                XmlConfig xml = new XmlConfig(configFilePath);
                AppConfig conf = xml.Load();

                ThreshNearValue = conf.ThreshNearValue;
                trackBarThreshNear.Value = conf.ThreshNearValue;
                labelBinThreshNearValue.Text = conf.ThreshNearValue.ToString();

                ThreshFarValue = conf.ThreshFarValue;
                trackBarThreshFar.Value = conf.ThreshFarValue;
                labelBinThreshFarValue.Text = conf.ThreshFarValue.ToString();

                BlobMinValue = conf.BlobMinValue;
                trackBarBlobMin.Value = conf.BlobMinValue;
                labelBlobSizeMinValue.Text = conf.BlobMinValue.ToString();

                BlobMaxValue = conf.BlobMaxValue;
                trackBarBlobMax.Value = conf.BlobMaxValue;
                labelBlobSizeMaxValue.Text = conf.BlobMaxValue.ToString();

                AntiNoiseValue = conf.AntiNoiseValue;
                trackBarAntiNoise.Value = conf.AntiNoiseValue;
                labelAntiNoiseValue.Text = conf.AntiNoiseValue.ToString();

                BallSmoothingValue = conf.BallSmoothingValue;
                trackBarBallSmoothing.Value = conf.BallSmoothingValue;
                labelBallSmoothingValue.Text = conf.BallSmoothingValue.ToString();

                PreviousBallDistanceThresholdValue = conf.PreviousBallDistanceThresholdValue;
                trackBarPreviousBallDistance.Value = conf.PreviousBallDistanceThresholdValue;
                labelPreviousBallDistanceValue.Text = conf.PreviousBallDistanceThresholdValue.ToString();

                rect0x = conf.rect0x;
                rect0y = conf.rect0y;

                if (rect0width == 0)
                {
                    rect0width = 100;
                }
                else
                {
                    rect0width = conf.rect0width;
                }

                if (rect0height == 0)
                {
                    rect0height = 100;
                }
                else
                {
                    rect0height = conf.rect0height;
                }
            }
            catch (FileNotFoundException e)
            {
                // Handle file not found exception
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            XmlConfig xml = new XmlConfig(Path.Combine(Directory.GetCurrentDirectory(), "config.xml"));
            AppConfig conf = new AppConfig();

            conf.ThreshNearValue = ThreshNearValue;
            conf.ThreshFarValue = ThreshFarValue;
            conf.BlobMinValue = BlobMinValue;
            conf.BlobMaxValue = BlobMaxValue;
            conf.AntiNoiseValue = AntiNoiseValue;
            conf.BallSmoothingValue = BallSmoothingValue;
            conf.PreviousBallDistanceThresholdValue = PreviousBallDistanceThresholdValue;

            conf.rect0x = rect0x;
            conf.rect0y = rect0y;
            conf.rect0width = rect0width;
            conf.rect0height = rect0height;

            xml.Save(conf);

            ignoreRects.Clear();

            base.OnFormClosing(e);
        }


        private void buttonReset_Click(object sender, EventArgs e)
        {

        }

        private void MyForm_Load(object sender, EventArgs e)
        {
            var targetSetup = new TargetSetup();
            targetSetup.ShowDialog(this);

            TargetSetEvent.Set();
        }

        private void exitEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // TODO: killKinectThread
            Close();
        }

        private void aboutAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var helpForm = new HelpForm();
            helpForm.Show();
        }

        public void SetKinectManagerThread(Thread thread)
        {
            kinectManagerThread = thread;
        }

        private void NearThresholdTrackbar_Scroll(object sender, EventArgs e)
        {
            labelBinThreshNearValue.Text = trackBarThreshNear.Value.ToString();
            ThreshNearValue = trackBarThreshNear.Value;
        }

        private void trackBarThreshFar_Scroll(object sender, EventArgs e)
        {
            labelBinThreshFarValue.Text = trackBarThreshFar.Value.ToString();
            ThreshFarValue = trackBarThreshFar.Value;
        }

        private void trackBarBlobMin_Scroll(object sender, EventArgs e)
        {
            labelBlobSizeMinValue.Text = trackBarBlobMin.Value.ToString();
            BlobMinValue = trackBarBlobMin.Value;
        }

        private void trackBarBlobMax_Scroll(object sender, EventArgs e)
        {
            labelBlobSizeMaxValue.Text = trackBarBlobMax.Value.ToString();
            BlobMaxValue = trackBarBlobMax.Value;
        }

        private void trackBarAntiNoise_Scroll(object sender, EventArgs e)
        {
            labelAntiNoiseValue.Text = trackBarAntiNoise.Value.ToString();
            AntiNoiseValue = trackBarAntiNoise.Value;
        }

        private void trackBarBallSmoothing_Scroll(object sender, EventArgs e)
        {
            labelBallSmoothingValue.Text = trackBarBallSmoothing.Value.ToString();
            BallSmoothingValue = trackBarBallSmoothing.Value;
        }

        private void trackBarPreviousBallDistance_Scroll(object sender, EventArgs e)
        {
            labelPreviousBallDistanceValue.Text = trackBarPreviousBallDistance.Value.ToString();
            PreviousBallDistanceThresholdValue = trackBarPreviousBallDistance.Value;
        }

        public void UpdateKinectImage0(Bitmap bmp)
        {
            if (InvokeRequired)
            {
                var d = new delegateUpdateKinectImage(UpdateKinectImage0);
                Invoke(d, bmp);
                return;
            }

            using var graphics0 = pictureBoxKinect0.CreateGraphics();
            var rect0 = new Rectangle(0, 0, pictureBoxKinect0.Width, pictureBoxKinect0.Height);
            graphics0.DrawImage(bmp, rect0);
            graphics0.DrawRectangle(Pens.Red, rect0x, rect0y, rect0width, rect0height);
        }

        public void UpdateFPSLabel(string text)
        {
            if (InvokeRequired)
            {
                var d = new delegateUpdateFPSLabel(UpdateFPSLabel);
                Invoke(d, text);
                return;
            }
            labelFPS.Text = text;
        }

        public void UpdateKinectImagec(Bitmap bmp)
        {
            if (InvokeRequired)
            {
                var d = new UpdateKinectImageDelegate(UpdateKinectImagec);
                Invoke(d, bmp);
                return;
            }

            using var graphicsc = pictureBoxCombined.CreateGraphics();
            var rect1 = new Rectangle(0, 0, pictureBoxCombined.Width, pictureBoxCombined.Height);
            graphicsc.DrawImage(bmp, rect1);
            graphicsc.DrawRectangle(Pens.Orange, rectcx, rectcy, rectcwidth, rectcheight);

            foreach (var irect in ignoreRects)
            {
                graphicsc.DrawRectangle(Pens.Red, irect.X, irect.Y, irect.Width, irect.Height);
            }
        }

        private void pictureBoxKinect0_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            rect0x = e.X;
            rect0y = e.Y;
            rect0width = 1;
            rect0height = 1;
        }

        private void pictureBoxKinect0_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left &&
                e.X - rect0x > 0 && e.X < pictureBoxKinect0.Size.Width &&
                e.Y - rect0y > 0 && e.Y < pictureBoxKinect0.Size.Height)
            {
                rect0width = e.X - rect0x;
                rect0height = e.Y - rect0y;
            }
        }
    }
}