using DatabaseHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace YF_WMS.WPFs
{
    /// <summary>
    /// WPF_Shelf_Manual.xaml 的交互逻辑
    /// </summary>
    public partial class WPF_Shelf_Manual : Window
    {
        WPF_Shelf parentwindow;
        DataRowView parentview;
        string _operation;
        public WPF_Shelf_Manual(WPF_Shelf window, DataRowView view, string operation)
        {
            InitializeComponent();
            parentwindow = window;
            parentview = view;
            _operation = operation;
        }

        private void shelf_manual_determine_Click(object sender, RoutedEventArgs e)
        {
            string sql = "";
            ///手动添加库位数据
            if (_operation.Equals("add"))
            {
                sql = "select * from Box where Box_ID ='" + shelf_id.Text.Trim() + "' and Shelf_Area ='" + shelf_area.Text.Trim() + "' and Shelf_Row ='" + shelf_row.Text.Trim() + "' and Shelf_Column ='" + shelf_column.Text.Trim() + "'";
                DataSet ds = MySQLHelper.GetDataSet(MySQLHelper.GetConn(), CommandType.Text, sql, null);
                ///库位不存在
                if (((ds == null) || (ds.Tables.Count == 0) || (ds.Tables.Count == 1 && ds.Tables[0].Rows.Count == 0)))
                {
                    sql = "insert into Shelf (Shelf_ID,Shelf_Area,Shelf_Row,Shelf_Column,Shelf_Status,Create_Time,Modify_Time) values ('" + shelf_id.Text.Trim() + "','" + shelf_area.Text.Trim() + "','" + shelf_row.Text.Trim() + "','" + shelf_column.Text.Trim() + "','" + shelf_status.Text.Trim() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    MySQLHelper.ExecuteNonQuery(MySQLHelper.GetConn(), CommandType.Text, sql, null);
                }
                ///库位存在
                else
                {
                    MessageBox.Show("库位已存在，请重新输入");
                }
            }
            ///编辑库位数据
            else if (_operation.Equals("edit"))
            {
                sql = "select * from Box where Box_ID ='" + shelf_id.Text.Trim() + "' and Shelf_Area ='" + shelf_area.Text.Trim() + "' and Shelf_Row ='" + shelf_row.Text.Trim() + "' and Shelf_Column ='" + shelf_column.Text.Trim() + "'";
                DataSet ds = MySQLHelper.GetDataSet(MySQLHelper.GetConn(), CommandType.Text, sql, null);
                ///库位不存在
                if (((ds == null) || (ds.Tables.Count == 0) || (ds.Tables.Count == 1 && ds.Tables[0].Rows.Count == 0)))
                {
                    sql = "update Shelf set Shelf_ID='" + shelf_id.Text.Trim() + "',Shelf_Area='" + shelf_area.Text.Trim() + "',Shelf_Row='" + shelf_row.Text.Trim() + "',Shelf_Column='" + shelf_column.Text.Trim() + "',Shelf_Status='" + shelf_status.Text.Trim() + "' Modify_Time = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where Shelf_ID='" + parentview.Row[0].ToString() + "'";
                    MySQLHelper.ExecuteNonQuery(MySQLHelper.GetConn(), CommandType.Text, sql, null);
                }
                ///库位存在
                else
                {
                    MessageBox.Show("库位已存在");
                }
            }

            parentwindow.Shelf_Select(sender, e);
            this.Close();
        }

        private void shelf_manual_cancel_Click(object sender, RoutedEventArgs e)
        {
            ///返回父窗口
            this.Close();
        }
    }
}
