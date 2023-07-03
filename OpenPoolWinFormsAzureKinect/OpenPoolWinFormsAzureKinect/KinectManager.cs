using Microsoft.Azure.Kinect.Sensor;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenPoolWinFormsAzureKinect;

public class KinectManager
{
    private int _depthWidth = 512;
    private int _depthHeight = 424;

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
            var videoImg16 = new Mat(_depthHeight, _depthWidth, MatType.CV_8UC1);

            Device kinect = Device.Open();

            kinect.StartCameras(new DeviceConfiguration
            {
                ColorFormat = ImageFormat.ColorBGRA32,
                ColorResolution = ColorResolution.R1080p,
                DepthMode = DepthMode.NFOV_2x2Binned,
                SynchronizedImagesOnly = true
            });

            using var capture = kinect.GetCapture();
            using var depthImage = capture.Depth;

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

            while (true)
            {
                //kinect.GetCapture().Depth.
            }
        }
        catch (Exception ex)
        {
            // TODO
        }
    }
}
