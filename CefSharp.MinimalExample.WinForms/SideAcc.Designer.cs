namespace H5Play
{
    partial class SideAcc
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
            this.labelName = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.openIt = new System.Windows.Forms.Button();
            this.deleteIt = new System.Windows.Forms.Button();
            this.addIt = new System.Windows.Forms.Button();
            this.defaultIt = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(96, 24);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(206, 18);
            this.labelName.TabIndex = 3;
            this.labelName.Text = "[当前多开路线为主路线]";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 18;
            this.listBox1.Location = new System.Drawing.Point(14, 75);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(218, 328);
            this.listBox1.TabIndex = 4;
            // 
            // openIt
            // 
            this.openIt.Location = new System.Drawing.Point(249, 75);
            this.openIt.Name = "openIt";
            this.openIt.Size = new System.Drawing.Size(123, 39);
            this.openIt.TabIndex = 5;
            this.openIt.Text = "打开线路";
            this.openIt.UseVisualStyleBackColor = true;
            this.openIt.Click += new System.EventHandler(this.OpenIt_Click);
            // 
            // deleteIt
            // 
            this.deleteIt.Location = new System.Drawing.Point(249, 190);
            this.deleteIt.Name = "deleteIt";
            this.deleteIt.Size = new System.Drawing.Size(123, 39);
            this.deleteIt.TabIndex = 6;
            this.deleteIt.Text = "删除线路";
            this.deleteIt.UseVisualStyleBackColor = true;
            this.deleteIt.Click += new System.EventHandler(this.DeleteIt_Click);
            // 
            // addIt
            // 
            this.addIt.Location = new System.Drawing.Point(249, 134);
            this.addIt.Name = "addIt";
            this.addIt.Size = new System.Drawing.Size(123, 39);
            this.addIt.TabIndex = 7;
            this.addIt.Text = "添加线路";
            this.addIt.UseVisualStyleBackColor = true;
            this.addIt.Click += new System.EventHandler(this.AddIt_Click);
            // 
            // defaultIt
            // 
            this.defaultIt.Location = new System.Drawing.Point(249, 249);
            this.defaultIt.Name = "defaultIt";
            this.defaultIt.Size = new System.Drawing.Size(123, 39);
            this.defaultIt.TabIndex = 8;
            this.defaultIt.Text = "设为默认";
            this.defaultIt.UseVisualStyleBackColor = true;
            this.defaultIt.Click += new System.EventHandler(this.DefaultIt_Click);
            // 
            // SideAcc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(387, 428);
            this.Controls.Add(this.defaultIt);
            this.Controls.Add(this.addIt);
            this.Controls.Add(this.deleteIt);
            this.Controls.Add(this.openIt);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.labelName);
            this.MaximizeBox = false;
            this.Name = "SideAcc";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "小号多开";
            this.Load += new System.EventHandler(this.SideAcc_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button openIt;
        private System.Windows.Forms.Button deleteIt;
        private System.Windows.Forms.Button addIt;
        private System.Windows.Forms.Button defaultIt;
    }
}