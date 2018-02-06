using DatabaseHelper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
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
using YF_WMS.Model;

namespace YF_WMS.WPFs
{
    /// <summary>
    /// WPF_SetToBox.xaml 的交互逻辑
    /// </summary>
    public partial class WPF_SetToBox : Window

    {
        DataRowView _view;
        string _operation;
        YF_WMS.Model.Inbound _list;
        public WPF_SetToBox(DataRowView view,string operation, YF_WMS.Model.Inbound list)
        {
            InitializeComponent();
            _view = view;
            _operation = operation;
            _list = list;
            if (_list!=null)
            {
                material_pqty.Text = _list.Material_PQty.ToString();
                material_qty.Text = _list.Material_PQty.ToString();
              
            }
           
        }

    
        private void settobox_determine(object sender, RoutedEventArgs e)
        {
            if (_list.Equals(null))
            {
                if (combobox.Text != "托板")
                {
                    ///获取下架料箱的库位编号
                    string sql = "select Shelf_ID from Box where Box_ID ='" + combobox.Text + "'";
                    string shelf_id = MySQLHelper.GetDataSet(MySQLHelper.GetConn(), CommandType.Text, sql, null).Tables[0].Rows[0]["Shelf_ID"].ToString();
                    ///判断库位编号是否已存在队列中
                    sql = "select Shelf_ID from Queue where Shelf_ID = '" + shelf_id + "'";
                    DataSet ds = MySQLHelper.GetDataSet(MySQLHelper.GetConn(), CommandType.Text, sql, null);
                    if ((ds == null) || (ds.Tables.Count == 0) || (ds.Tables.Count == 1 && ds.Tables[0].Rows.Count == 0))
                    {
                        ///插入入库的下架操作到队列中
                        sql = "insert into Queue (Shelf_ID,Queue_Type,Create_Time,Modify_Time) values ('" + shelf_id + "','入库','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        MySQLHelper.ExecuteNonQuery(MySQLHelper.GetConn(), CommandType.Text, sql, null);
                    }
                    ///插入入库记录
                    sql = "insert into Inbound (Inbound_Time,Supplier_Name,Box_ID,Purchase_ID,Material_ID,Material_Spec,Material_PQty,Material_SerialNum,Create_Time,Modify_Time) values ('" + _view.Row["FDate"].ToString() + " 12:00:00" + "','" + _view.Row["FName"].ToString() + "','" + combobox.Text + "','" + _view.Row["FBillNo"].ToString() + "','" + _view.Row["FNumber"].ToString() + "','" + _view.Row["FModel"].ToString() + "','" + material_qty.Text.Trim() + "','" + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    MySQLHelper.ExecuteNonQuery(MySQLHelper.GetConn(), CommandType.Text, sql, null);
                    ///查找采购物料是否在物料表上存在并且在同一料箱中
                    sql = "select * from Material where Material_ID ='" + _view.Row["FNumber"].ToString() + "' and Box_ID='" + combobox.Text + "'";
                    ds = MySQLHelper.GetDataSet(MySQLHelper.GetConn(), CommandType.Text, sql, null);
                    ///采购的物料在待放置料箱中存在
                    if (!((ds == null) || (ds.Tables.Count == 0) || (ds.Tables.Count == 1 && ds.Tables[0].Rows.Count == 0)))
                    {
                        ///更新物料表物料数量
                        sql = "update Material set Material_Qty = Material_Qty + " + material_qty.Text.Trim() + ",Modify_Time='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where Material_ID = '" + _view.Row["FNumber"].ToString() + "' and Box_ID='" + combobox.Text + "'";
                        MySQLHelper.ExecuteNonQuery(MySQLHelper.GetConn(), CommandType.Text, sql, null);
                        ///更新料箱表容量
                        sql = "update Box set Box_Capacity = " + now_box_capacity.Text.Trim() + ",Modify_Time='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where Box_ID = '" + combobox.Text + "'";
                        MySQLHelper.ExecuteNonQuery(MySQLHelper.GetConn(), CommandType.Text, sql, null);
                    }
                    ///采购的物料在待放置料箱中不存在
                    else
                    {
                        ///插入物料表数据
                        sql = "insert into  Material (Material_ID,Material_Spec,Material_Qty,Box_ID,Create_Time,Modify_Time) values ('" + _view.Row["FNumber"].ToString() + "','" + _view.Row["FModel"].ToString() + "','" + material_qty.Text.Trim() + "','" + combobox.Text + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        MySQLHelper.ExecuteNonQuery(MySQLHelper.GetConn(), CommandType.Text, sql, null);
                        ///更新料箱表容量
                        sql = "update Box set Box_Capacity = " + now_box_capacity.Text.Trim() + ",Modify_Time='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where Box_ID = '" + combobox.Text + "'";
                        MySQLHelper.ExecuteNonQuery(MySQLHelper.GetConn(), CommandType.Text, sql, null);
                    }
                }
                else
                {
                    string sql = "insert into Inbound (Inbound_Time,Supplier_Name,Box_ID,Purchase_ID,Material_ID,Material_Spec,Material_PQty,Material_SerialNum,Create_Time,Modify_Time) values ('" + _view.Row["FDate"].ToString() + " 12:00:00" + "','" + _view.Row["FName"].ToString() + "','" + combobox.Text + "','" + _view.Row["FBillNo"].ToString() + "','" + _view.Row["FNumber"].ToString() + "','" + _view.Row["FModel"].ToString() + "','" + material_qty.Text.Trim() + "','" + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    MySQLHelper.ExecuteNonQuery(MySQLHelper.GetConn(), CommandType.Text, sql, null);
                    sql = "insert into  Material (Material_ID,Material_Spec,Material_Qty,Box_ID,Create_Time,Modify_Time) values ('" + _view.Row["FNumber"].ToString() + "','" + _view.Row["FModel"].ToString() + "','" + material_qty.Text.Trim() + "','托板','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    MySQLHelper.ExecuteNonQuery(MySQLHelper.GetConn(), CommandType.Text, sql, null);
                }
                ///选择行数量改变
                _view.Row["FAuxQty"] = ((int)Convert.ToSingle(_view.Row["FAuxQty"].ToString()) - int.Parse(material_qty.Text)).ToString();
                ///采购订单数量小于等于0时，删除
                if (int.Parse(_view.Row["FAuxQty"].ToString()) <= 0)
                {
                    _view.Delete();
                }
                this.DialogResult = false;
                this.Close();
            }
            else if(!_list.Equals(null))
            {
                if (combobox.Text != "托板")
                {
                    ///获取下架料箱的库位编号
                    string sql = "select Shelf_ID from Box where Box_ID ='" + combobox.Text + "'";
                    string shelf_id = MySQLHelper.GetDataSet(MySQLHelper.GetConn(), CommandType.Text, sql, null).Tables[0].Rows[0]["Shelf_ID"].ToString();
                    ///判断库位编号是否已存在队列中
                    sql = "select Shelf_ID from Queue where Shelf_ID = '" + shelf_id + "'";
                    DataSet ds = MySQLHelper.GetDataSet(MySQLHelper.GetConn(), CommandType.Text, sql, null);
                    if ((ds == null) || (ds.Tables.Count == 0) || (ds.Tables.Count == 1 && ds.Tables[0].Rows.Count == 0))
                    {
                        ///插入入库的下架操作到队列中
                        sql = "insert into Queue (Shelf_ID,Queue_Type,Create_Time,Modify_Time) values ('" + shelf_id + "','入库','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        MySQLHelper.ExecuteNonQuery(MySQLHelper.GetConn(), CommandType.Text, sql, null);
                    }
                    ///插入入库记录
                    sql = "insert into Inbound (Inbound_Time,Supplier_Name,Box_ID,Purchase_ID,Material_ID,Material_Spec,Material_PQty,Material_SerialNum,Create_Time,Modify_Time) values ('" + DateTime.Parse(_list.Inbound_Time).ToString("yyyy-MM-dd HH:mm:ss") + "','" + _list.Supplier_Name + "','" + combobox.Text + "','" + _list.Purchase_ID + "','" + _list.Material_ID + "','" + _list.Material_Spec + "','" + _list.Material_PQty + "','" + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    MySQLHelper.ExecuteNonQuery(MySQLHelper.GetConn(), CommandType.Text, sql, null);
                    ///查找采购物料是否在物料表上存在并且在同一料箱中
                    sql = "select * from Material where Material_ID ='" + _list.Material_ID + "' and Box_ID='" + combobox.Text + "'";
                    ds = MySQLHelper.GetDataSet(MySQLHelper.GetConn(), CommandType.Text, sql, null);
                    ///采购的物料在待放置料箱中存在
                    if (!((ds == null) || (ds.Tables.Count == 0) || (ds.Tables.Count == 1 && ds.Tables[0].Rows.Count == 0)))
                    {
                        ///更新物料表物料数量
                        sql = "update Material set Material_Qty = Material_Qty + " + _list.Material_PQty + ",Modify_Time='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where Material_ID = '" + _list.Material_ID + "' and Box_ID='" + combobox.Text + "'";
                        MySQLHelper.ExecuteNonQuery(MySQLHelper.GetConn(), CommandType.Text, sql, null);
                        ///更新料箱表容量
                        sql = "update Box set Box_Capacity = " + now_box_capacity.Text.Trim() + ",Modify_Time='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where Box_ID = '" + combobox.Text + "'";
                        MySQLHelper.ExecuteNonQuery(MySQLHelper.GetConn(), CommandType.Text, sql, null);
                    }
                    ///采购的物料在待放置料箱中不存在
                    else
                    {
                        ///插入物料表数据
                        sql = "insert into  Material (Material_ID,Material_Spec,Material_Qty,Box_ID,Create_Time,Modify_Time) values ('" + _list.Material_ID + "','" + _list.Material_Spec + "','" + _list.Material_PQty + "','" + combobox.Text + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                        MySQLHelper.ExecuteNonQuery(MySQLHelper.GetConn(), CommandType.Text, sql, null);
                        ///更新料箱表容量
                        sql = "update Box set Box_Capacity = " + now_box_capacity.Text.Trim() + ",Modify_Time='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where Box_ID = '" + combobox.Text + "'";
                        MySQLHelper.ExecuteNonQuery(MySQLHelper.GetConn(), CommandType.Text, sql, null);
                    }
                }
                else
                {
                    string  sql = "insert into Inbound (Inbound_Time,Supplier_Name,Box_ID,Purchase_ID,Material_ID,Material_Spec,Material_PQty,Material_SerialNum,Create_Time,Modify_Time) values ('" + DateTime.Parse(_list.Inbound_Time).ToString("yyyy-MM-dd HH:mm:ss") + "','" + _list.Supplier_Name + "','" + combobox.Text + "','" + _list.Purchase_ID + "','" + _list.Material_ID + "','" + _list.Material_Spec + "','" + _list.Material_PQty + "','" + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    MySQLHelper.ExecuteNonQuery(MySQLHelper.GetConn(), CommandType.Text, sql, null);
                    sql = "insert into  Material (Material_ID,Material_Spec,Material_Qty,Box_ID,Create_Time,Modify_Time) values ('" + _list.Material_ID + "','" + _list.Material_Spec + "','" + _list.Material_PQty + "','托板','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    MySQLHelper.ExecuteNonQuery(MySQLHelper.GetConn(), CommandType.Text, sql, null);
                }
                
                this.DialogResult = true;
                this.Close();
            }
        }

