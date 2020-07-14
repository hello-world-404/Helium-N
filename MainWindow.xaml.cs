using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace Helium
{
    public partial class MainWindow : Window
    {
        public static string commandType = "/k ";
        private static string pacName;
        private static string curDir = Directory.GetCurrentDirectory() + "\\";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void reboot(object sender, MouseButtonEventArgs e)
        {
            run("adb reboot");
        }

        private void rebootToRec(object sender, MouseButtonEventArgs e)
        {
            run("adb reboot recovery");
        }
        private void rebootToBL(object sender, MouseButtonEventArgs e)
        {
            run("adb reboot bootloader");
        }
        private void shutdown(object sender, MouseButtonEventArgs e)
        {
            run("adb shutdown");
        }

        private void checkFastbootDevices(object sender, MouseButtonEventArgs e)
        {
            run("fastboot devices");
        }

        private void checkADBDevices(object sender, MouseButtonEventArgs e)
        {
            run("adb devices");
        }

        private void getModelNumber(object sender, MouseButtonEventArgs e)
        {
            run("adb shell getprop ro.product.model");
        }

        private void getSysInfo(object sender, MouseButtonEventArgs e)
        {
            run("adb shell getprop ro.build.version.release");
        }

        private void installApk(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            of.Filter = "APK 文件 (*.apk)|*.apk";
            of.Title = "选择要安装的APK文件";

            if ((bool)of.ShowDialog())
            {
                string pacName = of.FileName;
                if (pacName != null)
                {
                    run("adb install " + pacName);
                }
                else
                {
                    MessageBox.Show("糟糕！", "你未选择APK文件！", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
        }

        private void uninstallApk(object sender, MouseButtonEventArgs e)
        {
            run("adb uninstall " + pacName);
        }

        private void takeScreenshot(object sender, MouseButtonEventArgs e)
        {
            run("adb shell screencap -p /sdcard/screenshot.png");
            System.Threading.Thread.Sleep(2000);
            run("adb pull /sdcard/screenshot.png");
        }

        private void recordScreen(object sender, MouseButtonEventArgs e)
        {
            run("adb shell screenrecord /sdcard/record.mp4");
        }

        private void getBatteryInfo(object sender, MouseButtonEventArgs e)
        {
            run("adb shell dumpsys battery");
        }

        private void run(string cm)
        {
            checkVerbose();
            System.Diagnostics.Process.Start("cmd.exe", commandType + cm);
        }

        private void launchADB(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("cmd.exe");
        }

        private void launchShell(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("cmd.exe", "/k adb shell");
        }

        private void inputBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (pacName == null)
                {
                    MessageBox.Show("你未输入任何东西！", "错误！", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                else
                {
                    pacName = inputBox.Text;
                }
            }
        }


        private void installLoops(object sender, MouseButtonEventArgs e)
        {
            run("adb install " + curDir + "\\apk\\loops.apk");
        }
        /*
        private void enablePillNavigation(object sender, MouseButtonEventArgs e)
        {
            run("adb shell getprop ro.build.version.release");
        }
        private void enableLoopsClock(object sender, MouseButtonEventArgs e)
        {
            run("adb shell getprop ro.build.version.release");
        }
        private void enableWhiteTheme(object sender, MouseButtonEventArgs e)
        {
            run("adb shell getprop ro.build.version.release");
        }
        */

        private void launchAuthServerPage(object sender, MouseButtonEventArgs e)
        {
            switchAuthServerSplash sp = new switchAuthServerSplash();
            sp.Show();
        }

        private void checkVerbose()
        {
            if ((bool)verbose.IsChecked)
            {
                commandType = "/k ";
            }
            else
            {
                commandType = "/c ";
            }
        }


        private void actAirFrozen(object sender, MouseButtonEventArgs e)
        {
            run("adb shell dpm set-device-owner me.yourbay.airfrozen/.main.core.mgmt.MDeviceAdminReceiver");
        }
        private void actIceBox(object sender, MouseButtonEventArgs e)
        {
            run("adb shell dpm set-device-owner com.catchingnow.icebox/.receiver.DPMReceiver");
        }
        private void actShizuku(object sender, MouseButtonEventArgs e)
        {
            run("adb shell sh /data/user_de/0/moe.shizuku.privileged.api/start.sh");
        }
        private void actBrevent(object sender, MouseButtonEventArgs e)
        {
            run("adb -d shell sh /data/data/me.piebridge.brevent/brevent.sh ");
        }
        private void actBlackRoom(object sender, MouseButtonEventArgs e)
        {
            run("adb shell dpm set-device-owner web1n.stopapp/.receiver.AdminReceiver");
        }

        private void aboutMe_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/hello-world-404");
        }
    }
}
