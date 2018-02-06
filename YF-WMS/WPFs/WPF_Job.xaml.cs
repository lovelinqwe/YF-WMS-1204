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
    /// WPS_Job.xaml 的交互逻辑
    /// </summary>
    public partial class WPF_Job : Window
    {
        public WPF_Job()
        {
            ///初始化
            InitializeComponent();
        }

        private void Queue_Add(object sender, RoutedEventArgs e)
        {
            ///弹出队列输入框，数据同步到数据库
        }

        private void Queue_Delete(object sender, RoutedEventArgs e)
        {
            ///删除某一队列数据，同步到数据库
        }

        private void Queue_Select(object sender, RoutedEventArgs e)
        {
            ///根据文本框内容查找对应队列
        }

        private void Queue_Edit(object sender, RoutedEventArgs e)
        {
            ///弹出队列编辑输入框，数据同步到数据库
        }

        private void Queue_Export(object sender, RoutedEventArgs e)
        {
            ///导出队列数据到Excel
        }
    }
}
