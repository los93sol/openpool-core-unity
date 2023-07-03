namespace OpenPoolWinFormsAzureKinect
{
    partial class HelpForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HelpForm));
            label1 = new Label();
            CloseButton = new Button();
            labelUrl = new Label();
            pictureBoxLogo = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBoxLogo).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(14, 104);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(144, 15);
            label1.TabIndex = 0;
            label1.Text = "OpenPoolGUI version 0.10";
            // 
            // CloseButton
            // 
            CloseButton.Location = new Point(410, 15);
            CloseButton.Margin = new Padding(1);
            CloseButton.Name = "CloseButton";
            CloseButton.Size = new Size(104, 28);
            CloseButton.TabIndex = 1;
            CloseButton.Text = "Close";
            CloseButton.UseVisualStyleBackColor = true;
            CloseButton.Click += CloseButton_Click;
            // 
            // labelUrl
            // 
            labelUrl.AutoSize = true;
            labelUrl.ForeColor = SystemColors.HotTrack;
            labelUrl.Location = new Point(330, 104);
            labelUrl.Margin = new Padding(1, 0, 1, 0);
            labelUrl.Name = "labelUrl";
            labelUrl.Size = new Size(138, 15);
            labelUrl.TabIndex = 2;
            labelUrl.Text = "http://www.openpool.cc";
            labelUrl.TextAlign = ContentAlignment.MiddleCenter;
            labelUrl.Click += labelUrl_Click;
            // 
            // pictureBoxLogo
            // 
            pictureBoxLogo.BackgroundImage = (Image)resources.GetObject("pictureBoxLogo.BackgroundImage");
            pictureBoxLogo.BackgroundImageLayout = ImageLayout.Center;
            pictureBoxLogo.Location = new Point(14, 15);
            pictureBoxLogo.Margin = new Padding(4);
            pictureBoxLogo.Name = "pictureBoxLogo";
            pictureBoxLogo.Size = new Size(276, 85);
            pictureBoxLogo.TabIndex = 0;
            pictureBoxLogo.TabStop = false;
            // 
            // HelpForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(524, 141);
            Controls.Add(pictureBoxLogo);
            Controls.Add(labelUrl);
            Controls.Add(CloseButton);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(4);
            Name = "HelpForm";
            Text = "About";
            ((System.ComponentModel.ISupportInitialize)pictureBoxLogo).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBoxLogo;
        private Label label1;
        private Label labelUrl;
        private Button CloseButton;
    }
}