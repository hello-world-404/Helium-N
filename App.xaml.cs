using System.Windows;

namespace Helium
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// 诱骗语言至英语，测试Resources.en.resx
        /// </summary>
        /// <param name="e">Cheats program to use Resources.en.resx</param>
        /*
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");
        }
        */
    }
}
