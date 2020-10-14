using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helium
{
    class Global
    {
        #region Url 
        public static String repoUrl = "https://github.com/hello-world-404/Helium";
        public static String releaseUrl = "https://github.com/hello-world-404/Helium/releases";

        #endregion

        #region adbCommands
        public static String dumpBattery = "adb shell dumpsys battery";
        public static String getModel = "adb shell getprop ro.product.model";
        public static String takeScreenshot = "adb shell screencap -p /sdcard/screenshot.png && adb pull /sdcard/screenshot.png";
        public static String launchShell = "adb shell";
        public static String launchAdb = "";
        public static String reboot = "adb reboot";
        public static String rebootRec = "adb reboot recovery";
        public static String fastbootCheckDevices = "fastboot devices";
        public static String rebootBootloader = "adb reboot bootloader";
        public static String getDevices = "adb devies";
        public static String iceBox = "adb shell dpm set-device-owner com.catchingnow.icebox/.receiver.DPMReceiver";
        public static String brevent = "adb -d shell sh /data/data/me.piebridge.brevent/brevent.sh";
        public static String airFrozen = "adb shell dpm set-device-owner me.yourbay.airfrozen/.main.core.mgmt.MDeviceAdminReceiver";
        public static String blackRoom = "adb shell dpm set-device-owner web1n.stopapp/.receiver.AdminReceiver";
        public static String permissionDog = "adb shell sh /storage/emulated/0/Android/data/com.web1n.permissiondog/files/starter.sh";
        public static String installPrefix = "adb install ";
        #endregion
    }
}
