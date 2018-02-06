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

namespace YF_WMS
{
    /// <summary>
    /// WPF_SO.xaml 的交互逻辑
    /// </summary>
    public partial class WPF_SO : Window
    {
        public WPF_SO()
        {    
            ///初始化
            InitializeComponent();
        }

      
    

        private void SO_Export(object sender, RoutedEventArgs e)
        {
            ///导出列表的销售订单数据到Excel
            ///
            System.Windows.Controls.DataGrid DataGrid = datagridso;
            ExportExcel.Export(DataGrid);
        }

        private void SynERP(object sender, RoutedEventArgs e)
        {
            ///导入ERP系统SO表数据并显示
            ///
            string sql = "select S.FBillNo, I.FNumber,I.FModel,I.FName,E.FAuxQty,O.FName,CONVERT(varchar(10), S.FDate, 23)  as FDate FROM dbo.SEOrder S  INNER JOIN SEOrderEntry E ON S.FInterID=E.FInterID "
                      + " LEFT JOIN t_ICItem I ON I.FItemID=E.FItemID"
                      + " LEFT JOIN t_Organization O ON O.FItemID=S.FCustID"
                      + " WHERE S.FStatus < 3"
                      + " order by S.FDate";
            //DataSet ds = MySQLHelper.GetDataSet(Conn, CommandType.Text, sql, null);
            //datagridpo.ItemsSource = ds.Tables[0].DefaultView;
            string Conn = "Server=192.168.2.10;DataBase=AIS20180120095459;uid=sa;pwd=yy22471129!";
            SqlConnection conn = new SqlConnection(Conn);
            SqlCommand cmd = new SqlCommand(sql, conn);

            SqlDataAdapter sda = new SqlDataAdapter();
            sda.SelectCommand = cmd;

            DataSet ds = new DataSet();

            sda.Fill(ds);
            int i = 0;
            List<Model.SO> lists = new List<Model.SO>();
            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                Model.SO so = new Model.SO();
                so.FBillNo = ds.Tables[0].Rows[i]["FBillNo"].ToString();
                so.FNumber = ds.Tables[0].Rows[i]["FNumber"].ToString();
                so.FAuxQty = ds.Tables[0].Rows[i]["FAuxQty"].ToString();
                so.FName1 = ds.Tables[0].Rows[i]["FName1"].ToString();
                lists.Add(so);
            }
            StringBuilder text = new StringBuilder(" where ");
            for (i = 0; i < lists.Count; i++)
            {
                if (i < lists.Count - 1)
                {
                    text.Append(" Material_ID = '" + lists[i].FNumber + "' or ");

                }
                else
                {
                    text.Append("  Material_ID ='1'");
                }
            }
            
            sql = "SELECT " +
               "  UN3.Inbound_ID,Inbound2.Inbound_Time,Inbound2.Box_ID,Inbound2.Purchase_ID, " +
               "  Inbound2.Material_ID,Inbound2.Material_Spec," +
               "  UN3.stock" +
               "  from " +
               "  (SELECT * from" +
               "  (	select Inbound_ID,sum(Material_Qty) as stock from" +
               "	(	SELECT Inbound_ID,Material_Qty from Inbound2 " + text +
               "	UNION" +
               "	select Inbound_ID, Material_Qty*-1 from Outbound2 where Inbound_ID IN " +
               "	(SELECT Inbound_ID from Inbound2  " + text +
               "	)) as UN1" +
               "	GROUP BY Inbound_ID	) as UN2" +
               "	where stock >0	) as UN3" +
               "  LEFT JOIN Inbound2 ON UN3.Inbound_ID = Inbound2.Inbound_ID" +
               "  LEFT JOIN Outbound2 ON UN3.Inbound_ID = Outbound2.Inbound_ID" +
               "  ORDER BY Material_ID";
            ds = MySQLHelper.GetDataSet(MySQLHelper.GetConn(), CommandType.Text, sql, null);
            System.Data.DataTable dt = ds.Tables[0];
            datagridso.ItemsSource = dt.DefaultView;
        }

        private void SO_OutOfStock(object sender, RoutedEventArgs e)
        {
            ///下架
            ///
            DataRowView mySelectedElement = (DataRowView)datagridso.SelectedItem;
            WPF_OutOfBox wPF_OutOfBox = new WPF_OutOfBox(mySelectedElement,"input_so",null);
            string sql = "select Box_ID from Material where Material_ID = '"+mySelectedElement.Row[1].ToString()+ "' and Material_Qty != 0";
            DataSet ds = MySQLHelper.GetDataSet(MySQLHelper.GetConn(), CommandType.Text, sql, null);
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                wPF_OutOfBox.combobox.Items.Add(row["Box_ID"].ToString()); ;

            }
            wPF_OutOfBox.combobox.Items.Add("托板"); ;
            wPF_OutOfBox.ShowDialog();
        }
        private void Menu_Delete(object sender, RoutedEventArgs e)
        {
            DataRowView mySelectedElement = (DataRowView)datagridso.SelectedItem;
            mySelectedElement.Delete();
        }
        private void select(object sender, RoutedEventArgs e)
        {
            string sql = "select S.FBillNo, I.FNumber,I.FModel,I.FName,CAST(E.FAuxQty as decimal) as FAuxQty,O.FName, CONVERT(varchar(10), S.FDate, 23)  as FDate FROM dbo.SEOrder S  INNER JOIN SEOrderEntry E ON S.FInterID=E.FInterID "
                      + " LEFT JOIN t_ICItem I ON I.FItemID=E.FItemID"
                      + " LEFT JOIN t_Organization O ON O.FItemID=S.FCustID"
                      + " WHERE S.FStatus < 3 and S.FBillNo like '%" + selectso.Text + "%'"
                      + " order by S.FDate";
            //DataSet ds = MySQLHelper.GetDataSet(Conn, CommandType.Text, sql, null);
            //datagridpo.ItemsSource = ds.Tables[0].DefaultView;
            string Conn = "Server=192.168.2.10;DataBase=AIS20180120095459;uid=sa;pwd=yy22471129!";
            SqlConnection conn = new SqlConnection(Conn);
            SqlCommand cmd = new SqlCommand(sql, conn);

            SqlDataAdapter sda = new SqlDataAdapter();
            sda.SelectCommand = cmd;

            DataSet ds = new DataSet();

            sda.Fill(ds);

            datagridso.ItemsSource = ds.Tables[0].DefaultView;
        }
        private void Menu_Storage(object sender, RoutedEventArgs e)
        {
            ///查看对应代码库存记录
            ///
            DataRowView mySelectedElement = (DataRowView)datagridso.SelectedItem;
            string sql = "select sum(Material_Qty) as total from Material where Material_ID ='" + mySelectedElement.Row[1].ToString() + "'";
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
