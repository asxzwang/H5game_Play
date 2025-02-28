using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CefSharp.MinimalExample.WinForms
{

    public partial class Volume : Form
    {
        //音量
        [DllImport("winmm.dll")]
        private static extern int waveOutSetVolume(int uDeviceID, int dwVolume);
        // 判断激活窗口
        [DllImport("user32")]
        private static extern IntPtr GetActiveWindow(); 
        public bool showing;
        private int tempVolume;

        public Volume()
        {
            InitializeComponent();
            Opacity = 0.0; //窗体透明度为0
            //TimerFade.Start(); //计时开始
            tempVolume = Program.Volumes;
            
        }

        private void Volume_Load(object sender, EventArgs e)
        {
            //trackBar1.BackColor = Color.Red;
            if (Program.Volumes > 0)
            {
                volumeFull.Visible = true;
                volumeMuted.Visible = false;
            }
            else
            {
                volumeFull.Visible = false;
                volumeMuted.Visible = true;
            }
            if (Program.Volumes<10)
                label1.Text = "当前音量：  " + Program.Volumes + "%";
            else if (Program.Volumes < 100)
                label1.Text = "当前音量： " + Program.Volumes + "%";
            else
                label1.Text = "当前音量：" + Program.Volumes + "%";
            trackBar1.Value = Program.Volumes;
        }

        private void TimerFade_Tick(object sender, EventArgs e)
        {
            double d = 0.10;
            if (showing)
            {
                if (Opacity + d >= 0.9)
                {
                    Opacity = 0.9;
                    TimerFade.Enabled = false;
                    TimerCheck.Enabled = true;
                }
                else
                {
                    Opacity += d;
                }
            }
            else
            {
                if (Opacity - d <= 0.0)
                {
                    Opacity = 0.0;
                    
                    TimerFade.Enabled = false;
                }
                else
                {
                    Opacity -= d;
                }
            }

        }

        private void volumeFull_Click(object sender, EventArgs e)
        {
            volumeFull.Visible = false;
            volumeMuted.Visible = true;
            RegistryKey key = Registry.CurrentUser;
            RegistryKey software = key.OpenSubKey("software\\H5PlayCef", true);
            Program.Volumes = 0;
            software.SetValue("Volume", Program.Volumes.ToString());
            software.Close();
            key.Close();
            label1.Text = "当前音量：  0%";
            SetLR(0, 0);
        }

        private void volumeMuted_Click(object sender, EventArgs e)
        {
            volumeFull.Visible = true;
            volumeMuted.Visible = false;
            RegistryKey key = Registry.CurrentUser;
            RegistryKey software = key.OpenSubKey("software\\H5PlayCef", true);
            Program.Volumes = tempVolume;
            software.SetValue("Volume", Program.Volumes.ToString());
            software.Close();
            key.Close();
            if (Program.Volumes < 10)
                label1.Text = "当前音量：  " + Program.Volumes + "%";
            else if (Program.Volumes < 100)
                label1.Text = "当前音量： " + Program.Volumes + "%";
            else
                label1.Text = "当前音量：" + Program.Volumes + "%";
            SetLR(Program.Volumes * 65535 / 100, Program.Volumes * 65535 / 100);
        }

        private void label2_Click(object sender, EventArgs e)
        {
            showing = false;
            TimerFade.Enabled = true;
        }

        private void SetLR(int L, int R)
        {
            string mLR;
            mLR = "0x" + Program.Right("0000" + Convert.ToString(L, 16), 4) + Program.Right("0000" + Convert.ToString(R, 16), 4);
            waveOutSetVolume(-1, Convert.ToInt32(mLR, 16));
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            SetLR(trackBar1.Value * 65535 / 100, trackBar1.Value * 65535 / 100);
            Program.Volumes = trackBar1.Value;
            RegistryKey key = Registry.CurrentUser;
            RegistryKey software = key.OpenSubKey("software\\H5PlayCef", true);
            software.SetValue("Volume", Program.Volumes.ToString());
            software.Close();
            key.Close();
            if (Program.Volumes < 10)
                label1.Text = "当前音量：  " + Program.Volumes + "%";
            else if (Program.Volumes < 100)
                label1.Text = "当前音量： " + Program.Volumes + "%";
            else
                label1.Text = "当前音量：" + Program.Volumes + "%";
            tempVolume = Program.Volumes;
            if (tempVolume == 0)
                tempVolume = 100;
            if (Program.Volumes > 0)
            {
                volumeFull.Visible = true;
                volumeMuted.Visible = false;
            }
            else
            {
                volumeFull.Visible = false;
                volumeMuted.Visible = true;
            }
        }

        private void TimerCheck_Tick(object sender, EventArgs e)
        {
            if (GetActiveWindow() != (this.Handle) && GetActiveWindow() != (BrowserForm.FormHandle))
            {
                TimerCheck.Enabled = false;
                showing = false;
                TimerFade.Start();
            }
        }
    }
}
