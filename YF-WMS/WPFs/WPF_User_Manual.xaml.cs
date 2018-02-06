using DatabaseHelper;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace YF_WMS.WPFs
{
    /// <summary>
    /// WPF_User_Add.xaml 的交互逻辑
    /// </summary>
    public partial class WPF_User_Manual : Window
    {
        WPF_User parentwindow;
        DataRowView parentview;
        string _operation;
        public WPF_User_Manual(WPF_User window, DataRowView view, string operation)
        {
            InitializeComponent();
            parentwindow = window;
            parentview = view;
            _operation = operation;
        }

        private void user_manual_determine_Click(object sender, RoutedEventArgs e)
        {
            ///用户添加，数据同步到数据库
            ///
            string sql = "";
            if (_operation.Equals("add"))
            {
                sql = "select * from User where Username ='" + username.Text.Trim() + "' and Password ='" + password.Password.Trim() + "'";
                DataSet ds = MySQLHelper.GetDataSet(MySQLHelper.GetConn(), CommandType.Text, sql, null);
                if (!((ds == null) || (ds.Tables.Count == 0) || (ds.Tables.Count == 1 && ds.Tables[0].Rows.Count == 0)))
                {
                    sql = "insert into User (Username,Password,Create_Time,Modify_Time) values ('" + username.Text.Trim() + "','" + password.Password.Trim() +"','"+ DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +"','"+ DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    MySQLHelper.ExecuteNonQuery(MySQLHelper.GetConn(), CommandType.Text, sql, null);
                }
                else
                {
                    MessageBox.Show("用户已存在，请重新输入");
                }
            }

            else if (_operation.Equals("edit"))
            {
                sql = "select * from User where Username ='" + username.Text.Trim() + "' and Password ='" + password.Password.Trim() + "'";
                Console.WriteLine(sql);
                DataSet ds = MySQLHelper.GetDataSet(MySQLHelper.GetConn(), CommandType.Text, sql, null);
                if (((ds == null) || (ds.Tables.Count == 0) || (ds.Tables.Count == 1 && ds.Tables[0].Rows.Count == 0)))
                {
                    sql = "update User set Username='" + username.Text.Trim() + "',Password='" + password.Password.Trim() + "',Modify_Time='"+ DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where User_ID='" + parentview.Row[0].ToString() + "'";
                    MySQLHelper.ExecuteNonQuery(MySQLHelper.GetConn(), CommandType.Text, sql, null);
                }
                else
                {
                    MessageBox.Show("用户已存在");
                }
            }

            parentwindow.User_Select(sender, e);
            this.Close();
        }

        private void user_manual_cancel_Click(object sender, RoutedEventArgs e)
        {
            ///返回父窗口
            ///
            this.Close();
        }
    }
}
