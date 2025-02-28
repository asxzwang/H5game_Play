namespace CefSharp.MinimalExample.WinForms
{
    partial class BrowserForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BrowserForm));
            this.toolStripContainer = new System.Windows.Forms.ToolStripContainer();
            this.statusLabel = new System.Windows.Forms.Label();
            this.outputLabel = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.FreshPage = new System.Windows.Forms.ToolStripMenuItem();
            this.windows = new System.Windows.Forms.ToolStripMenuItem();
            this.SmallSize = new System.Windows.Forms.ToolStripMenuItem();
            this.MiddleSize = new System.Windows.Forms.ToolStripMenuItem();
            this.NormalSize = new System.Windows.Forms.ToolStripMenuItem();
            this.BigSize = new System.Windows.Forms.ToolStripMenuItem();
            this.OtherSize = new System.Windows.Forms.ToolStripMenuItem();
            this.Action = new System.Windows.Forms.ToolStripMenuItem();
            this.ClearMemories = new System.Windows.Forms.ToolStripMenuItem();
            this.ClearCache = new System.Windows.Forms.ToolStripMenuItem();
            this.ClearAllCache = new System.Windows.Forms.ToolStripMenuItem();
            this.break32 = new System.Windows.Forms.ToolStripSeparator();
            this.AutoClearMemories = new System.Windows.Forms.ToolStripMenuItem();
            this.Settings = new System.Windows.Forms.ToolStripMenuItem();
            this.CacheNames = new System.Windows.Forms.ToolStripMenuItem();
            this.break31 = new System.Windows.Forms.ToolStripSeparator();
            this.VolumeChange = new System.Windows.Forms.ToolStripMenuItem();
            this.Stick = new System.Windows.Forms.ToolStripMenuItem();
            this.ChangeTitle = new System.Windows.Forms.ToolStripMenuItem();
            this.TimerClrMry = new System.Windows.Forms.Timer(this.components);
            this.TimerSize = new System.Windows.Forms.Timer(this.components);
            this.toolStripContainer.ContentPanel.SuspendLayout();
            this.toolStripContainer.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer
            // 
            // 
            // toolStripContainer.ContentPanel
            // 
            this.toolStripContainer.ContentPanel.Controls.Add(this.statusLabel);
            this.toolStripContainer.ContentPanel.Controls.Add(this.outputLabel);
            resources.ApplyResources(this.toolStripContainer.ContentPanel, "toolStripContainer.ContentPanel");
            resources.ApplyResources(this.toolStripContainer, "toolStripContainer");
            this.toolStripContainer.Name = "toolStripContainer";
            // 
            // statusLabel
            // 
            resources.ApplyResources(this.statusLabel, "statusLabel");
            this.statusLabel.Name = "statusLabel";
            // 
            // outputLabel
            // 
            resources.ApplyResources(this.outputLabel, "outputLabel");
            this.outputLabel.Name = "outputLabel";
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FreshPage,
            this.windows,
            this.Action,
            this.Settings});
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Name = "menuStrip1";
            // 
            // FreshPage
            // 
            this.FreshPage.Name = "FreshPage";
            resources.ApplyResources(this.FreshPage, "FreshPage");
            this.FreshPage.Click += new System.EventHandler(this.FreshPage_Click);
            // 
            // windows
            // 
            this.windows.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SmallSize,
            this.MiddleSize,
            this.NormalSize,
            this.BigSize,
            this.OtherSize});
            this.windows.Name = "windows";
            resources.ApplyResources(this.windows, "windows");
            // 
            // SmallSize
            // 
            this.SmallSize.Name = "SmallSize";
            resources.ApplyResources(this.SmallSize, "SmallSize");
            this.SmallSize.Click += new System.EventHandler(this.SmallSize_Click);
            // 
            // MiddleSize
            // 
            this.MiddleSize.Name = "MiddleSize";
            resources.ApplyResources(this.MiddleSize, "MiddleSize");
            this.MiddleSize.Click += new System.EventHandler(this.MiddleSize_Click);
            // 
            // NormalSize
            // 
            this.NormalSize.Name = "NormalSize";
            resources.ApplyResources(this.NormalSize, "NormalSize");
            this.NormalSize.Click += new System.EventHandler(this.NormalSize_Click);
            // 
            // BigSize
            // 
            this.BigSize.Name = "BigSize";
            resources.ApplyResources(this.BigSize, "BigSize");
            this.BigSize.Click += new System.EventHandler(this.BigSize_Click);
            // 
            // OtherSize
            // 
            this.OtherSize.Name = "OtherSize";
            resources.ApplyResources(this.OtherSize, "OtherSize");
            this.OtherSize.Click += new System.EventHandler(this.OtherSize_Click);
            // 
            // Action
            // 
            this.Action.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ClearMemories,
            this.ClearCache,
            this.ClearAllCache,
            this.break32,
            this.AutoClearMemories});
            this.Action.Name = "Action";
            resources.ApplyResources(this.Action, "Action");
            // 
            // ClearMemories
            // 
            this.ClearMemories.Name = "ClearMemories";
            resources.ApplyResources(this.ClearMemories, "ClearMemories");
            this.ClearMemories.Click += new System.EventHandler(this.ClearMemories_Click);
            // 
            // ClearCache
            // 
            this.ClearCache.Name = "ClearCache";
            resources.ApplyResources(this.ClearCache, "ClearCache");
            this.ClearCache.Click += new System.EventHandler(this.ClearCache_Click);
            // 
            // ClearAllCache
            // 
            this.ClearAllCache.Name = "ClearAllCache";
            resources.ApplyResources(this.ClearAllCache, "ClearAllCache");
            this.ClearAllCache.Click += new System.EventHandler(this.ClearAllCache_Click);
            // 
            // break32
            // 
            this.break32.Name = "break32";
            resources.ApplyResources(this.break32, "break32");
            // 
            // AutoClearMemories
            // 
            this.AutoClearMemories.Name = "AutoClearMemories";
            resources.ApplyResources(this.AutoClearMemories, "AutoClearMemories");
            this.AutoClearMemories.Click += new System.EventHandler(this.AutoClearMemories_Click);
            // 
            // Settings
            // 
            this.Settings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CacheNames,
            this.break31,
            this.VolumeChange,
            this.Stick,
            this.ChangeTitle});
            this.Settings.Name = "Settings";
            resources.ApplyResources(this.Settings, "Settings");
            // 
            // CacheNames
            // 
            this.CacheNames.Name = "CacheNames";
            resources.ApplyResources(this.CacheNames, "CacheNames");
            this.CacheNames.Click += new System.EventHandler(this.CacheNames_Click);
            // 
            // break31
            // 
            this.break31.Name = "break31";
            resources.ApplyResources(this.break31, "break31");
            // 
            // VolumeChange
            // 
            this.VolumeChange.Name = "VolumeChange";
            resources.ApplyResources(this.VolumeChange, "VolumeChange");
            this.VolumeChange.Click += new System.EventHandler(this.VolumeChange_Click);
            // 
            // Stick
            // 
            this.Stick.Name = "Stick";
            resources.ApplyResources(this.Stick, "Stick");
            this.Stick.Click += new System.EventHandler(this.Stick_Click);
            // 
            // ChangeTitle
            // 
            this.ChangeTitle.Name = "ChangeTitle";
            resources.ApplyResources(this.ChangeTitle, "ChangeTitle");
            this.ChangeTitle.Click += new System.EventHandler(this.ChangeTitle_Click);
            // 
            // TimerClrMry
            // 
            this.TimerClrMry.Interval = 30000;
            this.TimerClrMry.Tick += new System.EventHandler(this.TimerClrMry_Tick);
            // 
            // TimerSize
            // 
            this.TimerSize.Tick += new System.EventHandler(this.TimerSize_Tick);
            // 
            // BrowserForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStripContainer);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "BrowserForm";
            this.Resize += new System.EventHandler(this.BrowserForm_Resize);
            this.toolStripContainer.ContentPanel.ResumeLayout(false);
            this.toolStripContainer.ContentPanel.PerformLayout();
            this.toolStripContainer.ResumeLayout(false);
            this.toolStripContainer.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer toolStripContainer;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem Action;
        private System.Windows.Forms.Label outputLabel;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.ToolStripMenuItem ClearAllCache;
        private System.Windows.Forms.ToolStripMenuItem Settings;
        private System.Windows.Forms.ToolStripMenuItem Stick;
        private System.Windows.Forms.ToolStripMenuItem ChangeTitle;
        private System.Windows.Forms.ToolStripMenuItem FreshPage;
        private System.Windows.Forms.ToolStripMenuItem ClearMemories;
        private System.Windows.Forms.ToolStripMenuItem SmallSize;
        private System.Windows.Forms.ToolStripMenuItem MiddleSize;
        private System.Windows.Forms.ToolStripMenuItem NormalSize;
        private System.Windows.Forms.ToolStripMenuItem BigSize;
        private System.Windows.Forms.ToolStripMenuItem OtherSize;
        private System.Windows.Forms.ToolStripSeparator break31;
        private System.Windows.Forms.ToolStripMenuItem AutoClearMemories;
        private System.Windows.Forms.Timer TimerClrMry;
        private System.Windows.Forms.ToolStripSeparator break32;
        private System.Windows.Forms.ToolStripMenuItem CacheNames;
        private System.Windows.Forms.ToolStripMenuItem VolumeChange;
        private System.Windows.Forms.Timer TimerSize;
        private System.Windows.Forms.ToolStripMenuItem ClearCache;
        private System.Windows.Forms.ToolStripMenuItem windows;
    }
}