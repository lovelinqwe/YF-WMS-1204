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
using System.Windows.Navigation;
using System.Windows.Shapes;
using YF_WMS;
using YF_WMS.WPFs;

namespace YF_WMS
{
    /// <summary>
    /// Page2.xaml 的交互逻辑
    /// </summary>
    public partial class WPF_Inbound : Page
    {
        DataRowView view = null;
        public WPF_Inbound()
        {
            InitializeComponent();
        }

        private void ManuallyAdd(object sender, RoutedEventArgs e)
        {
            ///弹出输入窗口Input_Inbound
            ///手动填写物料入库信息
            ///
            WPF_Inbound_Manual wpF_Inbound_Manual = new WPF_Inbound_Manual(this, view, "add");
            wpF_Inbound_Manual.ShowDialog();
        }

        private void Input_Excel(object sender, RoutedEventArgs e)
        {
            ///弹出文件选择对话框，选取Excel文件
            ///把Excel文件内容读至数组
            ///把数组添加到表Inbound

            //DataTable dt = InputExcel.GetTable();
            DataTable dt = InputToExcel.ExcelToDataTable(true);
            try
            {
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        List<Model.Inbound> lists = new List<Model.Inbound>();
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {

                            ///导入的数据模板
                            Model.Inbound u = new Model.Inbound();
                            // if (dt.Rows[i][0].GetType() == typeof(DBNull)) { u.Inbound_ID = ""; } else { u.Inbound_ID = dt.Rows[i][0].ToString(); }
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
            catch (ArgumentException a)
            {
                throw a;
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

            //ExportExcel.Export(DataGrid);
            // DataTable dt = ((DataView)datagridinbound.ItemsSource).Table;
            ExportExcel.DataTableToExcel(ExportExcel.Returntable(DataGrid));
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
            ds = MySQLHelper.GetDataSet(MySQLHelper.GetConn(), CommandType.Text, "Select * from Inbound WHERE YEARWEEK(date_format(Inbound_Time,'%Y-%m-%d')) = YEARWEEK(date_format('" + DateTime.Now.ToString("yyyy-MM-dd") + "','%Y-%m-%d'))", null);
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
            try
            {
                DataRowView mySelectedElement = (DataRowView)datagridinbound.SelectedItem;
                WPF_Inbound_Manual wpF_Inbound_Manual = new WPF_Inbound_Manual(this, mySelectedElement, "edit");
                wpF_Inbound_Manual.supplier_name.Text = mySelectedElement.Row[2].ToString();
                wpF_Inbound_Manual.box_id.Text = mySelectedElement.Row[3].ToString();
                wpF_Inbound_Manual.purchase_id.Text = mySelectedElement.Row[4].ToString();
                wpF_Inbound_Manual.material_id.Text = mySelectedElement.Row[5].ToString();
                wpF_Inbound_Manual.material_spec.Text = mySelectedElement.Row[6].ToString();
                wpF_Inbound_Manual.material_pqty.Text = mySelectedElement.Row[7].ToString();
                wpF_Inbound_Manual.material_serialnum.Text = mySelectedElement.Row[8].ToString();
                wpF_Inbound_Manual.Title = "编辑入库记录";
                wpF_Inbound_Manual.inbound_manual_determine.Content = "修改";

                wpF_Inbound_Manual.ShowDialog();
            }
            catch (InvalidCastException)
            {
                MessageBox.Show("导入的excel不能编辑");
            }
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

        private void In_bound(object sender, RoutedEventArgs e)
        {
            ///写入数据库
            try
            {
                List<Model.Inbound> lists = (List<Model.Inbound>)datagridinbound.ItemsSource;


                int i = datagridinbound.SelectedIndex;

                DataRowView dataRowView = null;
                Model.Inbound list = lists[i];
              
                WPF_SetToBox wPF_SetToBox = new WPF_SetToBox(dataRowView,"input_excel", list);
                string sql = "select Box_ID from Box where Box_Capacity < 100 and Box_Status='可存放'";
                DataSet ds = MySQLHelper.GetDataSet(MySQLHelper.GetConn(), CommandType.Text, sql, null);
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    wPF_SetToBox.combobox.Items.Add(row["Box_ID"].ToString()); ;

                }
                wPF_SetToBox.combobox.Items.Add("托板"); ;
                wPF_SetToBox.ShowDialog();
                if (wPF_SetToBox.DialogResult == true)//第3步，然后对DialogResult进行判断
                {
                    lists.RemoveAt(i);
                    datagridinbound.ItemsSource = null;
                    datagridinbound.ItemsSource = lists;
                }
               
            }
            catch (InvalidCastException a)
            {
               MessageBox.Show("只有导入的入库excel才能进行入库操作");
            }
        }
    }
}
