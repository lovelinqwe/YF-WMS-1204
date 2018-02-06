using DatabaseHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace YF_WMS.WPFs
{
    /// <summary>
    /// aaa.xaml 的交互逻辑
    /// </summary>
    public partial class aaa : Window
    {
        public aaa()
        {
            InitializeComponent();
        }

        private void ManuallyAdd(object sender, RoutedEventArgs e)
        {
            ///弹出输入窗口Input_Inbound
            ///手动填写物料入库信息
            ///
         
        }

        private void InputToExcel(object sender, RoutedEventArgs e)
        {
            ///弹出文件选择对话框，选取Excel文件
            ///把Excel文件内容读至数组
            ///把数组添加到表Inbound

            DataTable dt = InputExcel.GetTable();
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    List<Model.Inbound> lists = new List<Model.Inbound>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Console.WriteLine(Convert.ToInt32(dt.Rows[i][0]) + "," + dt.Rows[i][1].ToString() + "," + dt.Rows[i][2].ToString() + "," + dt.Rows[i][4].ToString() + "," + dt.Rows[i][3].ToString() + "," + dt.Rows[i][5].ToString() + "," + dt.Rows[i][6].ToString() + "," + Convert.ToInt32(dt.Rows[i][7]) + "," + dt.Rows[i][8].ToString());
                        ///导入的数据模板
                        Model.Inbound u = new Model.Inbound();
                        u.Inbound_ID = dt.Rows[i][0].ToString();
                        u.Inbound_Time = dt.Rows[i][1].ToString();
                        u.Supplier_Name = dt.Rows[i][2].ToString();
                        u.Box_ID = dt.Rows[i][4].ToString();
                        u.Purchase_ID = dt.Rows[i][3].ToString();
                        u.Material_ID = dt.Rows[i][5].ToString();
                        u.Material_Spec = dt.Rows[i][6].ToString();
                        u.Material_PQty = Convert.ToInt32(dt.Rows[i][7]);
                        u.Material_SerialNum = dt.Rows[i][8].ToString();
                        lists.Add(u);
                    }
                    datagridinbound.ItemsSource = lists;
                }
            }
            else
            {
                MessageBox.Show("excel表格式错误，请重新导入");
            }
        }

        private void InputPO(object sender, RoutedEventArgs e)
        {
            ///弹出窗口PO
            ///
            WPF_PO wPF_PO = new WPF_PO();
            wPF_PO.ShowDialog();
        }

        public void Select(object sender, RoutedEventArgs e)
        {
            ///执行查询：%
            ///显示Datagrid或弹出错误提示
            ///
            string select = selectinbound.Text.Trim();
            DataSet ds;
            if (select.Equals(""))
            {
                ds = MySQLHelper.GetDataSet(MySQLHelper.GetConn(), CommandType.Text, "Select * from  Inbound", null);
            }
            else
            {
                ds = MySQLHelper.GetDataSet(MySQLHelper.GetConn(), CommandType.Text, "Select * from  Inbound where Inbound_ID='" + select + "'", null);
            }
            DataTable dt = ds.Tables[0];
            datagridinbound.ItemsSource = dt.DefaultView;
        }

        private void ExportToExcel(object sender, RoutedEventArgs e)
        {
            ///弹出文件保存框，选择路径和Excel文件
            ///将Datagrid或Query 导出至Excel
            ///
            System.Windows.Controls.DataGrid DataGrid = datagridinbound;

            ExportExcel.Export(DataGrid);

        }


        private void Today_Select(object sender, RoutedEventArgs e)
        {
            ///执行Query:Inbound，并按日期段筛选
            ///更新Datagrid或提示错误
            ///
            DataSet ds;
            ds = MySQLHelper.GetDataSet(MySQLHelper.GetConn(), CommandType.Text, "Select * from  Inbound where Inbound_Time like'" + DateTime.Now.ToString("yyyy-MM-dd") + "%'", null);
            DataTable dt = ds.Tables[0];
            datagridinbound.ItemsSource = dt.DefaultView;
        }

        private void ThisWeek_Select(object sender, RoutedEventArgs e)
        {
            ///执行Query:Inbound，并按日期段筛选
            ///更新Datagrid或提示错误
            ///
            DataSet ds;
            ds = MySQLHelper.GetDataSet(MySQLHelper.GetConn(), CommandType.Text, "Select * from  Inbound WHERE YEARWEEK(date_format(Inbound_Time,'%Y-%m-%d')) = YEARWEEK(now()); ", null);
            DataTable dt = ds.Tables[0];
            datagridinbound.ItemsSource = dt.DefaultView;
        }

        private void ThisMonth_Select(object sender, RoutedEventArgs e)
        {
            ///执行Query:Inbound，并按日期段筛选
            ///更新Datagrid或提示错误
            DataSet ds;
            ds = MySQLHelper.GetDataSet(MySQLHelper.GetConn(), CommandType.Text, "Select * from  Inbound where Inbound_Time like'" + DateTime.Now.ToString("yyyy-MM") + "%'", null);
            DataTable dt = ds.Tables[0];
            datagridinbound.ItemsSource = dt.DefaultView;
        }

        private void AutoAudit_ERP(object sender, RoutedEventArgs e)
        {
            /// 执行Query：筛选出从PO导入到Inbound的记录，且未审核过的
            /// 执行Query：向KIS表里修改审核字段
            /// 执行Query：把WMS的审核状态字段修改
        }

        private void Menu_Delete(object sender, RoutedEventArgs e)
        {
            ///删除该记录 
            ///
            try
            {
                DataRowView mySelectedElement = (DataRowView)datagridinbound.SelectedItem;
                string inbound_id = mySelectedElement.Row[0].ToString();
                MySQLHelper.ExecuteNonQuery(MySQLHelper.GetConn(), CommandType.Text, "delete  from  Inbound where Inbound_ID='" + inbound_id + "'", null);
                Select(sender, e);
            }
            catch (InvalidCastException)
            {
                MessageBox.Show("导入的excel表不能删除");
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
           
        }

        private void Menu_Storage(object sender, RoutedEventArgs e)
        {
            ///查看对应代码库存记录
            ///
            DataRowView mySelectedElement = (DataRowView)datagridinbound.SelectedItem;
            string sql = "select sum(Material_Qty) as total from Material where Material_ID ='" + mySelectedElement.Row[5].ToString() + "'";
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
    }
}
