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
    /// WPF_Box.xaml 的交互逻辑
    /// </summary>
    public partial class WPF_Box : Window
    {
        DataRowView view = null;
        public WPF_Box()
        {
            ///初始化
            InitializeComponent();
        }

        public void Box_Add(object sender, RoutedEventArgs e)
        {
            ///弹出料箱输入框，数据同步到数据库
            ///
            WPFs.WPF_Box_Manual wPF_Box_Manual = new WPFs.WPF_Box_Manual(this, view,"add");
            wPF_Box_Manual.ShowDialog();
        }

        private void Menu_Delete(object sender, RoutedEventArgs e)
        {
            ///删除某一料箱记录，同步到数据库
            ///
            try
            {
                DataRowView mySelectedElement = (DataRowView)datagridbox.SelectedItem;
                string box_id = mySelectedElement.Row[0].ToString();
                string shelf_id = mySelectedElement.Row[5].ToString();
                MySQLHelper.ExecuteNonQuery(MySQLHelper.GetConn(), CommandType.Text, "delete  from  Box where Box_ID='" + box_id + "'", null);
                MySQLHelper.ExecuteNonQuery(MySQLHelper.GetConn(), CommandType.Text, "update Shelf set Shelf_Status = '0' where Shelf_ID='" + shelf_id + "'", null);
                Box_Select(sender, e);
            }
            catch (InvalidCastException)
            {
                MessageBox.Show("导入的excel不能进行删除操作");
            }
        }

        public void Box_Select(object sender, RoutedEventArgs e)
        {
            ///根据文本框内容查找对应料箱
            ///
            string select = selectbox.Text.Trim();
            DataSet ds;
            if (select.Equals(""))
            {
                ds = MySQLHelper.GetDataSet(MySQLHelper.GetConn(), CommandType.Text, "Select * from  Box", null);
            }
            else
            {
                ds = MySQLHelper.GetDataSet(MySQLHelper.GetConn(), CommandType.Text, "Select * from  Box where Box_ID='" + select + "'", null);
            }
            DataTable dt = ds.Tables[0];
            datagridbox.ItemsSource = dt.DefaultView;
        }

        private void Menu_Edit(object sender, RoutedEventArgs e)
        {
            ///弹出料箱编辑输入框，数据同步到数据库
            ///
            ///
            try
            {
                DataRowView mySelectedElement = (DataRowView)datagridbox.SelectedItem;
                string box_id = mySelectedElement.Row[0].ToString();
                string box_color = mySelectedElement.Row[1].ToString();
                string box_capacity = mySelectedElement.Row[2].ToString();
                string box_status = mySelectedElement.Row[3].ToString();
                string box_desc = mySelectedElement.Row[4].ToString();
                string shelf_id = mySelectedElement.Row[5].ToString();
                WPFs.WPF_Box_Manual wPF_Box_Manual = new WPFs.WPF_Box_Manual(this, mySelectedElement, "edit");
                wPF_Box_Manual.box_id.Text = box_id;
                wPF_Box_Manual.box_color.Text = box_color;
                wPF_Box_Manual.box_capacity.Text = box_capacity;
                wPF_Box_Manual.box_status.Text = box_status;
                wPF_Box_Manual.shelf_id.Text = shelf_id;
                wPF_Box_Manual.box_desc.Text = box_desc;
                wPF_Box_Manual.Title = "料箱编辑";
                wPF_Box_Manual.ShowDialog();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("未选中编辑行");
            }
            catch (InvalidCastException)
            {
                MessageBox.Show("导入的excel不能进行编辑操作");
            }
           

        }

        private void Box_Export(object sender, RoutedEventArgs e)
        {
            ///将列表的料箱数据导出到Excel
            ///
            System.Windows.Controls.DataGrid DataGrid = datagridbox;
            ExportExcel.Export(DataGrid);
        }
      

        private void Box_Input(object sender, RoutedEventArgs e)
        {
            DataTable dt = InputExcel.GetTable();
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    List<Model.Box> lists = new List<Model.Box>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Model.Box u = new Model.Box();  
                        u.Box_ID = Convert.ToInt32(dt.Rows[i][0]);
                        u.Box_Color = dt.Rows[i][1].ToString();
                        u.Box_Capacity = Convert.ToInt32(dt.Rows[i][2]);
                        u.Box_Status = dt.Rows[i][3].ToString();
                        u.Box_Desc = dt.Rows[i][4].ToString();
                        u.Shelf_ID = dt.Rows[i][5].ToString();
                        lists.Add(u);
                    }
                    datagridbox.ItemsSource = lists;
                }
            }
            else
            {
                MessageBox.Show("excel表格式错误，请重新导入");
            }
        }
    }
}
