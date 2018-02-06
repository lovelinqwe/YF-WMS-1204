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

namespace YF_WMS
{
    /// <summary>
    /// WPF_Main.xaml 的交互逻辑
    /// </summary>
    public partial class WPF_Main : Window
    {
        public WPF_Main()
        {
            ///初始化
           ///第三次
            InitializeComponent();
        }

        private void menuAbout_Click(object sender, RoutedEventArgs e)
        {
            WPF_About wpf_about = new WPF_About();
            wpf_about.ShowDialog();
        }

        private void menuExit_Click(object sender, RoutedEventArgs e)
        {
            ///突出主程序
            if (MessageBox.Show("你确定要退出吗？", "退出 YF-WMS", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                Application.Current.Shutdown();
            }
        }

        private void Outbound_Show(object sender, RoutedEventArgs e)
        {
            ///显示出库管理界面
            ///
            mainFrame.NavigationService.Navigate(new Uri("WPFs/WPF_Outbound.xaml", UriKind.Relative));
        }

        private void Inbound_Show(object sender, RoutedEventArgs e)
        {
            ///显示入库管理界面
            ///
            mainFrame.NavigationService.Navigate(new Uri("WPFs/WPF_Inbound.xaml", UriKind.Relative));
        }

        private void Storage_Show(object sender, RoutedEventArgs e)
        {
            ///显示库存管理界面
            mainFrame.NavigationService.Navigate(new Uri("WPFs/WPF_Stock.xaml", UriKind.Relative));
        }

        private void Board_Show(object sender, RoutedEventArgs e)
        {
            ///显示看板界面
        }
    }
}
