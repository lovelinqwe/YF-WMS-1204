using DatabaseHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
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
using YF_WMS;

namespace YF_WMS
{
    /// <summary>
    /// WPF_Login.xaml 的交互逻辑
    /// </summary>
    public partial class WPF_Login : Window
    {
        public WPF_Login()
        {
           InitializeComponent();
        }

        private void DBConfig_Set(object sender, RoutedEventArgs e)
        {
            ///弹出数据库连接设置
            WPF_Connection wpf_connection = new WPF_Connection();
            wpf_connection.ShowDialog();
        }

        private void User_Login(object sender, RoutedEventArgs e)
        {
            ///查询Username && Password 是否存在记录在表User
            ///是则Close > WPF_Main
            ///否则提示错误
            ///
            string sql = "select * from User where Username ='"+ username.Text.Trim()+ "' and Password='"+password.Password.Trim()+"'";
            DataSet ds = MySQLHelper.GetDataSet(MySQLHelper.GetConn(), CommandType.Text, sql, null);
            if (!((ds == null) || (ds.Tables.Count == 0) || (ds.Tables.Count == 1 && ds.Tables[0].Rows.Count == 0)))
            {
                MessageBox.Show("登录成功！");
                this.Close();
                WPF_Main main = new WPF_Main();
                main.Show();
            }
            else
            {
                MessageBox.Show("登录失败");
            }   
        }
    }
}
