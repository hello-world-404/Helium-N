using Microsoft.Win32;
using System;
using System.IO;
using System.Threading;
using System.Windows;
using static Helium.Global;


namespace Helium
{
    public partial class MainWindow : Window
    {
        //設定全局變量
        public string logPath = System.Environment.CurrentDirectory + @"\bin\app.log";
        private string binPath = System.Environment.CurrentDirectory + @"\bin\";

        public MainWindow()
        {
            InitializeComponent();

            create();
            logWrite(APP_INIT);

            //Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.GetCultureInfo("en-US");
        }


        #region ADB指令
        public void adb_dump_battery_data(object sender, RoutedEventArgs e)
        {
            runCommand(dumpBattery);
            logWrite(DMP_BAT_CLICK);
        }


        public void adb_dump_model_data(object sender, RoutedEventArgs e)
        {
            runCommand(getModel);
            logWrite(G_MODEL_CLICK);
        }

        public void adb_take_screenshot(object sender, RoutedEventArgs e)
        {
            runCommand(takeScreenshot);
            logWrite(TK_SCSHT_CLICK);
        }

        public void adb_install(object sender, RoutedEventArgs e)
        {
            string apkDir = null;
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = ".apk 文件 (*.apk) | *.apk";
            dialog.DefaultExt = ".apk";

            if ((bool)dialog.ShowDialog())
            {
                apkDir = dialog.FileName;
                runCommand(installPrefix + apkDir);
            }
            logWrite(INST_APP_CLICK);
        }

        public void others_launch_adb(object sender, RoutedEventArgs e)
        {
            runCommand(launchAdb);
            logWrite(L_ADB_CLICK);
        }

        public void others_launch_shell(object sender, RoutedEventArgs e)
        {
            runCommand(launchShell);
            logWrite(L_SHELL_CLICK);
        }

        public void flash_Reboot(object sender, RoutedEventArgs e)
        {
            runCommand(Global.reboot);
            logWrite(REBOOT_CLICK);
        }

        public void flash_Reboot_REC(object sender, RoutedEventArgs e)
        {
            runCommand(rebootRec);
            logWrite(REBOOT_REC_CLICK);
        }

        public void flash_Check_Fastboot_Devices(object sender, RoutedEventArgs e)
        {
            runCommand(fastbootCheckDevices);
            logWrite(FB_CHECK_DEV_CLICK);
        }

        public void flash_Reboot_BL(object sender, RoutedEventArgs e)
        {
            runCommand(rebootBootloader);
            logWrite(REBOOT_BL_CLICK);
        }

        public void flash_Check_ADB_Devices(object sender, RoutedEventArgs e)
        {
            runCommand(getDevices);
            logWrite(CHK_ADB_CLICK);
        }

        //需要测试刷Rec功能
        public void flash_Recovery_Image(object sender, RoutedEventArgs e)
        {
            string imgDir;

            OpenFileDialog of = new OpenFileDialog();
            of.Filter = ".IMG 文件 (*.img)|*.img";
            of.DefaultExt = ".img";

            if ((bool)of.ShowDialog())
            {
                imgDir = of.FileName;
                runCommand("adb devices && adb reboot bootloader && fastboot devices && fastboot flash recovery " + imgDir + " fastboot reboot-bootloader + fastboot erase cache && fastboot reboot");
                logWrite(FL_REC_IMG);

            }
        }

        #endregion

        #region 服務激活指令

        public void act_IceBox(object sender, RoutedEventArgs e)
        {
            runCommand(iceBox);
            logWrite(ICE_BOX_ACT);
        }

        public void act_Brevent(object sender, RoutedEventArgs e)
        {
            runCommand(brevent);
            logWrite(BREVENT_ACT);
        }


        public void act_airFrozen(object sender, RoutedEventArgs e)
        {
            runCommand(airFrozen);
            logWrite(AIRF_ACT);
        }

        public void act_BlackRoom(object sender, RoutedEventArgs e)
        {
            runCommand(blackRoom);
            logWrite(BR_ACT);
        }

        //TO-DO: 解决权限不足
        public void act_pmDog(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("该功能未进行充分测试，权限狗可能会返回 \'没有权限\'。", "注意",MessageBoxButton.OK, MessageBoxImage.Exclamation);
            runCommand(permissionDog);
            logWrite(PM_ACT);
        }

        #endregion


        //////There is a logic problem here.
        //The real command runner
        public void runCommand(string cm)
        {

            //Process with window
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.Arguments = "/k " + cm;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                //process.StartInfo.CreateNoWindow = true;
                process.Start();
                process.WaitForExit();

                logWrite(RUN_CMD);
                


            //Process with window
            /*
            System.Diagnostics.Process process = new System.Diagnostics.Process();
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.Arguments = "/c " + cm;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                //process.StartInfo.CreateNoWindow = true;
                process.Start();
                process.WaitForExit();
            */
        }

        private bool checkAdbDevice()
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.Arguments = "/c adb devices";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            string res = process.StandardOutput.ReadToEnd();

            process.WaitForExit();

            if(res.Length == 24)
            {
                //There is no device found
            }
            else
            {
                //Extract device information
            }

            MessageBox.Show(res, "Log", MessageBoxButton.OK, MessageBoxImage.Error);

            return true;
        }

        /*
        private void read()
        {
            if (!File.Exists(logPath))
            {
                create();
                logWrite("Storage created");
            }

            if (File.Exists(logPath))
            {
                using (StreamReader reader = File.OpenText(logPath))
                {
                    string str = String.Empty;
                    while ((str = reader.ReadLine()) != null)
                    {


                    }
                }
            }
        }
        */

        private void create()
        {
            if (!Directory.Exists(binPath))
            {
                System.IO.Directory.CreateDirectory(binPath);
            }

            if (!Directory.Exists(logPath))
            {
                using (StreamWriter sw = File.CreateText(logPath)) ;
                logWrite("Log file created");
            }
        }

        public void logWrite(string action)
        {
            File.AppendAllText(logPath, DateTime.Now.ToString() + "--" + action + Environment.NewLine);
        }

        private void RecordScreen_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void moreBtn_Click(object sender, RoutedEventArgs e)
        {
            AboutWindow abt = new AboutWindow();
            abt.Show();
        }
    }
}