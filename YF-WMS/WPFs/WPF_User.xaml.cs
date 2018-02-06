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

namespace YF_WMS
{
    /// <summary>
    /// WPF_User.xaml 的交互逻辑
    /// </summary>
    public partial class WPF_User : Window
    {
        DataRowView view = null;
        public WPF_User()
        {
            InitializeComponent();
        }

        private void User_Add(object sender, RoutedEventArgs e)
        {
            ///弹出用户添加输入框，添加用户记录到数据库
            ///
            WPFs.WPF_User_Manual wPF_User_Add = new WPFs.WPF_User_Manual(this,view,"add");
            wPF_User_Add.ShowDialog();
        }

        private void Menu_Delete(object sender, RoutedEventArgs e)
        {
            ///从数据库删除用户记录
            ///
            try
            {
                DataRowView mySelectedElement = (DataRowView)datagriduser.SelectedItem;
                string user_id = mySelectedElement.Row[0].ToString();
                MySQLHelper.ExecuteNonQuery(MySQLHelper.GetConn(), CommandType.Text, "delete  from  User where User_ID like '%" + user_id + "%'", null);
                User_Select(sender, e);
            }
            catch (InvalidCastException)
            {
                MessageBox.Show("导入的excel不能删除");
            }
        }

        private void Menu_Edit(object sender, RoutedEventArgs e)
        {
            ///弹出用户编辑修改框，修改数据库用户记录
            ///
            try
            {
                DataRowView mySelectedElement = (DataRowView)datagriduser.SelectedItem;
                string user_id = mySelectedElement.Row[0].ToString();
                string username = mySelectedElement.Row[1].ToString();
                string password = mySelectedElement.Row[2].ToString();
                WPFs.WPF_User_Manual wPF_User_Manual = new WPFs.WPF_User_Manual(this, mySelectedElement,"edit");
                wPF_User_Manual.password.Password = password;
                wPF_User_Manual.username.Text = username;
                wPF_User_Manual.Title = "用户编辑";
                wPF_User_Manual.ShowDialog();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("未选中编辑行");
            }
            catch (InvalidCastException)
            {
                MessageBox.Show("导入的excel不能编辑");
            }
        }

        private void User_Export(object sender, RoutedEventArgs e)
        {
            ///输出用户记录到excel
            ///    
            System.Windows.Controls.DataGrid DataGrid = datagriduser;
            ExportExcel.Export(DataGrid);
        }

        public void User_Select(object sender, RoutedEventArgs e)
        {
            ///select语句模拟查找用户名
            ///
            string select = selectuser.Text.Trim();
            DataSet ds;
            if (select.Equals(""))
            {
                ds = MySQLHelper.GetDataSet(MySQLHelper.GetConn(), CommandType.Text, "Select * from  User", null);
            }
            else
            {
                ds = MySQLHelper.GetDataSet(MySQLHelper.GetConn(), CommandType.Text, "Select * from  User where Username='" + select + "'", null);
            }
            DataTable dt = ds.Tables[0];
            datagriduser.ItemsSource = dt.DefaultView;
        }

        private void User_Input(object sender, RoutedEventArgs e)
        {
            ///导入excel到datagrid
            ///
            DataTable dt = InputExcel.GetTable();
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    List<Model.User> lists = new List<Model.User>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Model.User u = new Model.User();
                        u.Username = dt.Rows[i][0].ToString();         
                        u.Password = dt.Rows[i][1].ToString();
                        lists.Add(u);
                    }
                    datagriduser.ItemsSource = lists;
                }
            }
            else
            {
                MessageBox.Show("excel表格式错误，请重新导入");
            }
        }
    }
    }