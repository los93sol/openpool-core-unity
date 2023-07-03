using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenPoolWinFormsAzureKinect
{
    public partial class HelpForm : Form
    {
        public HelpForm()
        {
            InitializeComponent();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void labelUrl_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo { FileName = labelUrl.Text, UseShellExecute = true });
        }
    }
}
