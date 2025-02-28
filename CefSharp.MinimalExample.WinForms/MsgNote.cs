using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CefSharp.MinimalExample.WinForms
{
    public partial class MsgNote : Form
    {
        private bool showing = true;

        public MsgNote(string note)
        {
            InitializeComponent();
            Opacity = 0.0; //窗体透明度为0
            TimerFade.Start(); //计时开始
            label1.Text = "    " + note + "    ";
        }

        private void MsgNote_Load(object sender, EventArgs e)
        {
            label1.Top = (this.Height - label1.Height) / 2 + 2;
            pictureBox1.Top  = (this.Height - pictureBox1.Height) / 2;
            label1.Left = pictureBox1.Left + pictureBox1.Width;
            this.Width = label1.Left + label1.Width;
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
                    TimerChange.Enabled = true;
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
                    this.Close();
                }
                else
                {
                    Opacity -= d;
                }
            }

        }

        private void TimerChange_Tick(object sender, EventArgs e)
        {
            TimerChange.Enabled = false;
            showing = false;
            TimerFade.Enabled = true;
        }
    }
}
