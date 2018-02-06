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
    /// WPF_About.xaml 的交互逻辑
    /// </summary>
    public partial class WPF_About : Window
    {
        public WPF_About()
        {
            InitializeComponent();
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            ///关闭窗口，返回父（前）窗口
            this.Close();
        }
    }
}
