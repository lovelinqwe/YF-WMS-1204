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
    /// WPF_Role.xaml 的交互逻辑
    /// </summary>
    public partial class WPF_Role : Window
    {
        public WPF_Role()
        {   
            ///初始化
            InitializeComponent();
        }

        private void Role_Add(object sender, RoutedEventArgs e)
        {
            ///弹出角色输入框，数据同步到数据库
        }

        private void Role_Delete(object sender, RoutedEventArgs e)
        {
            ///删除某一个角色，数据库同步
        }

        private void Role_Select(object sender, RoutedEventArgs e)
        {
            ///根据文本框内容查找对应角色
        }

        private void Role_Edit(object sender, RoutedEventArgs e)
        {
            ///弹出角色编辑输入框，数据同步到数据库
        }

        private void Role_Export(object sender, RoutedEventArgs e)
        {
            ///导出列表的角色数据到Excel
        }
    }
}
