namespace DNX.Helpers.WinForms.Forms
{
    partial class Splash
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
            this.lblAppTitle = new System.Windows.Forms.Label();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.lblMessage = new System.Windows.Forms.Label();
            this.lnkURL = new System.Windows.Forms.LinkLabel();
            this.lblInfoText = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // lblAppTitle
            // 
            this.lblAppTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblAppTitle.Font = new System.Drawing.Font("Corbel", 27.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAppTitle.ForeColor = System.Drawing.Color.Blue;
            this.lblAppTitle.Location = new System.Drawing.Point(0, 0);
            this.lblAppTitle.Name = "lblAppTitle";
            this.lblAppTitle.Size = new System.Drawing.Size(395, 44);
            this.lblAppTitle.TabIndex = 0;
            this.lblAppTitle.Text = "App Title";
            this.lblAppTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // picLogo
            // 
            this.picLogo.Location = new System.Drawing.Point(12, 47);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(96, 96);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picLogo.TabIndex = 1;
            this.picLogo.TabStop = false;
            // 
            // lblMessage
            // 
            this.lblMessage.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblMessage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblMessage.Location = new System.Drawing.Point(0, 173);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Padding = new System.Windows.Forms.Padding(9, 0, 0, 0);
            this.lblMessage.Size = new System.Drawing.Size(395, 23);
            this.lblMessage.TabIndex = 2;
            this.lblMessage.Text = "Some Message";
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lnkURL
            // 
            this.lnkURL.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lnkURL.Location = new System.Drawing.Point(0, 150);
            this.lnkURL.Name = "lnkURL";
            this.lnkURL.Size = new System.Drawing.Size(395, 23);
            this.lnkURL.TabIndex = 3;
            this.lnkURL.TabStop = true;
            this.lnkURL.Text = "Some Link";
            this.lnkURL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lnkURL.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkURL_LinkClicked);
            // 
            // lblInfoText
            // 
            this.lblInfoText.Location = new System.Drawing.Point(120, 47);
            this.lblInfoText.Name = "lblInfoText";
            this.lblInfoText.Size = new System.Drawing.Size(263, 95);
            this.lblInfoText.TabIndex = 4;
            this.lblInfoText.Text = "Some Info Text";
            // 
            // Splash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(395, 196);
            this.ControlBox = false;
            this.Controls.Add(this.lblInfoText);
            this.Controls.Add(this.lnkURL);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.picLogo);
            this.Controls.Add(this.lblAppTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Splash";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Splash";
            this.Click += new System.EventHandler(this.Splash_Click);
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblAppTitle;
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.LinkLabel lnkURL;
        private System.Windows.Forms.Label lblInfoText;
    }
}