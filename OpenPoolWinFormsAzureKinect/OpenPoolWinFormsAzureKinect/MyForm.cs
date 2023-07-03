using OpenCvSharp;

namespace OpenPoolWinFormsAzureKinect
{
    public partial class MyForm : Form
    {
        public ManualResetEvent TargetSetEvent = new ManualResetEvent(false);

        private int ThreshNearValue;
        private int ThreshFarValue;
        private int BlobMinValue;
        private int BlobMaxValue;
        private int AntiNoiseValue;
        private int BallSmoothingValue;
        private int PreviousBallDistanceThresholdValue;

        private int rect0x;
        private int rect0y;
        private int rect0width;
        private int rect0height;

        private int rectcx;
        private int rectcy;
        private int rectcwidth;
        private int rectcheight;

        private List<Rect> ignoreRects = new List<Rect>();

        private Thread kinectManagerThread;

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
    }
}