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
    /// WPF_Stock.xaml 的交互逻辑
    /// </summary>
    public partial class WPF_Stock : Page
    {
        public WPF_Stock()
        {
            InitializeComponent();
        }

        private void Select(object sender, RoutedEventArgs e)
        {
            ///根据文本内容查找对应库存数据
            ///  string select = selectoutbound.Text.Trim();
            DataSet ds;
            string sql;
            string where = "";
            if (material_id.Text.Equals(""))
            {  
            }
            else
            {
                where = " where Material_ID = '" + material_id.Text + "'";
            }
            sql = "SELECT " +
                "  UN3.Inbound_ID,Inbound2.Inbound_Time,Outbound2.Outbound_ID,Outbound2.Outbound_Time," +
                "  Inbound2.Material_ID,Inbound2.Material_Spec,Inbound2.Material_Qty," +
                "  (Inbound2.Material_Qty-UN3.stock)*-1 as delivered,UN3.stock,Inbound2.Box_ID" +
                "  from " +
                "  (SELECT * from" +
                "  (	select Inbound_ID,sum(Material_Qty) as stock from" +
                "	(	SELECT Inbound_ID,Material_Qty from Inbound2 " + where +
                "	UNION" +
                "	select Inbound_ID, Material_Qty*-1 from Outbound2 where Inbound_ID IN " + 
                "	(SELECT Inbound_ID from Inbound2  " + where +
                "	)) as UN1" +
                "	GROUP BY Inbound_ID	) as UN2" +
                "	where stock >0	) as UN3" +
                "  LEFT JOIN Inbound2 ON UN3.Inbound_ID = Inbound2.Inbound_ID" +
                "  LEFT JOIN Outbound2 ON UN3.Inbound_ID = Outbound2.Inbound_ID" +
                "  ORDER BY Material_ID";
            Console.WriteLine(DateTime.Now.ToString());
            if (material_id.Text.Equals(""))
            {
                ds = MySQLHelper.GetDataSet(MySQLHelper.GetConn(), CommandType.Text, sql, null);
            }
            else
            {
                ds = MySQLHelper.GetDataSet(MySQLHelper.GetConn(), CommandType.Text, sql, null);
            }
            System.Data.DataTable dt = ds.Tables[0];
            datagridstock.ItemsSource = dt.DefaultView;
            Console.WriteLine(DateTime.Now.ToString());
        }

        private void InputSO(object sender, RoutedEventArgs e)
        {
            ///导入销售订单数据表数据
        }
    }
}
