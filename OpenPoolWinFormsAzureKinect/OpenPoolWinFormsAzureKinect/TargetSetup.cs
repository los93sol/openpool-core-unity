using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenPoolWinFormsAzureKinect
{
    public partial class TargetSetup : Form
    {
        private string targetIp;
        private int targetPort;

        public TargetSetup()
        {
            targetIp = "127.0.0.1";
            targetPort = 7000;

            InitializeComponent();
        }

        private void TargetSetup_Load(object sender, EventArgs e)
        {
            textBoxIp.Text = targetIp;
            textBoxPort.Text = targetPort.ToString();
        }

        private void buttonGo_Click(object sender, EventArgs e)
        {
            var parentForm = (MyForm)Owner!;

            parentForm.labelTargetIp.Text = textBoxIp.Text;
            parentForm.labelTargetPort.Text = textBoxPort.Text;

            Close();
        }
    }
}
