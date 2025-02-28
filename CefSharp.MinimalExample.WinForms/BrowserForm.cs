// Copyright © 2010-2015 The CefSharp Authors. All rights reserved.
//
// Use of this source code is governed by a BSD-style license that can be found in the LICENSE file.

using System;
using System.Windows.Forms;
using CefSharp.MinimalExample.WinForms.Controls;
using CefSharp.WinForms;
using System.Runtime.InteropServices;
using System.IO;
using Microsoft.Win32;
using Microsoft.VisualBasic;
using System.Drawing;
using H5Play;
using System.Resources;
using System.Collections.Generic;


namespace CefSharp.MinimalExample.WinForms
{
    public partial class BrowserForm : Form
    {
        //打开程序
        [DllImport("shell32.dll")]
        private static extern int ShellExecute(int hwnd, string lpoperation, string lpfile, string lpparameters, string lpdirectory, int nshowcmd);
        //音量
        [DllImport("winmm.dll")]
        private static extern int waveOutSetVolume(int uDeviceID, int dwVolume);
        //置顶
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int y, int Width, int Height, int flags);
        //释放内存
        [DllImport("kernel32.dll", EntryPoint = "SetProcessWorkingSetSize")]
        public static extern int SetProcessWorkingSetSize(IntPtr process, int minSize, int maxSize);
        //释放内存
        [DllImport("psapi")]
        private static extern int EmptyWorkingSet(int hProcess);
        [DllImport("KERNEL32")]
        private static extern int GetCurrentProcessId();
        [DllImport("KERNEL32")]
        private static extern int CloseHandle(int hObject);
        [DllImport("KERNEL32")]
        private static extern int OpenProcess(int dwDesiredAccess, int bInheritHandle, int dwProcessID);


        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;


        private static ChromiumWebBrowser browser;
        private Volume waveForm;
        public static IntPtr FormHandle;
        private static Size XandY;
        private static bool reSize = false;


