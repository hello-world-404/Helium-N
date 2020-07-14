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
using System.Windows.Shapes;

namespace Helium
{
    public partial class switchAuthServerSplash : Window
    {
        private static string httpAuthServer;
        private static string httpsAuthServer;
        public switchAuthServerSplash()
        {
            InitializeComponent();
        }

        private void authXiaomi(object sender, RoutedEventArgs e)
        {
            httpAuthServer = "http://connect.rom.miui.com/generate_204";
            httpsAuthServer = "https://connect.rom.miui.com/generate_204";
            changeAuth();

        }

        private void authV2ex(object sender, RoutedEventArgs e)
        {
            httpAuthServer = "http://captive.v2ex.co/generate_204";
            httpsAuthServer = "https://captive.v2ex.co/generate_204";
            changeAuth();
        }

        private void changeAuth()
        {
            if ((bool)http.IsChecked)
            {
                System.Diagnostics.Process.Start("cmd.exe", MainWindow.commandType + "adb shell settings delete global captive_portal_http_url");
                System.Diagnostics.Process.Start("cmd.exe", MainWindow.commandType + "adb shell settings put global captive_portal_http_url " + httpAuthServer);
            }
            else if ((bool)https.IsChecked)
            {
                System.Diagnostics.Process.Start("cmd.exe", MainWindow.commandType + "adb shell settings delete global captive_portal_https_url");
                System.Diagnostics.Process.Start("cmd.exe", MainWindow.commandType + "adb shell settings put global captive_portal_https_url " + httpsAuthServer);
            }
            else if((bool)https.IsChecked && (bool)http.IsChecked)
            {
                System.Diagnostics.Process.Start("cmd.exe", MainWindow.commandType + "adb shell settings delete global captive_portal_http_url");
                System.Diagnostics.Process.Start("cmd.exe", MainWindow.commandType + "adb shell settings put global captive_portal_http_url " + httpAuthServer);
                System.Diagnostics.Process.Start("cmd.exe", MainWindow.commandType + "adb shell settings delete global captive_portal_https_url");
                System.Diagnostics.Process.Start("cmd.exe", MainWindow.commandType + "adb shell settings put global captive_portal_https_url " + httpsAuthServer);
            }
            else
            {
                MessageBox.Show("未选择连接方式！", "错误！", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            
        }
    }
}
