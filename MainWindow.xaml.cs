using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.VisualBasic;
using Microsoft.Win32;

namespace Helium
{
    public partial class MainWindow : Window
    {
        private static string commandType = "/k ";
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
                if(pacName != null)
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
            
        }

        private void pushFile(object sender, MouseButtonEventArgs e)
        {
            run("adb reboot");
        }

        private void takeScreenshot(object sender, MouseButtonEventArgs e)
        {
            run("adb reboot");
        }

        private void recordScreen(object sender, MouseButtonEventArgs e)
        {
            run("adb reboot");
        }

        private void getBatteryInfo(object sender, MouseButtonEventArgs e)
        {
            run("adb shell dumpsys battery");
        }

        private void run(string cm)
        {
            System.Diagnostics.Process.Start("cmd.exe", commandType + cm);
        }
    }
}
