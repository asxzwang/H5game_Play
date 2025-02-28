using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using H5Play;
using CefSharp.MinimalExample.WinForms;
using Microsoft.VisualBasic;
using Microsoft.Win32;

namespace H5Play
{
    public partial class SideAcc : Form
    {
        public SideAcc()
        {
            InitializeComponent();
        }

        private void SideAcc_Load(object sender, EventArgs e)
        {
            labelName.Left = (this.Width - labelName.Width) / 2;
            labelName.Text = "[当前多开路线为"+ Program.CacheName.Replace("Main", "主线路") + "]";
            GetNameList();
        }

        private void GetNameList()
        {
            try
            {
                listBox1.Items.Clear();
                DirectoryInfo dir = new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CefSharp\\Cache"));
                FileSystemInfo[] fileinfo = dir.GetDirectories();
                foreach (FileSystemInfo i in fileinfo)
                {
                    listBox1.Items.Add(i.Name.Replace("Main", "主线路"));
                    Application.DoEvents();
                }
            }
            catch { }
        }

        private void OpenIt_Click(object sender, EventArgs e)
        {
            if (listBox1.Text !="")
            {
                System.Diagnostics.Process.Start(System.Reflection.Assembly.GetExecutingAssembly().Location, "SideAcc "+ listBox1.Text.Replace("主线路", "Main"));
            }
        }

        private void AddIt_Click(object sender, EventArgs e)
        {
            String s = Interaction.InputBox("请输入新添加线路的名称：", "添加线路", "", -1, -1);
            if(s!="")
            {
                if (s == "主线路"|| s == "Main")
                    return;
                String NamePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CefSharp\\Cache\\" + s);
                if (!Directory.Exists(NamePath))
                {
                    Directory.CreateDirectory(NamePath);
                }
                MessageBox.Show("添加多开线路"+s+"成功。", "提示", MessageBoxButtons.OK);
                GetNameList();
            }
            else
                MessageBox.Show("添加失败，线路名称不能为空。", "提示", MessageBoxButtons.OK);
        }

        private void DeleteIt_Click(object sender, EventArgs e)
        {
            if (listBox1.Text != "")
            {
                if(listBox1.Text == "主线路")
                {
                    MessageBox.Show("主线路无法删除。", "提示", MessageBoxButtons.OK);
                    return;
                }
                if(listBox1.Text == Program.CacheName)
                {
                    MessageBox.Show("当前线路正在使用，无法删除。", "提示", MessageBoxButtons.OK);
                    return;
                }
                if (MessageBox.Show("确定要删除一切该线路的内容吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                    return;
                // 删除文件缓存
                Program.DelectDir(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CefSharp\\Cache"));
                // 新增：删除注册表中的相关键值
                string lineName = listBox1.Text;
                try
                {
                    using (RegistryKey key = Registry.CurrentUser.OpenSubKey("software\\H5PlayCef", writable: true))
                    {
                        if (key != null)
                        {
                            // 删除 Title_{线路名} 键
                            string titleKey = $"Title_{lineName}";
                            if (key.GetValue(titleKey) != null)
                            {
                                key.DeleteValue(titleKey);
                            }

                            // 可选：删除其他与该线路关联的键（例如 Volume_Test、Skin_Test 等）
                            // key.DeleteValue($"OtherKey_{lineName}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"注册表清理失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                MessageBox.Show("多开线路" + listBox1.Text + "删除成功。", "提示", MessageBoxButtons.OK);
                GetNameList();
            }
        }

        private void DefaultIt_Click(object sender, EventArgs e)
        {
            if (listBox1.Text != "")
            {
                RegistryKey key = Registry.CurrentUser;
                RegistryKey software = key.OpenSubKey("software\\H5PlayCef", true);
                software.SetValue("CacheName", listBox1.Text.Replace("主线路","Main"));
                software.Close();
                key.Close();
                MessageBox.Show("已将多开线路" + listBox1.Text + "设置为默认路线。", "提示", MessageBoxButtons.OK);
            }
        }
    }
}
