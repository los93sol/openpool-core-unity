using Microsoft.Azure.Kinect.Sensor;
using OpenCvSharp;
using System.Diagnostics;

namespace OpenPoolWinFormsAzureKinect;

public class KinectManager
{
    private int _depthWidth = 1024;
    private int _depthHeight = 1024;

    public void KinectManagerThread(object param)
    {
        string targetIp = "127.0.0.1";
        int targetPort = 7000;

        var formInstance = (MyForm)param;

        formInstance.TargetSetEvent.WaitOne();

        targetIp = formInstance.labelTargetIp.Text.ToString();
        targetPort = Convert.ToInt32(formInstance.labelTargetPort.Text.ToString());

        // TODO: UdpTransmitSocket

        try
        {
            //var videoImg16 = new Mat(_depthHeight, _depthWidth, MatType.CV_8UC1);

            Device kinect = Device.Open();

            kinect.StartCameras(new DeviceConfiguration
            {
                ColorFormat = ImageFormat.ColorBGRA32,
                ColorResolution = ColorResolution.R1080p,
                DepthMode = DepthMode.WFOV_2x2Binned,
                SynchronizedImagesOnly = true
            });

            var image0 = new Mat(kinect.GetCalibration().DepthCameraCalibration.ResolutionHeight, kinect.GetCalibration().DepthCameraCalibration.ResolutionWidth, MatType.CV_16UC1);
            var image0c = new Mat(kinect.GetCalibration().DepthCameraCalibration.ResolutionHeight, kinect.GetCalibration().DepthCameraCalibration.ResolutionWidth, MatType.CV_8UC3);

            var mv = new List<Mat>();

            var whiteImg8 = new Mat(kinect.GetCalibration().DepthCameraCalibration.ResolutionHeight, kinect.GetCalibration().DepthCameraCalibration.ResolutionWidth, MatType.CV_8UC1);

            var resizedImage = new Mat(kinect.GetCalibration().DepthCameraCalibration.ResolutionHeight, kinect.GetCalibration().DepthCameraCalibration.ResolutionWidth, MatType.CV_8UC1);

            whiteImg8.SetTo(255);

            var backgroundVideoImg = new Mat(kinect.GetCalibration().DepthCameraCalibration.ResolutionHeight, kinect.GetCalibration().DepthCameraCalibration.ResolutionWidth, MatType.CV_8UC1);
            var diffVideoImg = new Mat(kinect.GetCalibration().DepthCameraCalibration.ResolutionHeight, kinect.GetCalibration().DepthCameraCalibration.ResolutionWidth, MatType.CV_8UC1);
            var outputVideoImg = new Mat(kinect.GetCalibration().DepthCameraCalibration.ResolutionHeight, kinect.GetCalibration().DepthCameraCalibration.ResolutionWidth, MatType.CV_8UC4);

            var rectForImageWindow = new Rect(0, 0, 100, 100);
            var rectForCombinedImageWindow = new List<Rect>();

            var sw = new Stopwatch();
            var frameCount = 0;
            sw.Start();

            while (true)
            {
                using var capture = kinect.GetCapture();
                using var depthImage = capture.Depth;

                try
                {
                    ProcessDepth(depthImage.GetPixels<ushort>().ToArray(), depthImage.WidthPixels, depthImage.HeightPixels, ref image0);
                    Cv2.Flip(image0, image0, FlipMode.Y);

                    mv.Clear();
                    mv.Add(image0);
                    mv.Add(image0);
                    mv.Add(image0);

                    Cv2.Merge(mv.ToArray(), image0c);

                    var ptr0 = image0c.Data;
                    var b0 = new Bitmap(image0c.Width, image0c.Height, (int)image0c.Step(), System.Drawing.Imaging.PixelFormat.Format24bppRgb, ptr0);

                    ++frameCount;

                    if (sw.Elapsed > TimeSpan.FromSeconds(2))
                    {
                        var framesPerSecond = (double)frameCount / sw.Elapsed.TotalSeconds;
                        var text = $"FPS: {framesPerSecond:F2}";
                        formInstance.UpdateFPSLabel(text);
                        sw.Restart();
                    }

                    formInstance.UpdateKinectImage0(b0);

                    rectForImageWindow = new Rect(formInstance.rect0x, formInstance.rect0y, formInstance.rect0width, formInstance.rect0height);

                    ResizeImage(image0, resizedImage, rectForImageWindow);

                    rectForCombinedImageWindow = formInstance.GetIgnoreRects;

                    CoreEngine(resizedImage, backgroundVideoImg, diffVideoImg, outputVideoImg,
                        formInstance.ThreshNearValue, formInstance.ThreshFarValue, formInstance.BlobMinValue,
                        formInstance.BlobMaxValue, formInstance.AntiNoiseValue, formInstance.BallSmoothingValue,
                        formInstance.PreviousBallDistanceThresholdValue, rectForCombinedImageWindow, ref frameCount); //, ref UdpSoc);

                    var ptrc = outputVideoImg.Data;
                    var bc = new Bitmap(outputVideoImg.Width, outputVideoImg.Height, (int)outputVideoImg.Step(), System.Drawing.Imaging.PixelFormat.Format32bppArgb, ptrc);

                    formInstance.UpdateKinectImagec(bc);
                }
                catch
                {

                }
            }
        }
        catch (Exception ex)
        {
            // TODO
        }
    }

