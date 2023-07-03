using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenPoolWinFormsAzureKinect;

public class AppConfig
{
    public int ThreshNearValue { get; set; }
    public int ThreshFarValue { get; set; }
    public int BlobMinValue { get; set; }
    public int BlobMaxValue { get; set; }
    public int AntiNoiseValue { get; set; }
    public int BallSmoothingValue { get; set; }
    public int PreviousBallDistanceThresholdValue { get; set; }

    public int rect0x { get; set; }
    public int rect0y { get; set; }
    public int rect0width { get; set; }
    public int rect0height { get; set; }
}
