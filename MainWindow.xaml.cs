using Microsoft.Win32;
using System.Diagnostics;
using System.Windows;


namespace Helium
{

    public partial class MainWindow : Window
    {
        private static bool isVerbose;

        public MainWindow()
        {
            InitializeComponent();
        }


        //ADB指令Grid命令
        public void adb_dump_battery_data(object sender, RoutedEventArgs e)
        {
            
            runCmd("adb shell dumpsys battery");
        }


        public void adb_dump_model_data(object sender, RoutedEventArgs e)
        {
            
            runCmd("adb shell getprop ro.product.model");
        }

        public void adb_take_screenshot(object sender, RoutedEventArgs e)
        {
            runCmd("adb shell screencap -p /sdcard/screenshot.png && adb pull /sdcard/screenshot.png");
        }

        public void adb_install(object sender, RoutedEventArgs e)
        {
            string apkDir;
            OpenFileDialog vf = new OpenFileDialog();
            vf.Filter = ".APK 文件 (*.apk) | *.apk";
            vf.DefaultExt = ".apk";

            if ((bool)vf.ShowDialog())
            {
                apkDir = vf.FileName;
                runCmd("adb install" + apkDir);
            }
        }

        //其他指令Grid命令
        public void others_launch_adb(object sender, RoutedEventArgs e)
        {
            runCmd("");
        }

        public void others_launch_shell(object sender, RoutedEventArgs e)
        {
            runCmd("adb shell");
        }

        //刷机指令Grid命令
        public void flash_Reboot(object sender, RoutedEventArgs e)
        {
            runCmd("adb reboot");
        }

        public void flash_Reboot_REC(object sender, RoutedEventArgs e)
        {
            runCmd("adb reboot recovery");
        }

        public void flash_Check_Fastboot_Devices(object sender, RoutedEventArgs e)
        {
            runCmd("fastboot devices");
        }

        public void flash_Reboot_BL(object sender, RoutedEventArgs e)
        {
            runCmd("adb reboot bootloader");
        }

        public void flash_Check_ADB_Devices(object sender, RoutedEventArgs e)
        {
            runCmd("adb devices");
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
                runCmd("adb devices && adb reboot bootloader && fastboot devices && fastboot flash recovery " + imgDir + " fastboot reboot-bootloader + fastboot erase cache && fastboot reboot");
                
            }
        }


        //服务激活Grid命令
        public void act_IceBox(object sender, RoutedEventArgs e)
        {
            runCmd("adb shell dpm set-device-owner com.catchingnow.icebox/.receiver.DPMReceiver");
        }

        public void act_Brevent(object sender, RoutedEventArgs e)
        {
            runCmd("adb -d shell sh /data/data/me.piebridge.brevent/brevent.sh");
        }


        public void act_airFrozen(object sender, RoutedEventArgs e)
        {
            runCmd("adb shell dpm set-device-owner me.yourbay.airfrozen/.main.core.mgmt.MDeviceAdminReceiver");
        }

        public void act_BlackRoom(object sender, RoutedEventArgs e)
        {
            runCmd("adb shell dpm set-device-owner web1n.stopapp/.receiver.AdminReceiver");
        }

        //TO-DO: 解决权限不足
        public void act_pmDog(object sender, RoutedEventArgs e)
        {
            runCmd("adb shell sh /storage/emulated/0/Android/data/com.web1n.permissiondog/files/starter.sh");
        }

        private void openRepo_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://github.com/hello-world-404/Helium");
        }

        private void openRelease_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://github.com/hello-world-404/Helium/releases");
        }

        public static void runCmd(string cm)
        {
            if (isVerbose)
            {
                Process.Start("cmd", "/k " + cm);
            }
            else
            {
                Process.Start("cmd", "/c " + cm);
            }
        }

        private void verbose_Click(object sender, RoutedEventArgs e)
        {
            if (!isVerbose)
            {
                isVerbose = true;
                verbose.Content = "Verbose Off";
            }
            else
            {
                isVerbose = false;
                verbose.Content = "Verbose on";
            }
        }
    }
}
