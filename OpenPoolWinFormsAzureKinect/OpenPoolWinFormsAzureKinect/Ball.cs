using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenPoolWinFormsAzureKinect;

public class Ball
{
    public double X { get; set; }
    public double Y { get; set; }
    public int PreviousBallId { get; set; }

    public Ball()
    {
        X = -1;
        Y = -1;
        PreviousBallId = -1;
    }

    public Ball(double x, double y, int id = -1)
    {
        X = x;
        Y = y;
        PreviousBallId = id;
    }
}
