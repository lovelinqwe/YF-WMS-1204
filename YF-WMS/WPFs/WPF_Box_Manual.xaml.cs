using DatabaseHelper;
using MySql.Data.MySqlClient;
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
    /// WPF_Box_Manual.xaml 的交互逻辑
    /// </summary>
    public partial class WPF_Box_Manual : Window
    {
        private string sql;
        private WPF_Box parentwindow;
        string _operation;
        DataRowView parentview;
        public WPF_Box_Manual(WPF_Box window, DataRowView view, string operation)
        {
            InitializeComponent();
            parentwindow = window;
            _operation = operation;
            parentview = view;
        }

        private void box_manual_determine(object sender, RoutedEventArgs e)
        {
            if (_operation.Equals("add"))
            {
                try
                {
                    sql = "select * from Shelf where Shelf_ID ='" + shelf_id.Text.Trim() + "' and Shelf_Status='0'";
                    DataSet ds = MySQLHelper.GetDataSet(MySQLHelper.GetConn(), CommandType.Text, sql, null);
                    if ((ds == null) || (ds.Tables.Count == 0) || (ds.Tables.Count == 1 && ds.Tables[0].Rows.Count == 0))
                    {
                        Console.WriteLine("dataset为空");
                        MessageBox.Show("库位编号不存在或库位已存放料箱，请重新编辑");
                    }
                    else
                    {
                        sql = "insert into Box (Box_ID,Box_Color,Box_Capacity,Box_Status,Box_Desc,Shelf_ID,Create_Time,Modify_Time) values ('" + box_id.Text.Trim() + "','" + box_color.Text.Trim() + "','" + box_capacity.Text.Trim() + "','" + box_status.Text.Trim() + "','" + box_desc.Text.Trim() + "','" + shelf_id.Text.Trim() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        MySQLHelper.ExecuteNonQuery(MySQLHelper.GetConn(), CommandType.Text, sql, null);
                        sql = "update Shelf set Shelf_Status='1',Modify_Time = '"+ DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where Shelf_ID='" + shelf_id.Text.Trim() + "'";
                        MySQLHelper.ExecuteNonQuery(MySQLHelper.GetConn(), CommandType.Text, sql, null);
                        parentwindow.Box_Select(sender, e);
                        this.Close();
                    }
                }
                catch (MySqlException)
                {
                    MessageBox.Show("库位编号不存在或库位已存放料箱，请重新编辑");
                }
            }
            else if (_operation.Equals("edit"))
            {
                try
                {
                    if (!parentview.Row[5].ToString().Equals(shelf_id.Text.Trim()))///库位编号已改变
                    {
                        sql = "select * from Shelf where Shelf_ID ='" + shelf_id.Text.Trim() + "' and Shelf_Status ='0'";
                        Console.WriteLine(sql);
                        DataSet ds = MySQLHelper.GetDataSet(MySQLHelper.GetConn(), CommandType.Text, sql, null);
                        if ((ds == null) || (ds.Tables.Count == 0) || (ds.Tables.Count == 1 && ds.Tables[0].Rows.Count == 0))
                        {
                            Console.WriteLine("dataset为空");
                            MessageBox.Show("库位编号不存在或库位已存放料箱，请重新编辑");
                        }
                        else
                        {
                            Console.WriteLine("dataset不为空");
                            sql = "update Box set Box_Color='" + box_color.Text.Trim() + "',Box_Capacity='" + box_capacity.Text.Trim() + "',Box_Status='" + box_status.Text.Trim() + "',Box_Desc='" + box_desc.Text.Trim() + "',Storage_ID='" + shelf_id.Text.Trim() + "',Modify_Time='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where Box_ID='" + box_id.Text.Trim() + "'";
                            MySQLHelper.ExecuteNonQuery(MySQLHelper.GetConn(), CommandType.Text, sql, null);
                            sql = "update Shelf set Shelf_Status='0' ,Modify_Time='"+ DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where Shelf_ID='" + parentview.Row[5].ToString() + "'";
                            MySQLHelper.ExecuteNonQuery(MySQLHelper.GetConn(), CommandType.Text, sql, null);
                            sql = "update Shelf set Shelf_Status='1',Modify_Time='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where Shelf_ID='" + shelf_id.Text.Trim() + "'";
                            MySQLHelper.ExecuteNonQuery(MySQLHelper.GetConn(), CommandType.Text, sql, null);
                            sql = "update Material set Box_ID='" + box_id.Text.Trim() + "',Modify_Time='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where Box_ID='" + parentview.Row[0].ToString() + "'";
                            MySQLHelper.ExecuteNonQuery(MySQLHelper.GetConn(), CommandType.Text, sql, null);
                        }
                    }
                    else
                    {
                        sql = "update Box set Box_Color='" + box_color.Text.Trim() + "',Box_Capacity='" + box_capacity.Text.Trim() + "',Box_Status='" + box_status.Text.Trim() + "',Box_Desc='" + box_desc.Text.Trim() + "',Box_ID='" + box_id.Text.Trim() + "',Modify_Time='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where Box_ID='" + parentview.Row[0].ToString() + "'";
                        MySQLHelper.ExecuteNonQuery(MySQLHelper.GetConn(), CommandType.Text, sql, null);

                        sql = "update Material set Box_ID='" + box_id.Text.Trim() + "',Modify_Time = '"+ DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where Box_ID='" + parentview.Row[0].ToString() + "'";
                        MySQLHelper.ExecuteNonQuery(MySQLHelper.GetConn(), CommandType.Text, sql, null);
                    }
                    parentwindow.Box_Select(sender, e);
                    this.Close();
                }
                catch (MySqlException)
                {
                    MessageBox.Show("库位编号不存在或库位已存放料箱，请重新编辑");
                }

            }

        }

        private void box_manual_cancel(object sender, RoutedEventArgs e)
        {
            ///关闭窗口，返回父窗口
            this.Close();
        }
    }
}
