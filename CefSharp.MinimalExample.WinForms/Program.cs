// Copyright © 2010-2015 The CefSharp Authors. All rights reserved.
//
// Use of this source code is governed by a BSD-style license that can be found in the LICENSE file.

using CefSharp.WinForms;
using Microsoft.VisualBasic;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Text;

namespace CefSharp.MinimalExample.WinForms
{
    public class Program
    {
    
        //设置项
        public static bool Stick;
        public static string Title;
        public static bool MiniMode;
        public static string SkinName;
        public static bool SkinAero;
        public static bool AutoClrMry;
        public static bool MousePrtSc;
        public static string CacheName;
        public static int Volumes;
        public static bool PageMode;
        public static bool SayHello;

        public static string PlayURL = "https://h5.nafeini.com/play/?g_token=iASIxeH4K9cjOn/+imtDkQ==";


        private static void GetSettings()
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser)
                using (RegistryKey software = key.CreateSubKey("software\\H5PlayCef"))
                {
                    string titleKey = $"Title_{CacheName}";
                    string info = software.GetValue(titleKey, "Nothing").ToString();

                    if (info == "Nothing")
                    {
                        software.SetValue(titleKey, "H5游戏登录器");
                        Title = "H5游戏登录器";
                    }
                    else
                    {
                        Title = info;
                    }

                    info = software.GetValue("AutoClrMry", "Nothing").ToString();
                    AutoClrMry = info != "Nothing" && info == "True";

                    if (string.IsNullOrEmpty(CacheName))
                    {
                        info = software.GetValue("CacheName", "Nothing").ToString();
                        CacheName = info == "Nothing" ? "Main" : info;
                        if (info == "Nothing")
                        {
                            software.SetValue("CacheName", "Main");
                        }
                    }

                    info = software.GetValue("Volume", "Nothing").ToString();
                    Volumes = info == "Nothing" ? 0 : Convert.ToInt32(info);
                    if (info == "Nothing")
                    {
                        software.SetValue("Volume", "0");
                    }

                    info = software.GetValue("Stick", "Nothing").ToString();
                    Stick = info != "Nothing" && info == "True";
                    if (info == "Nothing")
                    {
                        software.SetValue("Stick", "False");
                    }
                }
            }
            catch (Exception ex)
            {
                // 记录异常信息
                Console.WriteLine($"Error getting settings: {ex.Message}");
            }
        }

        public static bool Delay(int delayTime)
        {
            DateTime now = DateTime.Now;
            int s;
            do
            {
                TimeSpan spand = DateTime.Now - now;
                s = spand.Seconds;
                Application.DoEvents();
            }
            while (s < delayTime);
            return true;
        }
        
        public static string Right(string str, int i)
        {
            return str.Substring(str.Length - i, i);
        }

        public static void DelectDir(string srcPath)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(srcPath);
                FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();  //返回目录中所有文件和子目录
                foreach (FileSystemInfo i in fileinfo)
                {
                    if (i is DirectoryInfo)            //判断是否文件夹
                    {
                        DirectoryInfo subdir = new DirectoryInfo(i.FullName);
                        try
                        {
                            subdir.Delete(true);          //删除子目录和文件
                        }
                        catch { }
                    }
                    else
                    {
                        try
                        {
                            File.Delete(i.FullName);      //删除指定文件
                        }
                        catch { }
                    }
                }
            }
            catch
            {
                ;
            }
        }

        public static string Ds(string s, string e)
        {

            try
            {
                byte[] key = Encoding.Unicode.GetBytes(e);
                byte[] data = Convert.FromBase64String(s);
                DESCryptoServiceProvider descsp = new DESCryptoServiceProvider();
                MemoryStream MStream = new MemoryStream();
                CryptoStream CStream = new CryptoStream(MStream, descsp.CreateDecryptor(key, key), CryptoStreamMode.Write);
                CStream.Write(data, 0, data.Length);
                CStream.FlushFinalBlock();
                byte[] temp = MStream.ToArray();
                CStream.Close();
                MStream.Close();
                return Encoding.Unicode.GetString(temp);
            }
            catch
            {
                return s;
            }
        }

        public static bool WinIsLoad(string winName)
        {
            bool flag = false;//判断标志
            FormCollection formCollection = Application.OpenForms;//获取存在的窗体集合
            foreach (Form name in formCollection)//循环遍历，判断
            {
                if (name.Name == winName)//判断是否存在该窗体
                {
                    flag = true;
                    name.Activate();
                }
            }
            return flag;
        }

        [STAThread]
        public static void Main(string[] args)
        {
            // Monitor parent process exit and close subprocesses if parent process exits first
            CefSharpSettings.SubprocessExitIfParentProcessClosed = true;

            // For Windows 7 and above, best to include relevant app.manifest entries as well
            Cef.EnableHighDPISupport();

            CacheName = string.Empty;

            if (args.Length > 0)
            {
                try
                {
                    if (string.Equals(args[0], "SideAcc", StringComparison.OrdinalIgnoreCase))
                    {
                        CacheName = args[1];
                    }
                }
                catch (Exception ex)
                {
                    // 记录异常信息
                    Console.WriteLine($"Error processing arguments: {ex.Message}");
                }
            }

            GetSettings();

            var settings = new CefSettings
            {
                Locale = "zh-CN",
                CachePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CefSharp\\Cache\\" + CacheName),
                //UserAgent = "Mozilla/5.0 (Linux; Android 14; Pixel 7) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Mobile Safari/537.36",
                WindowlessRenderingEnabled = true,
                BackgroundColor = 0xFFFFFFFF
            };

            settings.CefCommandLineArgs.Add("--touch-events", "enabled");
            settings.CefCommandLineArgs.Add("--touch-devices", "1");  // 明确启用触摸设备
            settings.CefCommandLineArgs.Add("--enable-viewport", "1");
            settings.CefCommandLineArgs.Add("enable-media-stream", "1");
            settings.CefCommandLineArgs.Add("--enable-features", "TouchableAppContextMenu,WebContentsForceRender");
            settings.CefCommandLineArgs.Add("--enable-touch-drag-and-drop", "1");
            settings.CefCommandLineArgs.Add("--enable-touch-events", "1");
            settings.CefCommandLineArgs.Add("--disable-threaded-scrolling", "1");
            settings.CefCommandLineArgs.Add("--device-scale-factor", "2");
            //settings.CefCommandLineArgs.Add("--disable-gpu", "1");

            Cef.Initialize(settings, performDependencyCheck: true, browserProcessHandler: null);
            Application.EnableVisualStyles();

            var browser = new BrowserForm();
            Application.Run(browser);
        }
    }
}
