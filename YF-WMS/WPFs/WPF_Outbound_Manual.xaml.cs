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
    /// WPF_Outbound_Manual.xaml 的交互逻辑
    /// </summary>
    public partial class WPF_Outbound_Manual : Window
    {
        WPF_Outbound parentwindow;
        DataRowView parentview;
        string _operation;
        public WPF_Outbound_Manual(WPF_Outbound window, DataRowView view, string operation)
        {
            InitializeComponent();
            parentwindow = window;
            parentview = view;
            _operation = operation;
        }

        private void outbound_manual_cancel_click(object sender, RoutedEventArgs e)
        {
            ///返回父窗口
            ///
            this.Close();
        }

        private void outbound_manual_determine_click(object sender, RoutedEventArgs e)
        {
            string sql = "";
            string insert = "";
            string update = "";
            ///手动添加出库记录
            if (_operation.Equals("add"))
            {
                ///查找物料表物料库存数量大于销售数量的数据集
                sql = "select * from Material where Material_ID ='" + material_id.Text.Trim() + "' and Material_Qty >= " + material_sqty.Text.Trim() ;
                Console.WriteLine(sql);
                DataSet ds = MySQLHelper.GetDataSet(MySQLHelper.GetConn(), CommandType.Text, sql, null);
                ///物料库存数量大于销售数量时
                if (!((ds == null) || (ds.Tables.Count == 0) || (ds.Tables.Count == 1 && ds.Tables[0].Rows.Count == 0)))
                {
                    ///插入出库记录
                    insert = "insert into Outbound (Outbound_Time,Custom_Name,Box_ID,Sale_ID,Material_ID,Material_Spec,Material_SQty,Material_SerialNum,Create_Time,Modify_Time) values ('" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + custom_name.Text.Trim() + "','" + box_id.Text.Trim() + "','" + sale_id.Text.Trim() + "','" + material_id.Text.Trim() + "','" + material_spec.Text.Trim() + "','" + material_sqty.Text.Trim() + "','" + material_serialnum.Text.Trim() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    MySQLHelper.ExecuteNonQuery(MySQLHelper.GetConn(), CommandType.Text, insert, null); 
                    ///更新物料表数据
                    update = "update Material set Material_Qty = Material_Qty - " + material_sqty.Text.Trim() + ",Modify_Time='"+ DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where Material_ID = '" + material_id.Text.Trim() + "'";
                    MySQLHelper.ExecuteNonQuery(MySQLHelper.GetConn(), CommandType.Text, update, null);
                }
                ///物料库存数量少于销售数量时
                else
                {
                    MessageBox.Show("库存不足，请重新填写");
                }
            }
            ///编辑出库记录
            else if (_operation.Equals("edit"))
            {
                ///查找物料表物料库存数量大于销售数量的数据集
                sql = "select * from Material where Material_ID ='" + material_id.Text.Trim() + "' and Material_Qty  + " + parentview.Row[7].ToString() + " - " + material_sqty.Text.Trim() + " >= 0";
                DataSet ds = MySQLHelper.GetDataSet(MySQLHelper.GetConn(), CommandType.Text, sql, null);
                ///物料库存数量大于销售数量时
                if (!((ds == null) || (ds.Tables.Count == 0) || (ds.Tables.Count == 1 && ds.Tables[0].Rows.Count == 0)))
                {
                    ///更新出库记录表数据
                    update = "update Outbound set Custom_Name='" + custom_name.Text.Trim() + "',Sale_ID='" + sale_id.Text.Trim() + "',Box_ID='" + box_id.Text.Trim() + "',Material_ID='" + material_id.Text.Trim() + "',Material_Spec='" + material_spec.Text.Trim() + "',Material_SQty='" + material_sqty.Text.Trim() + "',Material_SerialNum='" + material_serialnum.Text.Trim() + "',Modify_Time='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where Outbound_ID='" + parentview.Row[0].ToString() + "'";
                    MySQLHelper.ExecuteNonQuery(MySQLHelper.GetConn(), CommandType.Text, update, null);
                    ///更新物料表数据
                    sql = "update Material set Material_Qty = Material_Qty + " + parentview.Row[7].ToString() + " - " + material_sqty.Text.Trim() + ",Modify_Time='"+ DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where Material_ID = '" + material_id.Text.Trim() + "'";
                    MySQLHelper.ExecuteNonQuery(MySQLHelper.GetConn(), CommandType.Text, sql, null);
                }
                ///物料库存数量少于销售数量时
                else
                {
                    MessageBox.Show("库存不足，请重新填写");
                }
            }
            parentwindow.Select(sender, e);
            this.Close();
        }
    }
}
