namespace CefSharp.MinimalExample.WinForms
{
    partial class Volume
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Volume));
            this.label1 = new System.Windows.Forms.Label();
            this.TimerFade = new System.Windows.Forms.Timer(this.components);
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.volumeFull = new System.Windows.Forms.PictureBox();
            this.volumeMuted = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TimerCheck = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.volumeFull)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.volumeMuted)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(96, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(197, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "当前音量：100%";
            // 
            // TimerFade
            // 
            this.TimerFade.Interval = 10;
            this.TimerFade.Tick += new System.EventHandler(this.TimerFade_Tick);
            // 
            // trackBar1
            // 
            this.trackBar1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(41)))), ((int)(((byte)(41)))));
            this.trackBar1.Location = new System.Drawing.Point(25, 66);
            this.trackBar1.Maximum = 100;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.trackBar1.Size = new System.Drawing.Size(287, 56);
            this.trackBar1.TabIndex = 2;
            this.trackBar1.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // volumeFull
            // 
            this.volumeFull.Cursor = System.Windows.Forms.Cursors.Hand;
            this.volumeFull.Image = ((System.Drawing.Image)(resources.GetObject("volumeFull.Image")));
            this.volumeFull.Location = new System.Drawing.Point(25, 12);
            this.volumeFull.Name = "volumeFull";
            this.volumeFull.Size = new System.Drawing.Size(51, 48);
            this.volumeFull.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.volumeFull.TabIndex = 1;
            this.volumeFull.TabStop = false;
            this.volumeFull.Visible = false;
            this.volumeFull.Click += new System.EventHandler(this.volumeFull_Click);
            // 
            // volumeMuted
            // 
            this.volumeMuted.Cursor = System.Windows.Forms.Cursors.Hand;
            this.volumeMuted.Image = ((System.Drawing.Image)(resources.GetObject("volumeMuted.Image")));
            this.volumeMuted.Location = new System.Drawing.Point(25, 12);
            this.volumeMuted.Name = "volumeMuted";
            this.volumeMuted.Size = new System.Drawing.Size(51, 48);
            this.volumeMuted.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.volumeMuted.TabIndex = 3;
            this.volumeMuted.TabStop = false;
            this.volumeMuted.Visible = false;
            this.volumeMuted.Click += new System.EventHandler(this.volumeMuted_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(300, -2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 36);
            this.label2.TabIndex = 4;
            this.label2.Text = "×";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // TimerCheck
            // 
            this.TimerCheck.Tick += new System.EventHandler(this.TimerCheck_Tick);
            // 
            // Volume
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(41)))), ((int)(((byte)(41)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(335, 116);
            this.ControlBox = false;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.volumeMuted);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.volumeFull);
            this.Controls.Add(this.label1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Volume";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Volume_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.volumeFull)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.volumeMuted)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox volumeFull;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.PictureBox volumeMuted;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.Timer TimerFade;
        private System.Windows.Forms.Timer TimerCheck;
    }
}