    private void ProcessDepth(ushort[] depthData, int width, int height, ref Mat image)
    {
        if (depthData == null || depthData.Length != width * height)
            return;

        if (image.Rows != height || image.Cols != width || image.Type() != MatType.CV_8UC1)
            image = new Mat(height, width, MatType.CV_8UC1);

        byte[] imageData = new byte[width * height];

        var minDepth = ushort.MaxValue;
        var maxDepth = ushort.MinValue;

        for (int i = 0; i < depthData.Length; i++)
        {
            ushort depth = depthData[i];

            if (depth < minDepth)
            {
                minDepth = depth;
            }

            if (depth > maxDepth)
            {
                maxDepth = depth;
            }
        }

        for (int i = 0; i < depthData.Length; i++)
        {
            ushort depth = depthData[i];

            byte intensity = (byte)((depth >= minDepth && depth <= maxDepth) ? (depth - minDepth) * (256.0 / (maxDepth - minDepth)) : 0);

            int x = i % width;
            int y = i / width;

            imageData[y * width + x] = intensity;
        }

        image.SetArray<byte>(imageData);
    }

    //private void ResizeImage(Mat imagein, Mat imageout, Rect rect)
    //{
    //    var temp = new Mat(imagein, new(2 * rect.Y, 2 * (rect.Y + rect.Height)), new(2 * rect.X, 2 * (rect.X + rect.Width)));
    //    Cv2.Resize(temp, imageout, imageout.Size());
    //}

    void ResizeImage(Mat imagein, Mat imageout, Rect rect)
    {
        Mat temp = new Mat(imagein, new Rect(2 * rect.X, 2 * rect.Y, 2 * rect.Width, 2 * rect.Height));
        Cv2.Resize(temp, imageout, imageout.Size());
    }

