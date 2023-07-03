namespace OpenPoolWinFormsAzureKinect
{
    partial class TargetSetup
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
            label1 = new Label();
            label2 = new Label();
            textBoxIp = new TextBox();
            textBoxPort = new TextBox();
            buttonGo = new Button();
            Text = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 59);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(60, 15);
            label1.TabIndex = 0;
            label1.Text = "IP address";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 90);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(29, 15);
            label2.TabIndex = 1;
            label2.Text = "Port";
            // 
            // textBoxIp
            // 
            textBoxIp.Location = new Point(94, 59);
            textBoxIp.Margin = new Padding(4);
            textBoxIp.Name = "textBoxIp";
            textBoxIp.Size = new Size(233, 23);
            textBoxIp.TabIndex = 2;
            // 
            // textBoxPort
            // 
            textBoxPort.Location = new Point(94, 86);
            textBoxPort.Margin = new Padding(4);
            textBoxPort.Name = "textBoxPort";
            textBoxPort.Size = new Size(233, 23);
            textBoxPort.TabIndex = 3;
            // 
            // buttonGo
            // 
            buttonGo.Location = new Point(335, 52);
            buttonGo.Margin = new Padding(4);
            buttonGo.Name = "buttonGo";
            buttonGo.Size = new Size(107, 58);
            buttonGo.TabIndex = 4;
            buttonGo.Text = "Go";
            buttonGo.UseVisualStyleBackColor = true;
            buttonGo.Click += buttonGo_Click;
            // 
            // Text
            // 
            Text.AutoSize = true;
            Text.Location = new Point(12, 22);
            Text.Margin = new Padding(4, 0, 4, 0);
            Text.Name = "Text";
            Text.Size = new Size(148, 15);
            Text.TabIndex = 0;
            Text.Text = "Set Target IP and Port here:";
            // 
            // TargetSetup
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(456, 125);
            Controls.Add(Text);
            Controls.Add(buttonGo);
            Controls.Add(textBoxPort);
            Controls.Add(textBoxIp);
            Controls.Add(label2);
            Controls.Add(label1);
            Margin = new Padding(4);
            Name = "TargetSetup";
            Load += TargetSetup_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private new Label Text;
        private Label label1;
        private Label label2;
        private TextBox textBoxIp;
        private TextBox textBoxPort;
        private Button buttonGo;
    }
}