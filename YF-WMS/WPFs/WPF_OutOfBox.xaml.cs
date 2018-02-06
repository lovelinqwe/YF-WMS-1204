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
    /// WPF_OutOfBox.xaml 的交互逻辑
    /// </summary>
    public partial class WPF_OutOfBox : Window
    {
        DataRowView _view;
        string _operation;
        YF_WMS.Model.Outbound _list;

        private string _Outbound_Time;
        private string _Custom_Name;
        private string _Box_ID;
        private string _Sale_ID;
        private string _Material_ID;
        private string _Material_Spec;
        private string _Material_SQty;
        private string _Material_SerialNum;
        private string _Material_RSQty;

        public WPF_OutOfBox(DataRowView view, string operation, YF_WMS.Model.Outbound list)
        {
            InitializeComponent();
            _view = view;
            _operation = operation;
            _list = list;
            if (_list != null)
            {
                material_sqty.Text = _list.Material_SQty.ToString();
                material_sqty.IsEnabled = false;
                material_qty.IsEnabled = false;

                _Outbound_Time = DateTime.Parse(_list.Outbound_Time).ToString("yyyy-MM-dd HH:mm:ss");
                _Custom_Name = _list.Custom_Name;
                _Box_ID = combobox.Text;
                _Sale_ID = _list.Sale_ID;
                _Material_ID = _list.Material_ID;
                _Material_Spec = _list.Material_Spec;
                _Material_SQty = material_sqty.Text.Trim();
                _Material_SerialNum = _list.Material_SerialNum;
                _Material_RSQty = material_sqty.Text.Trim();
            }
            else
            {
                _Outbound_Time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                _Custom_Name = _view.Row["FName1"].ToString();
                _Box_ID = combobox.Text;
                _Sale_ID = _view.Row["FBillNo"].ToString();
                _Material_ID = _view.Row["FNumber"].ToString();
                _Material_Spec = _view.Row["FModel"].ToString();
                _Material_SQty = material_sqty.Text.Trim();
                _Material_SerialNum = _list.Material_SerialNum;
                _Material_RSQty = _view.Row["FAuxQty"].ToString();
            }

        }

        private void outofbox_determine(object sender, RoutedEventArgs e)
        {

            if (_list != null)
            {
              
                select_input("input_excel",_Outbound_Time, _Custom_Name, _Box_ID, _Sale_ID, _Material_ID, _Material_Spec, _Material_SQty, _Material_SerialNum, _Material_RSQty);
            }
            else if (_list == null)
            {
               
                select_input("input_so",_Outbound_Time, _Custom_Name, _Box_ID, _Sale_ID, _Material_ID, _Material_Spec, _Material_SQty, _Material_SerialNum, _Material_RSQty);
            }
              

        }

        private void select_input(string _operation,string _Outbound_Time,string _Custom_Name,string _Box_ID,string _Sale_ID,string _Material_ID,string _Material_Spec,string _Material_SQty,string _Material_SerialNum,string _Material_RSQty)
        {
            string sql;
            DataSet ds;
            if (combobox.Text!="托板")
            {
                ///获取下架料箱的库位编号
                sql = "select Shelf_ID from Box where Box_ID ='" + combobox.Text + "'";
                string shelf_id = MySQLHelper.GetDataSet(MySQLHelper.GetConn(), CommandType.Text, sql, null).Tables[0].Rows[0]["Shelf_ID"].ToString();
                ///判断库位编号是否已存在队列中
                sql = "select Shelf_ID from Queue where Shelf_ID = '" + shelf_id + "'";
                ds = MySQLHelper.GetDataSet(MySQLHelper.GetConn(), CommandType.Text, sql, null);
                if ((ds == null) || (ds.Tables.Count == 0) || (ds.Tables.Count == 1 && ds.Tables[0].Rows.Count == 0))
                {
                    ///插入出库的下架操作到队列中
                    sql = "insert into Queue (Shelf_ID,Queue_Type,Create_Time,Modify_Time) values ('" + shelf_id + "','出库','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    MySQLHelper.ExecuteNonQuery(MySQLHelper.GetConn(), CommandType.Text, sql, null);
                }
            }
            ///插入出库记录
            sql = "insert into Outbound (Outbound_Time,Custom_Name,Box_ID,Sale_ID,Material_ID,Material_Spec,Material_SQty,Material_SerialNum,Create_Time,Modify_Time) values ('" + _Outbound_Time + "','" + _Custom_Name + "','" + combobox.Text + "','" + _Sale_ID + "','" + _Material_ID + "','" + _Material_Spec + "','" + _Material_SQty + "','" + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            MySQLHelper.ExecuteNonQuery(MySQLHelper.GetConn(), CommandType.Text, sql, null);
            ///获取下架数量是否大于料箱物料数量的数据集
            sql = "select * from Material where Material_ID ='" + _Material_ID + "' and Box_ID='" + combobox.Text + "' and Material_Qty >= " + _Material_SQty;
            ds = MySQLHelper.GetDataSet(MySQLHelper.GetConn(), CommandType.Text, sql, null);
            ///判断下架数量是否大于料箱物料数量
            Console.WriteLine(sql);
            if (!((ds == null) || (ds.Tables.Count == 0) || (ds.Tables.Count == 1 && ds.Tables[0].Rows.Count == 0)))
            {

                ///待销售数量与销售订单数量相同
                ///
                Console.WriteLine((int)Convert.ToSingle(_Material_RSQty)+","+ int.Parse(material_sqty.Text));
                if (((int)Convert.ToSingle(_Material_RSQty) - int.Parse(material_sqty.Text)) == 0)
                {
                    ///待销售数量与料箱中物料数量相同
                    if (int.Parse(material_qty.Text) == int.Parse(material_sqty.Text))
                    {
                        ///删除物料表数据
                        sql = "delete from  Material where Material_ID = '" + _Material_ID + "' and Box_ID='" + combobox.Text + "'";
                        MySQLHelper.ExecuteNonQuery(MySQLHelper.GetConn(), CommandType.Text, sql, null);

                    }
                    ///待销售数量小于料箱中物料数量相同
                    else if (int.Parse(material_qty.Text) > int.Parse(material_sqty.Text))
                    {
                        ///更新物料表数据
                        sql = "update Material set Material_Qty = Material_Qty - " + material_sqty.Text.Trim() + ",Modify_Time = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where Material_ID = '" + _Material_ID + "' and Box_ID='" + combobox.Text + "'";
                        MySQLHelper.ExecuteNonQuery(MySQLHelper.GetConn(), CommandType.Text, sql, null);

                    }
                    ///待销售数量大于料箱中物料数量相同
                    else if (int.Parse(material_qty.Text) > int.Parse(material_sqty.Text))
                    {
                        MessageBox.Show("待销售数量大于料箱物料数量，请重新输入");
                    }
                    ///更新料箱容量
                    sql = "update Box set Box_Capacity = '" + now_box_capacity.Text.Trim() + "',Modify_Time = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where Box_ID = '" + combobox.Text + "'";
                    MySQLHelper.ExecuteNonQuery(MySQLHelper.GetConn(), CommandType.Text, sql, null);

                    if (_operation.Equals("input_so"))
                    {
                        ///更新datagrid销售订单数据
                        _view.Row["FAuxQty"] = ((int)Convert.ToSingle(_Material_RSQty) - int.Parse(material_sqty.Text)).ToString();
                        if (int.Parse(_view.Row["FAuxQty"].ToString()) <= 0)
                        {
                            _view.Delete();
                            this.DialogResult = false;
                        }
                    }
                    else if (_operation.Equals("input_excel"))
                    {
                        this.DialogResult = true;
                    }
                   
                    this.Close();
                }
                ///待销售数量小于销售订单数量
                else if (((int)Convert.ToSingle(_Material_RSQty) - int.Parse(material_sqty.Text)) > 0)
                {
                    ///待销售数量大于料箱中物料数量
                    if (int.Parse(material_qty.Text) == int.Parse(material_sqty.Text))
                    {
                        ///删除物料表
                        sql = "delete from  Material where Material_ID = '" + _Material_ID + "' and Box_ID='" + combobox.Text + "'";
                        MySQLHelper.ExecuteNonQuery(MySQLHelper.GetConn(), CommandType.Text, sql, null);

                    }
                    ///待销售数量小于料箱中物料数量
                    else if (int.Parse(material_qty.Text) > int.Parse(material_sqty.Text))
                    {
                        ///更新物料表
                        sql = "update Material set Material_Qty = Material_Qty - " + material_sqty.Text.Trim() + ",Modify_Time='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where Material_ID = '" + _Material_ID + "' and Box_ID='" + combobox.Text + "'";
                        MySQLHelper.ExecuteNonQuery(MySQLHelper.GetConn(), CommandType.Text, sql, null);

                    }
                    ///待销售数量大于销售订单数量
                    else if (int.Parse(material_qty.Text) > int.Parse(material_sqty.Text))
                    {
                        MessageBox.Show("待销售数量大于料箱物料数量，请重新输入");
                    }
                    ///更新料箱容量
                    sql = "update Box set Box_Capacity = '" + now_box_capacity.Text.Trim() + "',Modify_Time='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where Box_ID = '" + combobox.Text + "'";
                    MySQLHelper.ExecuteNonQuery(MySQLHelper.GetConn(), CommandType.Text, sql, null);

                    if (_operation.Equals("input_so"))
                    {
                        ///更新datagrid销售订单数据
                        _view.Row["FAuxQty"] = ((int)Convert.ToSingle(_Material_RSQty) - int.Parse(material_sqty.Text)).ToString();
                        if (int.Parse(_view.Row["FAuxQty"].ToString()) <= 0)
                        {
                            _view.Delete();
                        }
                        this.DialogResult = false;
                    }
                    else if (_operation.Equals("input_excel"))
                    {
                        this.DialogResult = true;
                    }
                    this.Close();
                }
                else if (((int)Convert.ToSingle(_Material_RSQty) - int.Parse(material_sqty.Text)) < 0)
                {
                    MessageBox.Show("待销售数量大于销售订单数量，请重新输入");
                }
            }
            else
            {
                MessageBox.Show("销售数量超出库存，请重新填写");
            }
        }
        private void combobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ///随着combobox变化料箱物料数量和容量也跟着变化
            ///
          
            if (_operation.Equals("input_so"))
            {
                string text = (string)combobox.SelectedItem;
                string sql;
                DataSet ds;
                if (text != "托板")
                {
                    back_box_capacity.IsEnabled = true;
                    now_box_capacity.IsEnabled = true;
                    sql = "select Box_Capacity from Box where Box_ID = '" + text + "'";
                    back_box_capacity.Text = MySQLHelper.GetDataSet(MySQLHelper.GetConn(), CommandType.Text, sql, null).Tables[0].Rows[0]["Box_Capacity"].ToString();
                    sql = "select Material_Qty from Material where Material_ID = '" + _view.Row[1].ToString() + "' and Box_ID = '" + text + "'";
                    ds = MySQLHelper.GetDataSet(MySQLHelper.GetConn(), CommandType.Text, sql, null);
                    if (!((ds == null) || (ds.Tables.Count == 0) || (ds.Tables.Count == 1 && ds.Tables[0].Rows.Count == 0)))
                    {
                        material_qty.Text = MySQLHelper.GetDataSet(MySQLHelper.GetConn(), CommandType.Text, sql, null).Tables[0].Rows[0]["Material_Qty"].ToString();
                    }
                }
                else
                {
                    back_box_capacity.IsEnabled = false;
                    now_box_capacity.IsEnabled = false;
                    back_box_capacity.Text = "";
                    now_box_capacity.Text = "";
                }
                sql = "select Material_Qty from Material where Material_ID = '" + _Material_ID + "' and Box_ID = '" + text + "'";
                ds = MySQLHelper.GetDataSet(MySQLHelper.GetConn(), CommandType.Text, sql, null);
                if (!((ds == null) || (ds.Tables.Count == 0) || (ds.Tables.Count == 1 && ds.Tables[0].Rows.Count == 0)))
                {
                    material_qty.Text = MySQLHelper.GetDataSet(MySQLHelper.GetConn(), CommandType.Text, sql, null).Tables[0].Rows[0]["Material_Qty"].ToString();
                }

            }
            else if(_operation.Equals("input_excel"))
            {
                string text = (string)combobox.SelectedItem;
                string sql;
                DataSet ds;
                if (text != "托板")
                {
                    back_box_capacity.IsEnabled = true;
                    now_box_capacity.IsEnabled = true;
                    sql = "select Box_Capacity from Box where Box_ID = '" + text + "'";
                    ds = MySQLHelper.GetDataSet(MySQLHelper.GetConn(), CommandType.Text, sql, null);
                    if (!((ds == null) || (ds.Tables.Count == 0) || (ds.Tables.Count == 1 && ds.Tables[0].Rows.Count == 0)))
                    {
                        back_box_capacity.Text = MySQLHelper.GetDataSet(MySQLHelper.GetConn(), CommandType.Text, sql, null).Tables[0].Rows[0]["Box_Capacity"].ToString();
                    }                
                    Console.WriteLine(_Material_ID);
                    sql = "select Material_Qty from Material where Material_ID = '" + _Material_ID + "' and Box_ID = '" + text + "'";
                    ds = MySQLHelper.GetDataSet(MySQLHelper.GetConn(), CommandType.Text, sql, null);
                    if (!((ds == null) || (ds.Tables.Count == 0) || (ds.Tables.Count == 1 && ds.Tables[0].Rows.Count == 0)))
                    {
                        material_qty.Text = MySQLHelper.GetDataSet(MySQLHelper.GetConn(), CommandType.Text, sql, null).Tables[0].Rows[0]["Material_Qty"].ToString();
                    }
                      
                }
                else
                {
                    back_box_capacity.IsEnabled = false;
                    now_box_capacity.IsEnabled = false;
                    back_box_capacity.Text = "";
                    now_box_capacity.Text = "";

                    sql = "select Material_Qty from Material where Material_ID = '" + _Material_ID + "' and Box_ID = '" + text + "'";
                    ds = MySQLHelper.GetDataSet(MySQLHelper.GetConn(), CommandType.Text, sql, null);
                    if (!((ds == null) || (ds.Tables.Count == 0) || (ds.Tables.Count == 1 && ds.Tables[0].Rows.Count == 0)))
                    {
                        material_qty.Text = MySQLHelper.GetDataSet(MySQLHelper.GetConn(), CommandType.Text, sql, null).Tables[0].Rows[0]["Material_Qty"].ToString();
                    }
                }
            }
           
        }

    }
}