    int CoreEngine(Mat videoImg8,
               Mat backgroundVideoImg,
               Mat diffVideoImg,
               Mat outputVideoImg,
               int binalization_threshold_near,
               int binalization_threshold_far,
               int blob_min,
               int blob_max,
               int antinoise_count,
               int ballsmooth_count,
               int previousball_distance_threshold,
               List<Rect> ignoreRects,
               ref int frameCount)
               //UdpTransmitSocket soc)
    {
        var tempVideoImg1 = new Mat();
        var tempVideoImg2 = new Mat();
        var colorTempVideoImg = new Mat();
        var whiteImg = new Mat(videoImg8.Size(), MatType.CV_8UC1, new Scalar(255));

        List<Mat> mv = new List<Mat>();

        // Grayscale to 4ch image
        mv.Clear();
        mv.Add(videoImg8);
        mv.Add(videoImg8);
        mv.Add(videoImg8);
        mv.Add(whiteImg);

        Cv2.Merge(mv.ToArray(), colorTempVideoImg);

        // Clear outputVideoImg
        outputVideoImg.SetTo(new Scalar(0, 0, 0, 0));

        // diff
        Cv2.Absdiff(videoImg8, backgroundVideoImg, diffVideoImg);

        // Binalization
        Cv2.Threshold(diffVideoImg, tempVideoImg1, binalization_threshold_near, 255, ThresholdTypes.Binary);
        Cv2.Threshold(diffVideoImg, tempVideoImg2, binalization_threshold_far, 255, ThresholdTypes.BinaryInv);

        // And
        Cv2.BitwiseAnd(tempVideoImg1, tempVideoImg2, tempVideoImg1);

        // Erode & dilate
        Cv2.Erode(tempVideoImg1, tempVideoImg2, null, null, antinoise_count);
        Cv2.Dilate(tempVideoImg2, tempVideoImg1, null, null, antinoise_count);

        // Labelling
        //var contours = new List<List<OpenCvSharp.Point>>();
        var ballframes = new Queue<List<Ball>>();
        var balls = new List<Ball>();

        Cv2.FindContours(tempVideoImg1, out OpenCvSharp.Point[][] contoursArray, out var hierarchy, RetrievalModes.List, ContourApproximationModes.ApproxSimple);
        var contours = new List<OpenCvSharp.Point[]>(contoursArray);

        Cv2.DrawContours(outputVideoImg, contours, -1, Scalar.Red, 2);

        foreach (var contour in contours)
        {
            if (contour.Count() <= blob_min || contour.Count() >= blob_max)
            {
                continue;
            }
            else
            {
                var ignore = false;

                var pointsMatrix = new Mat(contour.Count(), 1, MatType.CV_32SC2, contour.Select(p => new OpenCvSharp.Point(p.X, p.Y)).ToArray());
                var color = Scalar.Green;

                var r0 = Cv2.BoundingRect(pointsMatrix);
                var x = (r0.X + r0.Width / 2);
                var y = r0.Y + r0.Height / 2;

                foreach (var irect in ignoreRects)
                {
                    if (x < irect.X || (irect.X + irect.Width) < x ||
                        y < irect.Y * 2 || (irect.Y + irect.Height) * 2 < y)
                    {
                        continue;
                    }
                    else
                    {
                        ignore = true;
                        break;
                    }
                }

                if (!ignore)
                {
                    Cv2.Rectangle(outputVideoImg, r0, color, 1);

                    double normalized_x = (double)x / (double)outputVideoImg.Cols;
                    double normalized_y = (double)y / (double)outputVideoImg.Rows;

                    balls.Add(new Ball(normalized_x, normalized_y));
                }
            }
        }

        if (balls.Count != 0)
        {
            // TODO: find previous id;
            if (ballframes.Count != 0)
            {
                List<Ball> balls_previous = ballframes.Last();

                for (int i = 0; i < balls.Count; i++)
                {
                    double distance = Math.Pow(outputVideoImg.Cols, 2) + Math.Pow(outputVideoImg.Rows, 2);

                    double distance_temp = 0;

                    for (int id = 0; id < balls_previous.Count; id++)
                    {
                        Ball b_p = balls_previous[id];

                        distance_temp = Math.Pow((balls[i].X - b_p.X) * outputVideoImg.Cols, 2) + Math.Pow((balls[i].Y - b_p.Y) * outputVideoImg.Rows, 2);

                        if (Math.Pow(((double)previousball_distance_threshold / (double)100) * outputVideoImg.Cols * 0.1, 2) > distance_temp && distance > distance_temp)
                        {
                            distance = distance_temp;
                            balls[i].PreviousBallId = id;
                        }
                    }
                }
            }
            // Add to buffer
            ballframes.Enqueue(balls);
            while (ballframes.Count > ballsmooth_count + 1)
            {
                ballframes.Dequeue();
            }

            // TODO: smoothing operation
            List<Ball> balls_smooth = new List<Ball>();
            List<Ball> now_balls;

            balls_smooth.Clear();
            double smooth_x, smooth_y;

            now_balls = ballframes.Last();

            foreach (Ball now_ball in now_balls)
            {
                int previous_count = 1;
                Ball previous_ball = new Ball(-1, -1, -1);
                Ball temp_ball = new Ball(-1, -1, -1);

                smooth_x = now_ball.X;
                smooth_y = now_ball.Y;

                temp_ball.X = now_ball.X;
                temp_ball.Y = now_ball.Y;
                temp_ball.PreviousBallId = now_ball.PreviousBallId;

                while (previous_count <= ballsmooth_count && previous_count < ballframes.Count)
                {
                    if (temp_ball.PreviousBallId < 0)
                    {
                        break;
                    }
                    else
                    {
                        previous_ball = ballframes.ElementAt(ballframes.Count - previous_count - 1)[temp_ball.PreviousBallId];

                        smooth_x += previous_ball.X;
                        smooth_y += previous_ball.Y;

                        Cv2.Line(outputVideoImg,
                            new OpenCvSharp.Point((int)(previous_ball.X * outputVideoImg.Cols), (int)(previous_ball.Y * outputVideoImg.Rows)),
                            new OpenCvSharp.Point((int)(temp_ball.X * outputVideoImg.Cols), (int)(temp_ball.Y * outputVideoImg.Rows)),
                            Scalar.White,
                            5);

                        temp_ball.X = previous_ball.X;
                        temp_ball.Y = previous_ball.Y;
                        temp_ball.PreviousBallId = previous_ball.PreviousBallId;

                        previous_count++;
                    }
                }

                smooth_x = (double)smooth_x / (double)(previous_count);
                smooth_y = (double)smooth_y / (double)(previous_count);

                Cv2.Circle(outputVideoImg, new OpenCvSharp.Point((int)(smooth_x * outputVideoImg.Cols), (int)(smooth_y * outputVideoImg.Rows)), 5, Scalar.Green);
                Cv2.Circle(outputVideoImg, new OpenCvSharp.Point((int)(smooth_x * outputVideoImg.Cols), (int)(smooth_y * outputVideoImg.Rows)), 15, Scalar.Green);
                Cv2.Circle(outputVideoImg, new OpenCvSharp.Point((int)(smooth_x * outputVideoImg.Cols), (int)(smooth_y * outputVideoImg.Rows)), 20, Scalar.Green);
                Cv2.Circle(outputVideoImg, new OpenCvSharp.Point((int)(smooth_x * outputVideoImg.Cols), (int)(smooth_y * outputVideoImg.Rows)), 25, Scalar.Green);

                balls_smooth.Add(new Ball(smooth_x, smooth_y, previous_count));
            }
            
            // TODO: SendOSC_ball(gFrameNum, balls_smooth, soc);
        }

        if (frameCount >= 100)
        {
            frameCount = 0;
        }
        
        Cv2.AddWeighted(colorTempVideoImg, 0.5, outputVideoImg, 0.5, 0.0, outputVideoImg);

        return 0;
    }

}
