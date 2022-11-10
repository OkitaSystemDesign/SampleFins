namespace SampleFins
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnFinsUdp = new System.Windows.Forms.Button();
            this.btnFinsUdpASync = new System.Windows.Forms.Button();
            this.btnFinsTcp = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnFinsUdp
            // 
            this.btnFinsUdp.Location = new System.Drawing.Point(40, 30);
            this.btnFinsUdp.Name = "btnFinsUdp";
            this.btnFinsUdp.Size = new System.Drawing.Size(150, 50);
            this.btnFinsUdp.TabIndex = 0;
            this.btnFinsUdp.Text = "FinsUdp\r\n同期通信";
            this.btnFinsUdp.UseVisualStyleBackColor = true;
            this.btnFinsUdp.Click += new System.EventHandler(this.btnFinsUdp_Click);
            // 
            // btnFinsUdpASync
            // 
            this.btnFinsUdpASync.Location = new System.Drawing.Point(40, 99);
            this.btnFinsUdpASync.Name = "btnFinsUdpASync";
            this.btnFinsUdpASync.Size = new System.Drawing.Size(150, 50);
            this.btnFinsUdpASync.TabIndex = 1;
            this.btnFinsUdpASync.Text = "FinsUdpASync\r\n非同期通信";
            this.btnFinsUdpASync.UseVisualStyleBackColor = true;
            this.btnFinsUdpASync.Click += new System.EventHandler(this.btnFinsUdpASync_Click);
            // 
            // btnFinsTcp
            // 
            this.btnFinsTcp.Location = new System.Drawing.Point(250, 30);
            this.btnFinsTcp.Name = "btnFinsTcp";
            this.btnFinsTcp.Size = new System.Drawing.Size(150, 50);
            this.btnFinsTcp.TabIndex = 2;
            this.btnFinsTcp.Text = "FinsTcp\r\n同期通信";
            this.btnFinsTcp.UseVisualStyleBackColor = true;
            this.btnFinsTcp.Click += new System.EventHandler(this.btnFinsTcp_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(537, 227);
            this.Controls.Add(this.btnFinsTcp);
            this.Controls.Add(this.btnFinsUdpASync);
            this.Controls.Add(this.btnFinsUdp);
            this.Name = "Form1";
            this.Text = "SampleFins";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Button btnFinsUdp;
        private Button btnFinsUdpASync;
        private Button btnFinsTcp;
    }
}