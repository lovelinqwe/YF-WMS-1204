using DatabaseHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using YF_WMS.WPFs;
using System.Reflection;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data.OleDb;
using Microsoft.Win32;

namespace YF_WMS
{
    /// <summary>
    /// WPF_Outbound.xaml 的交互逻辑
    /// </summary>
    public partial class WPF_Outbound : Page
    {
        DataRowView view = null;
        string operation;
        public WPF_Outbound()
        {
            InitializeComponent();
        }

        private void ManuallyAdd(object sender, RoutedEventArgs e)
        {
            ///弹出输入窗口Output_Outbound
            ///手动填写物料出库信息
            ///
            WPF_Outbound_Manual wpF_Outbound_Manual = new WPF_Outbound_Manual(this, view, "add");
            wpF_Outbound_Manual.ShowDialog();
        }

        private void Input_Excel(object sender, RoutedEventArgs e)
        {
            ///弹出文件选择对话框，选取Excel文件
            ///把Excel文件内容读至数组
            ///把数组添加到表Outbound
            ///
            //DataTable dt = InputExcel.GetTable();
            DataTable dt = InputToExcel.ExcelToDataTable(true);

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    List<Model.Outbound> lists = new List<Model.Outbound>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Model.Outbound u = new Model.Outbound();
                        u.Outbound_ID = dt.Rows[i][0].ToString();
                        u.Outbound_Time = dt.Rows[i][1].ToString();
                        u.Custom_Name = dt.Rows[i][2].ToString();
                        u.Box_ID = dt.Rows[i][4].ToString();
                        u.Sale_ID = dt.Rows[i][3].ToString();
                        u.Material_ID = dt.Rows[i][5].ToString();
                        u.Material_Spec = dt.Rows[i][6].ToString();
                        u.Material_SQty = Convert.ToInt32(dt.Rows[i][7]);
                        u.Material_SerialNum = dt.Rows[i][8].ToString();
                        lists.Add(u);
                    }
                    datagridoutbound.ItemsSource = lists;
                }
            }
            else
            {
                MessageBox.Show("excel表格式错误，请重新导入");
            }

        }

        private void InputSO(object sender, RoutedEventArgs e)
        {
            ///弹出窗口SO
            ///
            WPF_SO wPF_SO = new WPF_SO();
            wPF_SO.ShowDialog();
        }

        public void Select(object sender, RoutedEventArgs e)
        {
            ///执行查询：%
            ///显示Datagrid或弹出错误提示
            ///
            string select = selectoutbound.Text.Trim();
            DataSet ds;
            if (select.Equals(""))
            {
                ds = MySQLHelper.GetDataSet(MySQLHelper.GetConn(), CommandType.Text, "Select * from  Outbound", null);
            }
            else
            {
                ds = MySQLHelper.GetDataSet(MySQLHelper.GetConn(), CommandType.Text, "Select * from  Outbound where Outbound_ID='" + select + "'", null);
            }
            System.Data.DataTable dt = ds.Tables[0];
            datagridoutbound.ItemsSource = dt.DefaultView;
        }

        private void ExportToExcel(object sender, RoutedEventArgs e)
        {

            ///弹出文件保存框，选择路径和Excel文件
            ///将Datagrid或Query 导出至Excel
            ///
            System.Windows.Controls.DataGrid DataGrid = datagridoutbound;
            //ExportExcel.Export(DataGrid);
            ExportExcel.DataTableToExcel(ExportExcel.Returntable(DataGrid));
        }
       
       
        private void AutoAudit_ERP(object sender, RoutedEventArgs e)
        {
            /// 执行Query：筛选出从PO导入到Outbound的记录，且未审核过的
            /// 执行Query：向KIS表里修改审核字段
            /// 执行Query：把WMS的审核状态字段修改
        }

        private void Today_Select(object sender, RoutedEventArgs e)
        {
            ///执行Query:Inbound，并按日期段筛选
            ///更新Datagrid或提示错误
            ///
            DataSet ds;
            ds = MySQLHelper.GetDataSet(MySQLHelper.GetConn(), CommandType.Text, "Select * from  Outbound where Outbound_Time like'" + DateTime.Now.ToString("yyyy-MM-dd") + "%'", null);
            System.Data.DataTable dt = ds.Tables[0];
            datagridoutbound.ItemsSource = dt.DefaultView;
        }

        private void ThisWeek_Select(object sender, RoutedEventArgs e)
        {
            ///执行Query:Inbound，并按日期段筛选
            ///更新Datagrid或提示错误
            ///
            DataSet ds;
            ds = MySQLHelper.GetDataSet(MySQLHelper.GetConn(), CommandType.Text, "Select * from Outbound WHERE YEARWEEK(date_format(Outbound_Time,'%Y-%m-%d')) = YEARWEEK(date_format('" + DateTime.Now.ToString("yyyy-MM-dd") + "','%Y-%m-%d'))", null);
            System.Data.DataTable dt = ds.Tables[0];
            datagridoutbound.ItemsSource = dt.DefaultView;
        }

        private void ThisMonth_Select(object sender, RoutedEventArgs e)
        {
            ///执行Query:Inbound，并按日期段筛选
            ///更新Datagrid或提示错误
            ///
            DataSet ds;
            ds = MySQLHelper.GetDataSet(MySQLHelper.GetConn(), CommandType.Text, "Select * from  Outbound where Outbound_Time like'" + DateTime.Now.ToString("yyyy-MM") + "%'", null);
            System.Data.DataTable dt = ds.Tables[0];
            datagridoutbound.ItemsSource = dt.DefaultView;
        }

        private void Menu_Delete(object sender, RoutedEventArgs e)
        {
            ///删除该记录
            ///
            try
            {
                DataRowView mySelectedElement = (DataRowView)datagridoutbound.SelectedItem;
                string outbound_id = mySelectedElement.Row[0].ToString();
                MySQLHelper.ExecuteNonQuery(MySQLHelper.GetConn(), CommandType.Text, "delete  from  Outbound where Outbound_ID='" + outbound_id + "'", null);
                Select(sender, e);
            }
            catch (InvalidCastException)
            {
                MessageBox.Show("导入的excel不能删除");
            }
        }

        private void Menu_WithDraw(object sender, RoutedEventArgs e)
        {
            ///撤回该记录
        }

        private void Menu_Edit(object sender, RoutedEventArgs e)
        {
            ///弹出编辑框编辑该记录
            ///

            try
            {
                DataRowView mySelectedElement = (DataRowView)datagridoutbound.SelectedItem;
                WPF_Outbound_Manual wpF_Outbound_Manual = new WPF_Outbound_Manual(this, mySelectedElement, "edit");
                wpF_Outbound_Manual.custom_name.Text = mySelectedElement.Row[2].ToString();
                wpF_Outbound_Manual.box_id.Text = mySelectedElement.Row[3].ToString();
                wpF_Outbound_Manual.sale_id.Text = mySelectedElement.Row[4].ToString();
                wpF_Outbound_Manual.material_id.Text = mySelectedElement.Row[5].ToString();
                wpF_Outbound_Manual.material_spec.Text = mySelectedElement.Row[6].ToString();
                wpF_Outbound_Manual.material_sqty.Text = mySelectedElement.Row[7].ToString();
                wpF_Outbound_Manual.material_serialnum.Text = mySelectedElement.Row[8].ToString();
                wpF_Outbound_Manual.Title = "编辑入库记录";
                wpF_Outbound_Manual.outbound_manual_determine.Content = "修改";

                wpF_Outbound_Manual.ShowDialog();
            }
            catch(InvalidCastException)
            {
                MessageBox.Show("导入的excel不能编辑");
            }
        }

        private void Menu_Storage(object sender, RoutedEventArgs e)
        {
            ///查看对应代码库存记录
            ///
            YF_WMS.Model.Outbound SelectedElement = (YF_WMS.Model.Outbound)datagridoutbound.SelectedItem;
            string sql = "select sum(Material_Qty) as total from Material where Material_ID ='" + SelectedElement.Material_ID + "'";
            Console.WriteLine(sql);
            DataSet ds = MySQLHelper.GetDataSet(MySQLHelper.GetConn(), CommandType.Text, sql, null);
            if (!((ds == null) || (ds.Tables.Count == 0) || (ds.Tables.Count == 1 && ds.Tables[0].Rows.Count == 0)))
            {
                if (ds.Tables[0].Rows[0]["total"].ToString().Equals("") || ds.Tables[0].Rows[0]["total"].ToString().Equals(null))
                {
                    MessageBox.Show("库存为：0");
                }
                else
                {
                    MessageBox.Show("库存为：" + ds.Tables[0].Rows[0]["total"].ToString());
                }

            }
            else
            {
                MessageBox.Show("库存为：0");
            }
        }
        private void out_bound(object sender, RoutedEventArgs e)
        {
            ///出库操作
            try
            {
                List<Model.Outbound> lists = (List<Model.Outbound>)datagridoutbound.ItemsSource;


                int i = datagridoutbound.SelectedIndex;

                DataRowView dataRowView = null;
                Model.Outbound list = lists[i];

                WPF_OutOfBox wPF_OutOfBox = new WPF_OutOfBox(dataRowView, "input_excel", list);
                string sql = "select Box_ID from Material where Material_ID ='" +list.Material_ID +"'";
                DataSet ds = MySQLHelper.GetDataSet(MySQLHelper.GetConn(), CommandType.Text, sql, null);
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    wPF_OutOfBox.combobox.Items.Add(row["Box_ID"].ToString()); ;

                }   
                wPF_OutOfBox.ShowDialog();
                if (wPF_OutOfBox.DialogResult == true)//第3步，然后对DialogResult进行判断
                {
                    lists.RemoveAt(i);
                    datagridoutbound.ItemsSource = null;
                    datagridoutbound.ItemsSource = lists;
                }

            }
            catch (InvalidCastException a)
            {
                MessageBox.Show("只有导入的出库excel才能进行入库操作");
            }
        }
    }

}
