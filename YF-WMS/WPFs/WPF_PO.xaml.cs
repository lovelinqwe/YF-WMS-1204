using DatabaseHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using YF_WMS.WPFs;
using YF_WMS;

namespace YF_WMS
{
    /// <summary>
    /// WPF_PO.xaml 的交互逻辑
    /// </summary>
    public partial class WPF_PO : Window
    {
        public WPF_PO()
        {
            InitializeComponent();
        }

        private void SynERP(object sender, RoutedEventArgs e)
        {
            ///导入ERP系统PO表数据并显示
            
           ///查询采购订单时序表
           string sql = "select P.FBillNo, V.FName,T.FNumber,T.FModel,T.FName,L.FAuxQty,CONVERT(varchar(10), L.FDate, 23) as FDate FROM dbo.POOrder P INNER JOIN POOrderEntry L ON P.FInterID=L.FInterID"
                       + " LEFT JOIN dbo.t_ICItem T ON t.FItemID = L.FItemID"
                       + " LEFT JOIN dbo.t_Supplier V ON V.FItemID = P.FSupplyID"
                       + " WHERE P.FStatus < 3"
                       +" order by l.FDate";  
            ///建立与KIS数据库的连接
            string Conn = "Server=192.168.2.10;DataBase=AIS20180120095459;uid=sa;pwd=yy22471129!";
            SqlConnection  conn = new SqlConnection(Conn);
            SqlCommand cmd = new SqlCommand(sql, conn);

            SqlDataAdapter sda = new SqlDataAdapter();
            sda.SelectCommand = cmd;

            DataSet ds = new DataSet();

            sda.Fill(ds);

            datagridpo.ItemsSource = ds.Tables[0].DefaultView;
            

        }

        private void PO_Export(object sender, RoutedEventArgs e)
        {
            ///导出列表的PO数据到excel
            ///
            System.Windows.Controls.DataGrid DataGrid = datagridpo;
            ExportExcel.Export(DataGrid);


        }

        private void BatchSetToBox(object sender, RoutedEventArgs e)
        {
            ///批量选取物料放入料箱，入库记录表添加数据
        }

        private void SetToBox(object sender, RoutedEventArgs e)
        {
            ///选取物料放入料箱，入库记录表添加数据
            ///
            //DataRowView mySelectedElement = (DataRowView)datagridpo.SelectedItem;
            //WPF_SetToBox wPF_SetToBox = new WPF_SetToBox(mySelectedElement);
            //string sql = "select Box_ID from Box where Box_Capacity < 100 and Box_Status='可存放'";
            //DataSet ds = MySQLHelper.GetDataSet(MySQLHelper.GetConn(), CommandType.Text, sql, null);
            //foreach (DataRow row in ds.Tables[0].Rows)
            //{
            //    wPF_SetToBox.combobox.Items.Add(row["Box_ID"].ToString());;

            //}
            //wPF_SetToBox.ShowDialog();
            DataRowView mySelectedElement = (DataRowView)datagridpo.SelectedItem;
            WPF_SetToBox wPF_SetToBox = new WPF_SetToBox(mySelectedElement,"input_po",null);
            string sql = "select Box_ID from Box where Box_Capacity < 100 and Box_Status='可存放'";
            DataSet ds = MySQLHelper.GetDataSet(MySQLHelper.GetConn(), CommandType.Text, sql, null);
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                wPF_SetToBox.combobox.Items.Add(row["Box_ID"].ToString()); ;

            }
            wPF_SetToBox.ShowDialog();

        }
        private void Menu_Delete(object sender, RoutedEventArgs e)
        {
            var mySelectedElement = datagridpo.SelectedItems;
            int count = datagridpo.SelectedItems.Count;

            Console.WriteLine(count);
            DataRowView[] drv = new DataRowView[count];
            for (int i = 0;i<count;i++)
            {
                Console.WriteLine(((DataRowView)datagridpo.SelectedItem).Row["FBillNo"].ToString());
                Console.WriteLine(((DataRowView)datagridpo.SelectedItem).Row["FName"].ToString());
                Console.WriteLine(((DataRowView)datagridpo.SelectedItem).Row["FNumber"].ToString());
                Console.WriteLine(((DataRowView)datagridpo.SelectedItem).Row["FModel"].ToString());
                Console.WriteLine(((DataRowView)datagridpo.SelectedItem).Row["FName1"].ToString());
                Console.WriteLine(((DataRowView)datagridpo.SelectedItem).Row["FDate"].ToString());
                Console.WriteLine(((DataRowView)datagridpo.SelectedItem).Row["FAuxQty"].ToString());
                ((DataRowView)datagridpo.SelectedItems[0]).Delete();
            }


        }

        private void select(object sender, RoutedEventArgs e)
        {
            string sql = "select P.FBillNo, V.FName,T.FNumber,T.FModel,T.FName,CAST(E.FAuxQty as decimal) as FAuxQty,CONVERT(varchar(10), L.FDate, 23) as FDate FROM dbo.POOrder P INNER JOIN POOrderEntry L ON P.FInterID=L.FInterID"
                      + " LEFT JOIN dbo.t_ICItem T ON t.FItemID = L.FItemID"
                      + " LEFT JOIN dbo.t_Supplier V ON V.FItemID = P.FSupplyID"
                      + " WHERE P.FStatus < 3 and P.FBillNo like '%"+ selectpo.Text+ "%'"
                      + " order by l.FDate";
            //DataSet ds = MySQLHelper.GetDataSet(Conn, CommandType.Text, sql, null);
            //datagridpo.ItemsSource = ds.Tables[0].DefaultView;
            string Conn = "Server=192.168.2.10;DataBase=AIS20180120095459;uid=sa;pwd=yy22471129!";
            SqlConnection conn = new SqlConnection(Conn);
            SqlCommand cmd = new SqlCommand(sql, conn);

            SqlDataAdapter sda = new SqlDataAdapter();
            sda.SelectCommand = cmd;

            DataSet ds = new DataSet();

            sda.Fill(ds);

            datagridpo.ItemsSource = ds.Tables[0].DefaultView;

        }
        private void Menu_Storage(object sender, RoutedEventArgs e)
        {
            ///查看对应代码库存记录
            ///
            DataRowView mySelectedElement = (DataRowView)datagridpo.SelectedItem;
            string sql = "select sum(Material_Qty) as total from Material where Material_ID ='" + mySelectedElement.Row[2].ToString() + "'";
            Console.WriteLine(sql);
            DataSet ds = MySQLHelper.GetDataSet(MySQLHelper.GetConn(), CommandType.Text, sql, null);
            Console.WriteLine(ds.Tables[0].Rows[0]["total"].ToString());
            if (!((ds == null) || (ds.Tables.Count == 0) || (ds.Tables.Count == 1 && ds.Tables[0].Rows.Count == 0)))
            {
                if (ds.Tables[0].Rows[0]["total"].ToString().Equals("")|| ds.Tables[0].Rows[0]["total"].ToString().Equals(null))
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