        private void combobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ///根据combobox的值料箱容量跟着变化
            ///
            if(_operation.Equals("input_po"))
            {
                string text = (string)combobox.SelectedItem;
                if (text != "托板")
                {
                    back_box_capacity.IsEnabled = true;
                    now_box_capacity.IsEnabled = true;
                    string sql = "select Box_Capacity from Box where Box_ID = '" + text + "'";
                    back_box_capacity.Text = MySQLHelper.GetDataSet(MySQLHelper.GetConn(), CommandType.Text, sql, null).Tables[0].Rows[0]["Box_Capacity"].ToString();
                    material_pqty.Text = _view.Row["FAuxQty"].ToString();
                }
                else
                {
                    back_box_capacity.IsEnabled = false;
                    now_box_capacity.IsEnabled = false;
                    back_box_capacity.Text = "";
                    now_box_capacity.Text = "";
                }
               
            }
            else if(_operation.Equals("input_excel"))
            {
                string text = (string)combobox.SelectedItem;
                if (text != "托板")
                {
                    back_box_capacity.IsEnabled = true;
                    now_box_capacity.IsEnabled = true;
                    string sql = "select Box_Capacity from Box where Box_ID = '" + text + "'";
                    back_box_capacity.Text = MySQLHelper.GetDataSet(MySQLHelper.GetConn(), CommandType.Text, sql, null).Tables[0].Rows[0]["Box_Capacity"].ToString();
                }
                else
                {
                    back_box_capacity.IsEnabled = false;
                    now_box_capacity.IsEnabled = false;
                    back_box_capacity.Text = "";
                    now_box_capacity.Text = "";
                }
            }
          
        }
    }
}
