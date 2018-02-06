using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using YF_WMS.WPFs;

namespace YF_WMS
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// liuzhaolin
        /// aaaaaa
        /// HJtest
        /// bbbbbb
        /// not saved
        /// 2 diff files changing together
        /// </summary>
        public MainWindow()
        {
            ///git
            InitializeComponent();
           

        }
        private void WriteSome()
        {
            Console.WriteLine("11111111111");
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WPF_Login wpf_login = new WPF_Login();
            wpf_login.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            WPF_Connection wpf_connection = new WPF_Connection();
            wpf_connection.ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            WPF_Main wpf_main = new WPF_Main();
            wpf_main.ShowDialog();
        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            WPF_About wpf_about = new WPF_About();
            wpf_about.ShowDialog();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            aaa aa = new aaa();
            aa.ShowDialog();

        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            WPF_PO wpf_po = new WPF_PO();
            wpf_po.ShowDialog();
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            WPF_Outbound wpf_outbound = new WPF_Outbound();
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
           
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            WPF_Shelf wpf_shelf = new WPF_Shelf();
            wpf_shelf.ShowDialog();
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            WPF_Box wpf_box = new WPF_Box();
            wpf_box.ShowDialog();
        }

        private void Button_Click_10(object sender, RoutedEventArgs e)
        {
            WPF_User wpf_user = new WPF_User();
            wpf_user.ShowDialog();
        }

        private void Button_Click_11(object sender, RoutedEventArgs e)
        {
            WPF_Role wpf_role = new WPF_Role();
            wpf_role.ShowDialog();
        }

        private void Button_Click_12(object sender, RoutedEventArgs e)
        {
            WPF_SO wpf_so = new WPF_SO();
            wpf_so.ShowDialog();
        }

        private void Button_Click_13(object sender, RoutedEventArgs e)
        {
            WPF_Material wpf_material = new WPF_Material();
            wpf_material.ShowDialog();
        }

        private void Button_Click_14(object sender, RoutedEventArgs e)
        {
            WPF_ERP_Interface wpf_erp_interface = new WPF_ERP_Interface();
            wpf_erp_interface.ShowDialog();
        }

        private void Button_Click_15(object sender, RoutedEventArgs e)
        {
            WPF_Kanban wpf_kanban = new WPF_Kanban();
            wpf_kanban.Show();
        }

        private void Button_Click_16(object sender, RoutedEventArgs e)
        {
            WPF_Job wpf_job = new WPF_Job();
            wpf_job.ShowDialog();
        }


    }
}
