using Microsoft.Win32;
using System.Windows;


namespace Helium
{
    /// <summary>
    /// Main page interaction
    /// </summary>
    ///

    public partial class MainWindow : Window
    {

        public static string cmdType = "/k ";
        public static bool mouseEntered = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        //输入框读取项目
        public string readInput()
        {
            return inputField.Text;
        }

        //ADB指令Grid命令
        public void adb_dump_battery_data(object sender, RoutedEventArgs e)
        {
            cmdType = "/k ";
            Func.runCmd("adb shell dumpsys battery");
        }

        public void adb_push_file(object sender, RoutedEventArgs e)
        {
            string targetDir;
            string fileDir;

            OpenFileDialog of = new OpenFileDialog();
            of.Filter = "文件 (*.*)|*.*";
            of.DefaultExt = ".*";

            if ((bool)of.ShowDialog())
            {
                fileDir = of.FileName;
                if (readInput() == "")
                {
                    MessageBox.Show("请输入需要输入的字符：", null, MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                else
                {
                    targetDir = readInput();
                    Func.runCmd("adb push " + fileDir + " " + targetDir);
                }
            }
        }

        public void adb_pull_file(object sender, RoutedEventArgs e)
        {
           
        }

        public void adb_input_text(object sender, RoutedEventArgs e)
        {
            string input;
            if(readInput() == "")
            {
                MessageBox.Show("请输入需要输入的字符：", null, MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                input = readInput();
                Func.runCmd("adb shell input text \"" + input + "\"");
            }
        }

        public void adb_dump_model_data(object sender, RoutedEventArgs e)
        {
            cmdType = "/k ";
            Func.runCmd("adb shell getprop ro.product.model");
        }

        public void adb_take_screenshot(object sender, RoutedEventArgs e)
        {
            Func.runCmd("adb shell screencap -p /sdcard/screenshot.png && adb pull /sdcard/screenshot.png");
        }

        public void adb_install_app(object sender, RoutedEventArgs e)
        {
            string apkDir;
            OpenFileDialog of = new OpenFileDialog();
            of.Filter = ".APK 文件 (*.apk)|*.apk";
            of.DefaultExt = ".apk";

            if ((bool)of.ShowDialog())
            {
                apkDir = of.FileName;
                Func.runCmd("adb install " + apkDir);

            }
        }


        //其他指令Grid命令
        public void others_launch_adb(object sender, RoutedEventArgs e)
        {
            cmdType = "/k ";
            Func.runCmd("");
        }

        public void others_launch_shell(object sender, RoutedEventArgs e)
        {
            cmdType = "/k ";
            Func.runCmd("adb shell");
        }

        public void others_initiate_net_adb(object sender, RoutedEventArgs e)
        {
                if (readInput() == null || readInput() == "")
                {
                    MessageBox.Show("请在程序右上方输入框中输入手机局域网IP地址和TCPIP连接端口 （如：192.168.0.4:5555)", null, MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                else
                {
                    cmdType = "/c ";
                    Func.runCmd("adb devices && adb tcpip 5555");
                    cmdType = "/k ";
                    Func.runCmd("adb connect " + readInput());
                }
        }

        //刷机指令Grid命令
        public void flash_Reboot(object sender, RoutedEventArgs e)
        {
            Func.runCmd("adb reboot");
        }

        public void flash_Reboot_REC(object sender, RoutedEventArgs e)
        {
            Func.runCmd("adb reboot recovery");
        }

        public void flash_Check_Fastboot_Devices(object sender, RoutedEventArgs e)
        {
            Func.runCmd("fastboot devices");
        }

        public void flash_Reboot_BL(object sender, RoutedEventArgs e)
        {
            Func.runCmd("adb reboot bootloader");
        }

        public void flash_Check_ADB_Devices(object sender, RoutedEventArgs e)
        {
            Func.runCmd("adb devices");
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
                Func.runCmd("adb devices && adb reboot bootloader && fastboot devices && fastboot flash recovery " + imgDir + " fastboot reboot-bootloader + fastboot erase cache && fastboot reboot");
                
            }
        }


        //服务激活Grid命令
        public void act_IceBox(object sender, RoutedEventArgs e)
        {
            Func.runCmd("adb shell dpm set-device-owner com.catchingnow.icebox/.receiver.DPMReceiver");
        }

        public void act_Brevent(object sender, RoutedEventArgs e)
        {
            Func.runCmd("adb -d shell sh /data/data/me.piebridge.brevent/brevent.sh");
        }


        public void act_airFrozen(object sender, RoutedEventArgs e)
        {
            Func.runCmd("adb shell dpm set-device-owner me.yourbay.airfrozen/.main.core.mgmt.MDeviceAdminReceiver");
        }

        public void act_BlackRoom(object sender, RoutedEventArgs e)
        {
            Func.runCmd("adb shell dpm set-device-owner web1n.stopapp/.receiver.AdminReceiver");
        }

        //TO-DO: 解决权限不足
        public void act_pmDog(object sender, RoutedEventArgs e)
        {
            Func.runCmd("adb shell sh /storage/emulated/0/Android/data/com.web1n.permissiondog/files/starter.sh");
        }



        //功能警告
        private void warnIcon_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (!mouseEntered)
            {
                warningMessage.Text = "黄色背景的按钮表示该功能尚未完成测试，请谨慎使用！";
                mouseEntered = true;
            }
            else
            {
                warningMessage.Text = "";
                mouseEntered = false;
            }
            
        }
    }

    class Func
    {
        public static void runCmd(string cm)
        {
            System.Diagnostics.Process.Start("cmd.exe", MainWindow.cmdType + cm);
        }
    }
}
