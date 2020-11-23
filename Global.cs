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

        #region cultureCodes
        public static String ZH_ROC = "zh-TW";
        public static String ZH_PROC = "zh-CN";
        public static String EN_US = "en-US";
        #endregion

        #region logMsg
        public static String APP_INIT = "Application initialized";
        public static String ON_EXIT = "Application exit";
        public static String RUN_CMD = "Command run";
        public static String DMP_BAT_CLICK = "DUMP BATTERY CLICKED";
        public static String G_MODEL_CLICK = "GET MODEL CLICKED";
        public static String TK_SCSHT_CLICK = "TAKE SCREENSHOT CLICKED";
        public static String L_SHELL_CLICK = "LAUNCHING SHELL";
        public static String L_ADB_CLICK = "LAUNCHING ADB";
        public static String REBOOT_CLICK = "REBOOT CLICKED";
        public static String REBOOT_REC_CLICK = "REBOOT RECOVERY CLICKED";
        public static String FB_CHECK_DEV_CLICK = "FASTBOOT CHECK DEVICES CLICKED";
        public static String REBOOT_BL_CLICK = "REBOOT BOOTLOADER";
        public static String GET_DEV_CLICK = "GET DEVICES";
        public static String ICE_BOX_ACT = "ACTIVATE ICEBOX";
        public static String BREVENT_ACT = "ACTIVATE BREVENT";
        public static String AIRF_ACT = "ACTIVATE AIRFROZEN";
        public static String BR_ACT = "ACTIVATE BLACK ROOM";
        public static String PM_ACT = "ACTIVATE PERMISSION DOG";
        public static String INST_APP_CLICK = "INSTALLING APP";
        public static String CHK_ADB_CLICK = "CHECKING ADB DEVICE";
        public static String FL_REC_IMG = "FLASH RECOVERY IMAGE";
        #endregion

    }
}
