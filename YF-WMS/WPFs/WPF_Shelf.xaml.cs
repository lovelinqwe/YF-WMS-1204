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
    /// WPF_Shelf.xaml 的交互逻辑
    /// </summary>
    public partial class WPF_Shelf : Window
    {
        DataRowView view = null;
        public WPF_Shelf()
        {
            InitializeComponent();
        }

        private void Shelf_Add(object sender, RoutedEventArgs e)
        {
            ///弹出库位添加输入框，库位表添加数据
            ///
            WPFs.WPF_Shelf_Manual wPF_Shelf_Manual = new WPFs.WPF_Shelf_Manual(this,view,"add");
            wPF_Shelf_Manual.ShowDialog();
        }

        private void Menu_Delete(object sender, RoutedEventArgs e)
        {
            try
            {
                ///数据库删除库位数据
                ///
                DataRowView mySelectedElement = (DataRowView)datagridshelf.SelectedItem;
                string shelf_status = mySelectedElement.Row[4].ToString();
                string shelf_id = mySelectedElement.Row[0].ToString();
                if (shelf_status.Equals("1"))
                {
                    String box_id = MySQLHelper.GetDataSet(MySQLHelper.GetConn(), CommandType.Text, "select Box_ID from  Box where Shelf_ID='" + shelf_id + "'", null).Tables[0].Rows[0]["Box_ID"].ToString();
                    MessageBox.Show("库位已存放料箱,料箱编号为： " + box_id);
                }
                else
                {
                    MySQLHelper.ExecuteNonQuery(MySQLHelper.GetConn(), CommandType.Text, "delete  from  Shelf where Shelf_ID='" + shelf_id + "'", null);
                }
                Shelf_Select(sender, e);
            }
            catch(InvalidCastException)
            {
                MessageBox.Show("导入的excel不能删除");
            }
        }

        public void Shelf_Select(object sender, RoutedEventArgs e)
        {
            ///select查询库位数据并显示
            ///
            ///根据文本框内容查找对应料箱
            ///
            string select = selectshelf.Text.Trim();
            DataSet ds;
            if (select.Equals(""))
            {
                ds = MySQLHelper.GetDataSet(MySQLHelper.GetConn(), CommandType.Text, "Select * from  Shelf", null);
            }
            else
            {
                ds = MySQLHelper.GetDataSet(MySQLHelper.GetConn(), CommandType.Text, "Select * from  Shelf where Shelf_ID='" + select + "'", null);
            }
            DataTable dt = ds.Tables[0];
            datagridshelf.ItemsSource = dt.DefaultView;
        }

        private void Menu_Edit(object sender, RoutedEventArgs e)
        {
            ///弹出库位编辑框，库位表修改对应数据
            ///
            try
            {
                DataRowView mySelectedElement = (DataRowView)datagridshelf.SelectedItem;
                string shelf_id = mySelectedElement.Row[0].ToString();
                string shelf_area = mySelectedElement.Row[1].ToString();
                string shelf_row = mySelectedElement.Row[2].ToString();
                string shelf_column = mySelectedElement.Row[3].ToString();
                string shelf_status = mySelectedElement.Row[4].ToString();   
                WPFs.WPF_Shelf_Manual wPF_Shelf_Manual = new WPFs.WPF_Shelf_Manual(this, mySelectedElement,"edit");
                wPF_Shelf_Manual.shelf_id.Text = shelf_id;
                wPF_Shelf_Manual.shelf_area.Text = shelf_area;
                wPF_Shelf_Manual.shelf_row.Text = shelf_row;
                wPF_Shelf_Manual.shelf_column.Text = shelf_column;
                wPF_Shelf_Manual.shelf_status.Text = shelf_status;
                wPF_Shelf_Manual.Title = "库位编辑";
                wPF_Shelf_Manual.ShowDialog();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("未选中编辑行");
            }
            catch(InvalidCastException)
            {
                MessageBox.Show("导入的excel不能编辑");
            }
        }

        private void Shelf_Export(object sender, RoutedEventArgs e)
        {
            ///库位数据导出到execl
            ///
            System.Windows.Controls.DataGrid DataGrid = datagridshelf;
            ExportExcel.Export(DataGrid);
        }

        private void Shelf_Input(object sender, RoutedEventArgs e)
        {
            DataTable dt = InputExcel.GetTable();
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    List<Model.Shelf> lists = new List<Model.Shelf>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Model.Shelf u = new Model.Shelf();    
                        u.Shelf_ID = dt.Rows[i][1].ToString();
                        u.Shelf_Area = Convert.ToInt32(dt.Rows[i][2]);
                        u.Shelf_Row = Convert.ToInt32(dt.Rows[i][2]);
                        u.Shelf_Column = Convert.ToInt32(dt.Rows[i][2]);        
                        u.Shelf_Status = dt.Rows[i][1].ToString();
                        lists.Add(u);
                    }
                    datagridshelf.ItemsSource = lists;
                }
            }
            else
            {
                MessageBox.Show("excel表格式错误，请重新导入");
            }
        }
    }
}
