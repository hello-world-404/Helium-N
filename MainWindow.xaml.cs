using Microsoft.Win32;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using static Helium.Global;


namespace Helium
{

    public partial class MainWindow : Window
    {
        private static bool isVerbose;
        private static bool needsCheckDevice;

        public MainWindow()
        {
            InitializeComponent();

            checkAdbDevice();
        }


        ///All event handlers for click events

        //ADB指令Grid命令
        public void adb_dump_battery_data(object sender, RoutedEventArgs e)
        {
            runCommand(dumpBattery);
        }


        public void adb_dump_model_data(object sender, RoutedEventArgs e)
        {
            runCommand(getModel);
        }

        public void adb_take_screenshot(object sender, RoutedEventArgs e)
        {
            runCommand(takeScreenshot);
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
        }

        //其他指令Grid命令
        public void others_launch_adb(object sender, RoutedEventArgs e)
        {
            runCommand(launchAdb);
        }

        public void others_launch_shell(object sender, RoutedEventArgs e)
        {
            runCommand(launchShell);
        }

        //刷机指令Grid命令
        public void flash_Reboot(object sender, RoutedEventArgs e)
        {
            runCommand(Global.reboot);
        }

        public void flash_Reboot_REC(object sender, RoutedEventArgs e)
        {
            runCommand(rebootRec);
        }

        public void flash_Check_Fastboot_Devices(object sender, RoutedEventArgs e)
        {
            runCommand(fastbootCheckDevices);
        }

        public void flash_Reboot_BL(object sender, RoutedEventArgs e)
        {
            runCommand(rebootBootloader);
        }

        public void flash_Check_ADB_Devices(object sender, RoutedEventArgs e)
        {
            runCommand(getDevices);
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

            }
        }


        //服务激活Grid命令
        public void act_IceBox(object sender, RoutedEventArgs e)
        {
            runCommand(iceBox);
        }

        public void act_Brevent(object sender, RoutedEventArgs e)
        {
            runCommand(brevent);
        }


        public void act_airFrozen(object sender, RoutedEventArgs e)
        {
            runCommand(airFrozen);
        }

        public void act_BlackRoom(object sender, RoutedEventArgs e)
        {
            runCommand(blackRoom);
        }

        //TO-DO: 解决权限不足
        public void act_pmDog(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("该功能未进行充分测试，权限狗可能会返回 \'没有权限\'。", "注意",MessageBoxButton.OK, MessageBoxImage.Exclamation);
            runCommand(permissionDog);
        }

        private void openRepo_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(repoUrl);
        }

        private void openRelease_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(releaseUrl);
        }


        //////There is a logic problem here.
        //The real command runner
        public static void runCommand(string cm)
        {
            if (isVerbose)
            {
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.Arguments = "/k " + cm;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.CreateNoWindow = true;
                process.Start();
                process.WaitForExit();
            }
            else
            {
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.Arguments = "/c " + cm;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.CreateNoWindow = true;
                process.Start();
                process.WaitForExit();
            }
        }

        //Event listeners for verbose mode
        private void verbose_Checked(object sender, RoutedEventArgs e){isVerbose = true;}

        private void verbose_Unchecked(object sender, RoutedEventArgs e){isVerbose = false;}

        private void checkDevice_Checked(object sender, RoutedEventArgs e)
        {
            needsCheckDevice = true;
        }

        private void checkDevice_Unchecked(object sender, RoutedEventArgs e)
        {
            needsCheckDevice = false;
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
    }
}
