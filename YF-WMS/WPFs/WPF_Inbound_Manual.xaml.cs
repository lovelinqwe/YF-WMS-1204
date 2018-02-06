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
    /// WPF_Inbound_Manual.xaml 的交互逻辑
    /// </summary>
    public partial class WPF_Inbound_Manual : Window
    {
        WPF_Inbound parentwindow;
        DataRowView parentview;
        string _operation;
        public WPF_Inbound_Manual(WPF_Inbound window, DataRowView view,string operation)
        {
            InitializeComponent();
            parentwindow = window;
            parentview = view;
            _operation = operation;
            string sql = "select Box_ID from Box where Box_Capacity < 100";
            DataSet ds = MySQLHelper.GetDataSet(MySQLHelper.GetConn(), CommandType.Text, sql, null);
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                box_id.Items.Add(row["Box_ID"].ToString()); 

            }
            box_id.Items.Add("托板");
        }

        private void inbound_manual_determine_click(object sender, RoutedEventArgs e)
        {
            ///手动添加或修改入库记录，数据同步到数据库
            ///
            string sql="";
            ///手动添加入库记录
            if (_operation.Equals("add"))
            {
                ///插入入库记录
                sql = "insert into Inbound (Inbound_Time,Supplier_Name,Box_ID,Purchase_ID,Material_ID,Material_Spec,Material_PQty,Material_SerialNum,Create_Time,Modify_Time) values ('" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + supplier_name.Text.Trim() + "','" + box_id.Text.Trim() + "','" + purchase_id.Text.Trim() + "','" + material_id.Text.Trim() + "','" + material_spec.Text.Trim() + "','" + material_pqty.Text.Trim() + "','" + material_serialnum.Text.Trim() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";      
                MySQLHelper.ExecuteNonQuery(MySQLHelper.GetConn(), CommandType.Text, sql, null);
                sql = "select * from Material where Material_ID ='" + material_id.Text.Trim() + "' and Box_ID='"+ box_id.Text.Trim() + "'"; 
                DataSet ds = MySQLHelper.GetDataSet(MySQLHelper.GetConn(), CommandType.Text, sql, null);
                ///物料存在或者在同一料箱中
                if (!((ds == null) || (ds.Tables.Count == 0) || (ds.Tables.Count == 1 && ds.Tables[0].Rows.Count == 0)))
                {
                    sql = "update Material set Material_Qty = Material_Qty + " + material_pqty.Text.Trim()+ ",Modify_Time = '"+ DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where Material_ID = '" + material_id.Text.Trim() + "' and Box_ID='" + box_id.Text.Trim() + "'";     
                    MySQLHelper.ExecuteNonQuery(MySQLHelper.GetConn(), CommandType.Text, sql, null);
                }
                ///物料不存在或者不在同一料箱中
                else
                {
                    sql = "insert into  Material (Material_ID,Material_Spec,Material_Qty,Box_ID,Create_Time,Modify_Time) values ('" + material_id.Text.Trim() + "','" + material_spec.Text.Trim() + "','" + material_pqty.Text.Trim() + "','" + box_id.Text.Trim() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    MySQLHelper.ExecuteNonQuery(MySQLHelper.GetConn(), CommandType.Text, sql, null);
                }
            }
            ///编辑入库记录
            else if (_operation.Equals("edit"))
            {
                sql = "select * from Material where Material_ID = '" + material_id.Text.Trim()+"'and Box_ID = '"+box_id.Text.Trim()+ "' and Material_Qty - " +parentview.Row[7].ToString() + " + " +material_pqty.Text.Trim() +" >=0 ";
                Console.WriteLine(sql);
                DataSet ds = MySQLHelper.GetDataSet(MySQLHelper.GetConn(), CommandType.Text, sql, null);
                if (!((ds == null) || (ds.Tables.Count == 0) || (ds.Tables.Count == 1 && ds.Tables[0].Rows.Count == 0)))
                {
                    ///更新入库记录
                    sql = "update Inbound set Supplier_Name='" + supplier_name.Text.Trim() + "',Purchase_ID='" + purchase_id.Text.Trim() + "',Box_ID='" + box_id.Text.Trim() + "',Material_ID='" + material_id.Text.Trim() + "',Material_Spec='" + material_spec.Text.Trim() + "',Material_PQty='" + material_pqty.Text.Trim() + "',Material_SerialNum='" + material_serialnum.Text.Trim() + "',Modify_Time='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where Inbound_ID='" + parentview.Row[0].ToString() + "'";
                    MySQLHelper.ExecuteNonQuery(MySQLHelper.GetConn(), CommandType.Text, sql, null);
                    ///更新物料表
                    sql = "update Material set Material_Qty = Material_Qty - " + parentview.Row[7].ToString() + " + " + material_pqty.Text.Trim() + ",Modify_Time='"+ DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where Material_ID = '" + material_id.Text.Trim() + "' and Box_ID='"+box_id.Text.Trim()+"'";
                    MySQLHelper.ExecuteNonQuery(MySQLHelper.GetConn(), CommandType.Text, sql, null);
                }
                else
                {
                    MessageBox.Show("库存不足，请重新修改入库数量");
                }
              
            }
          
            parentwindow.Select(sender, e);
            this.Close();
        }

        private void inbound_manual_cancel_click(object sender, RoutedEventArgs e)
        {
            ///返回父窗口
            ///
            this.Close();
        }

        private void Material_id_determine(object sender, RoutedEventArgs e)
        {
            string sql = "select Material_Spec from Material where Material_ID = '"+material_id.Text.Trim()+"'";
            DataSet ds = MySQLHelper.GetDataSet(MySQLHelper.GetConn(), CommandType.Text, sql, null);
            if (!((ds == null) || (ds.Tables.Count == 0) || (ds.Tables.Count == 1 && ds.Tables[0].Rows.Count == 0)))
            {
                material_spec.Text = ds.Tables[0].Rows[0]["Material_Spec"].ToString();
            }
            else
            {
                material_spec.Text = "";
            }
        }
        private void box_id_determine(object sender, RoutedEventArgs e)
        {
            string text = (string)box_id.SelectedItem;
            string sql = "select Box_Capacity from Box where Box_ID = '" + text + "'";
            DataSet ds = MySQLHelper.GetDataSet(MySQLHelper.GetConn(), CommandType.Text, sql, null);
            if (!((ds == null) || (ds.Tables.Count == 0) || (ds.Tables.Count == 1 && ds.Tables[0].Rows.Count == 0)))
            {
                box_capacity.Text = ds.Tables[0].Rows[0]["Box_Capacity"].ToString();
            }
            else
            {
                box_capacity.Text = "";
            }
        }
    }
}