        [DllImport("user32.dll")]
        private static extern bool RegisterTouchWindow(IntPtr hWnd, uint ulFlags);
        private const uint TWF_WANTPALM = 0x00000002;
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            // 注册窗体接收触摸输入
            RegisterTouchWindow(this.Handle, TWF_WANTPALM);
        }

        [DllImport("User32.dll")]
        private static extern bool GetTouchInputInfo(IntPtr hTouchInput, int cInputs, [Out] TOUCHINPUT[] pInputs, int cbSize);

        [DllImport("User32.dll")]
        private static extern void CloseTouchInputHandle(IntPtr hTouchInput);

        private const int WM_TOUCH = 0x0240;

        [StructLayout(LayoutKind.Sequential)]
        public struct TOUCHINPUT
        {
            public int X;
            public int Y;
            public IntPtr hSource;
            public int dwID;
            public int dwFlags;
            public int dwMask;
            public int dwTime;
            public IntPtr dwExtraInfo;
            public int cxContact;
            public int cyContact;
        }

        private readonly Dictionary<int, Point> touchPoints = new Dictionary<int, Point>();

        // 启用窗体拖放
        protected override void OnDragEnter(DragEventArgs drgevent)
        {
            drgevent.Effect = DragDropEffects.Move;
            base.OnDragEnter(drgevent);
        }


        public BrowserForm()
        {
            InitializeComponent();
            this.AutoScaleMode = AutoScaleMode.Dpi;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.FormBorderStyle = FormBorderStyle.Sizable; // 恢复默认边框
            browser = new ChromiumWebBrowser(Program.PlayURL)
            {
                Dock = DockStyle.Fill,
            };
            toolStripContainer.ContentPanel.Controls.Add(browser);

            // 监听页面加载完成事件
            browser.FrameLoadEnd += OnFrameLoadEnd;
            browser.IsBrowserInitializedChanged += OnIsBrowserInitializedChanged;
            browser.StatusMessage += OnBrowserStatusMessage;
            SetWin();
            SetStick();
            SetTimer();
            FormHandle = this.Handle;

        }



        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_TOUCH:
                    HandleTouch(m);
                    break;
            }
            base.WndProc(ref m);
        }

        private void HandleTouch(Message m)
        {
            int inputCount = m.WParam.ToInt32() & 0xFFFF;
            TOUCHINPUT[] inputs = new TOUCHINPUT[inputCount];

            if (GetTouchInputInfo(m.LParam, inputCount, inputs, Marshal.SizeOf(typeof(TOUCHINPUT))))
            {
                foreach (var input in inputs)
                {
                    int touchId = input.dwID;
                    Point pt = new Point(input.X / 100, input.Y / 100); // 转换坐标
                    pt = this.PointToClient(pt);

                    if ((input.dwFlags & 0x01) != 0) // TOUCHEVENTF_DOWN
                    {
                        touchPoints[touchId] = pt;
                    }
                    else if ((input.dwFlags & 0x02) != 0) // TOUCHEVENTF_UP
                    {
                        touchPoints.Remove(touchId);
                    }
                    else if ((input.dwFlags & 0x04) != 0) // TOUCHEVENTF_MOVE
                    {
                        touchPoints[touchId] = pt;
                    }
                }
                CloseTouchInputHandle(m.LParam);
                this.Invalidate(); // 请求重绘
            }
        }


        public class TransparentOverlay : Control
        {
            public Dictionary<int, Point> TouchPoints { get; set; } = new Dictionary<int, Point>();

            public TransparentOverlay()
            {
                this.DoubleBuffered = true;
                this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
                this.BackColor = Color.Transparent;
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);
                foreach (var point in TouchPoints.Values)
                {
                    using (var brush = new SolidBrush(Color.FromArgb(128, 0, 0, 255)))
                    {
                        e.Graphics.FillEllipse(brush, point.X - 25, point.Y - 25, 50, 50);
                    }
                }
            }
        }




        private void Browser_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void OnFrameLoadEnd(object sender, FrameLoadEndEventArgs e)
        {
            ProcessFrame(e.Frame);

            // 处理子框架
            var childFrames = e.Frame.Browser.GetFrameIdentifiers();
            foreach (var childFrameId in childFrames)
            {
                var childFrame = e.Frame.Browser.GetFrame(childFrameId);
                if (childFrame != null)
                {
                    ProcessFrame(childFrame);
                }
            }
        }

        private void ProcessFrame(IFrame frame)
        {
            const string script = @"
            (function(){
                // 移除全局拖动逻辑
                let isDragging = false;
                let startX = 0;
                let startY = 0;

                // 仅允许特定元素拖动
                const enableDragForElement = (element) => {
                    element.style.webkitUserDrag = 'element';
                    element.style.cursor = 'move';

                    element.addEventListener('mousedown', e => {
                        if(e.button !== 0) return; // 仅左键
                        isDragging = true;
                        startX = e.clientX;
                        startY = e.clientY;
                    });

                    document.addEventListener('mousemove', e => {
                        if (!isDragging) return;
                        const deltaX = e.clientX - startX;
                        const deltaY = e.clientY - startY;
                        window.external.SendDragMessage(deltaX, deltaY);
                        startX = e.clientX;
                        startY = e.clientY;
                    });

                    document.addEventListener('mouseup', () => {
                        isDragging = false;
                    });
                };

                // 只允许class包含'draggable'的元素拖动
                document.querySelectorAll('.draggable').forEach(enableDragForElement);
            })();
            ";
            frame.ExecuteJavaScriptAsync(script);
        }


        ~BrowserForm()
        {
            browser.Dispose();
            Cef.Shutdown();
            Close();
        }
        private void OnIsBrowserInitializedChanged(object sender, IsBrowserInitializedChangedEventArgs e)
        {
            if(e.IsBrowserInitialized)
            {
                var b = ((ChromiumWebBrowser)sender);

                this.InvokeOnUiThreadIfRequired(() => b.Focus());

            }
            
        }

        private void OnBrowserStatusMessage(object sender, StatusMessageEventArgs args)
        {
            this.InvokeOnUiThreadIfRequired(() => statusLabel.Text = args.Value);
        }



        private void DisplayOutput(string output)
        {
            if (outputLabel.InvokeRequired)
            {
                outputLabel.BeginInvoke((Action)(() => outputLabel.Text = output));
            }
            else
            {
                outputLabel.Text = output;
            }
        }

        private void BackButtonClick(object sender, EventArgs e)
        {
            browser.Back();
        }

        private void ForwardButtonClick(object sender, EventArgs e)
        {
            browser.Forward();
        }

        private void LoadUrl(string url)
        {
            if (Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute))
            {
                browser.Load(url);
            }
        }

        private void FreshPage_Click(object sender, EventArgs e)
        {
            LoadUrl(Program.PlayURL);
        }
 
        private void SetWin()
        {
            Text = Program.Title;
            waveForm = new Volume();
            waveForm.Show();
            SetLR(Program.Volumes * 65535 / 100, Program.Volumes * 65535 / 100);
            this.Height = 720+56;
            this.Width = 1280+22;
            XandY.Height = this.Height;
            XandY.Width = this.Width;
            
        }


        private void SetStick()
        {
            if (Program.Stick == true)
            {
                this.Stick.Checked = true;
                SetWindowPos(this.Handle, -1, 0, 0, 0, 0, 1 | 2);
            }
            else
            {
                this.Stick.Checked = false;
                SetWindowPos(this.Handle, -2, 0, 0, 0, 0, 1 | 2);
            }
        }

        private void SetTimer()
        {
            TimerClrMry.Enabled = Program.AutoClrMry;
            AutoClearMemories.Checked = Program.AutoClrMry;
        }

        private void ClearMemory(bool ClearAll)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
            }
            if (ClearAll)
            {
                try
                {
                    int nHandle;
                    int nReturn;
                    int i_PID;
                    i_PID = GetCurrentProcessId();
                    nHandle = OpenProcess(0x1F0FFF, 0, i_PID);
                    nReturn = EmptyWorkingSet(nHandle);
                    CloseHandle(nHandle);
                }
                catch { }
            }
        }

        private void SetLR(int L,int R)
        {
            string mLR;
            mLR = "0x" + Program.Right("0000" + Convert.ToString(L, 16), 4) + Program.Right("0000" + Convert.ToString(R, 16), 4);
            waveOutSetVolume(-1,Convert.ToInt32(mLR, 16));
        }


        private void ClearCache_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("清理缓存需要重启登录器并清理路线"+ "“"+ Program.CacheName.Replace("Main", "主线路") + "”"+"中的缓存文件，是否继续？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                return;
            browser.Dispose();
            Cef.Shutdown();
            Program.DelectDir(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CefSharp\\Cache\\" + Program.CacheName + "\\Cache"));
            Application.Exit();
            System.Diagnostics.Process.Start(System.Reflection.Assembly.GetExecutingAssembly().Location);
        }

        private void ClearAllCache_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("清空数据需要重启登录器并清空一切网页内容，是否继续？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                return;
            browser.Dispose();
            Cef.Shutdown();
            Program.DelectDir(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CefSharp\\Cache"));
            Application.Exit();
            System.Diagnostics.Process.Start(System.Reflection.Assembly.GetExecutingAssembly().Location);
        }

        private void Stick_Click(object sender, EventArgs e)
        {
            Program.Stick = !(Program.Stick);
            SetStick();
            RegistryKey key = Registry.CurrentUser;
            RegistryKey software = key.OpenSubKey("software\\H5PlayCef", true);
            software.SetValue("Stick", (Program.Stick ? "True" : "False"));
            software.Close();
            key.Close();
            MsgNote msgnote;
            if (Program.Stick)
            {
                msgnote = new MsgNote("窗口已置顶");
                msgnote.Show();
            }
            else
            {
                msgnote = new MsgNote("已取消置顶");
                msgnote.Show();
            }
        }

        private void ChangeTitle_Click(object sender, EventArgs e)
        {
            String s = Interaction.InputBox("请输入需要修改的登录器标题：", "修改标题", Program.Title, -1, -1);
            if (string.IsNullOrWhiteSpace(s)) return;

            try
            {
                Program.Title = s;
                this.Text = s;

                // 修改点：保存到带实例标识的Key ------
                string titleKey = $"Title_{Program.CacheName}";
                RegistryKey key = Registry.CurrentUser;
                using (RegistryKey software = key.OpenSubKey("software\\H5PlayCef", true))
                {
                    software.SetValue(titleKey, s);
                }
                // -----------------------------------
            }
            catch (Exception ex)
            {
                MessageBox.Show($"保存失败：{ex.Message}");
            }
        }

        private void ClearMemories_Click(object sender, EventArgs e)
        {
            ClearMemory(true);
            MsgNote msgnote = new MsgNote("内存已释放");
            msgnote.Show();
        }

        private void SmallSize_Click(object sender, EventArgs e)
        {
            this.Height = 324;
            this.Width = 480;
        }

        private void MiddleSize_Click(object sender, EventArgs e)
        {
            this.Height = 460;
            this.Width = 720;
        }

        private void NormalSize_Click(object sender, EventArgs e)
        {
            this.Height = 720+22;
            this.Width = 1280+56;
        }

        private void BigSize_Click(object sender, EventArgs e)
        {
            this.Height = 1000;
            this.Width = 1682;
        }

        private void OtherSize_Click(object sender, EventArgs e)
        {
            MessageBox.Show("使用鼠标直接拖拽窗口即可快速缩放。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void AutoClearMemories_Click(object sender, EventArgs e)
        {
            Program.AutoClrMry = !(Program.AutoClrMry);
            TimerClrMry.Enabled = Program.AutoClrMry;
            RegistryKey key = Registry.CurrentUser;
            RegistryKey software = key.OpenSubKey("software\\H5PlayCef", true);
            software.SetValue("AutoClrMry", (Program.AutoClrMry ? "True" : "False"));
            software.Close();
            key.Close();
            SetTimer();
            MsgNote msgnote;
            if (Program.AutoClrMry)
            {
                msgnote = new MsgNote("开启自动释放内存");
                msgnote.Show();
            }
            else
            {
                msgnote = new MsgNote("关闭自动释放内存");
                msgnote.Show();
            }
        }

        private void TimerClrMry_Tick(object sender, EventArgs e)
        {
            ClearMemory(false);
        }

        private void VolumeChange_Click(object sender, EventArgs e)
        {
            waveForm.showing = true;
            waveForm.TimerFade.Start();
        }

        private void BrowserForm_Resize(object sender, EventArgs e)
        {

            if (this.WindowState == FormWindowState.Normal)
            {

                if (!reSize)
                {
                    XandY.Height = this.Height;
                    XandY.Width = this.Width;
                }
                else
                {
                    TimerSize.Enabled = true;
                }
            }
            else if (this.WindowState == FormWindowState.Minimized || this.WindowState == FormWindowState.Maximized)
            {
                reSize = true;
            }
        }

        private void TimerSize_Tick(object sender, EventArgs e)
        {
            TimerSize.Enabled = false;
            this.Height = XandY.Height;
            this.Width = XandY.Width;
            reSize = false;
        }


        private void CacheNames_Click(object sender, EventArgs e)
        {
            if (!(Program.WinIsLoad("SideAcc")))
            {
                SideAcc sideacc = new SideAcc();
                sideacc.Show();
            }
        }

    }
}
