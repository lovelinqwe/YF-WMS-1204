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
    /// WPF_Material_Manual.xaml 的交互逻辑
    /// </summary>
    public partial class WPF_Material_Manual : Window
    {
        WPF_Material parentwindow;
        DataRowView parentview;
        string _operation;
        public WPF_Material_Manual(WPF_Material window, DataRowView view, string operation)
        {
            InitializeComponent();
            parentwindow = window;
            parentview = view;
            _operation = operation;
        }

        private void material_manual_determine_Click(object sender, RoutedEventArgs e)
        {
            ///确定
            ///

            string sql = "";
            ///手动添加物料
            if (_operation.Equals("add"))
            {    
                sql = "select * from Box where Box_ID ='" + box_id.Text.Trim() + "'";
                DataSet ds = MySQLHelper.GetDataSet(MySQLHelper.GetConn(), CommandType.Text, sql, null);
                ///料箱编号存在
                if (!((ds == null) || (ds.Tables.Count == 0) || (ds.Tables.Count == 1 && ds.Tables[0].Rows.Count == 0)))
                {
                    sql = "insert into Material (Material_ID,Material_Spec,Material_Qty,Box_ID,Create_Time,Modify_Time) values ('" + material_id.Text.Trim() + "','" + material_spec.Text.Trim() + "','" + material_qty.Text.Trim() + "','" + box_id.Text.Trim() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    MySQLHelper.ExecuteNonQuery(MySQLHelper.GetConn(), CommandType.Text, sql, null);
                }
                ///料箱编号不存在
                else
                {
                    MessageBox.Show("料箱编号不存在，请重新输入");
                }
            }
            ///编辑物料数据
            else if (_operation.Equals("edit"))
            {    

                sql = "select * from Box where Box_ID ='" + box_id.Text.Trim() + "'";
                DataSet ds = MySQLHelper.GetDataSet(MySQLHelper.GetConn(), CommandType.Text, sql, null);
                ///料箱编号存在
                if (!((ds == null) || (ds.Tables.Count == 0) || (ds.Tables.Count == 1 && ds.Tables[0].Rows.Count == 0)))
                {
                    sql = "update Material set Material_ID='" + material_id.Text.Trim() + "',Material_Spec='" + material_spec.Text.Trim() + "',Material_Qty='" + material_qty.Text.Trim() + "',Box_ID='" + box_id.Text.Trim() + "',Modify_Time='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where Material_ID='" + parentview.Row[0].ToString() + "'";
                    MySQLHelper.ExecuteNonQuery(MySQLHelper.GetConn(), CommandType.Text, sql, null);
                }
                ///料箱编号不存在
                else
                {
                    MessageBox.Show("料箱编号不存在，请重新输入");
                }
            }

            parentwindow.Select(sender, e);
            this.Close();
        }

        private void material_manual_cancel_Click(object sender, RoutedEventArgs e)
        {
            ///返回父窗口
            this.Close();
        }
    }
}
