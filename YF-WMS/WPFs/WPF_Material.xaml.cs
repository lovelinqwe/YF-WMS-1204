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
using YF_WMS.WPFs;

namespace YF_WMS
{
    /// <summary>
    /// WPF_Material.xaml 的交互逻辑
    /// </summary>
    public partial class WPF_Material : Window
    {
        DataRowView view = null;
        public WPF_Material()
        {
            InitializeComponent();
        }

        public void Select(object sender, RoutedEventArgs e)
        {
            ///根据文本内容查找对应物料
            string select = selectmeterial.Text.Trim();
            DataSet ds;
            if (select.Equals(""))
            {
                ds = MySQLHelper.GetDataSet(MySQLHelper.GetConn(), CommandType.Text, "Select * from  Material", null);
            }
            else
            {
                ds = MySQLHelper.GetDataSet(MySQLHelper.GetConn(), CommandType.Text, "Select * from  Material where Material_ID like '%" + select + "%'", null);
            }
            DataTable dt = ds.Tables[0];
            datagridmaterial.ItemsSource = dt.DefaultView;
        }

        private void ExportToExcel(object sender, RoutedEventArgs e)
        {
            ///将datagrid数据导出到excel
            ///
            System.Windows.Controls.DataGrid DataGrid = datagridmaterial;
            ExportExcel.Export(DataGrid);
        }

        private void ManuallyAdd(object sender, RoutedEventArgs e)
        {
            //手动添加修改物料数据，数据同步到数据库

            WPF_Material_Manual wpF_Material_Manual = new WPF_Material_Manual(this, view, "add");
            wpF_Material_Manual.ShowDialog();
        }

        private void InputToExcel(object sender, RoutedEventArgs e)
        {
            ///将excel数据导入到datagrid
            ///
            DataTable dt = InputExcel.GetTable();
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    List<Model.Material> lists = new List<Model.Material>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Model.Material u = new Model.Material();              
                        u.Material_ID = dt.Rows[i][0].ToString();
                        u.Material_Spec = dt.Rows[i][1].ToString();
                        u.Material_Qty = Convert.ToInt32(dt.Rows[i][2]);
                        u.Box_ID = Convert.ToInt32(dt.Rows[i][3]);
                        lists.Add(u);
                    }
                    datagridmaterial.ItemsSource = lists;
                }
            }
            else
            {
                MessageBox.Show("excel表格式错误，请重新导入");
            }
        }
        private void Menu_Delete(object sender, RoutedEventArgs e)
        {
            ///删除物料数据，数据同步到数据库
            ///
            try
            {
                DataRowView mySelectedElement = (DataRowView)datagridmaterial.SelectedItem;
                string material_id = mySelectedElement.Row[0].ToString();
                MySQLHelper.ExecuteNonQuery(MySQLHelper.GetConn(), CommandType.Text, "delete  from  Material where Material_ID='" + material_id + "'", null);
                Select(sender, e);
            }
            catch (InvalidCastException)
            {
                MessageBox.Show("导入的excel不能删除");
            }
        }
        private void Menu_Edit(object sender, RoutedEventArgs e)
        {
            ///编辑物料数据，数据同步到数据库
            ///

            try
            {
                DataRowView mySelectedElement = (DataRowView)datagridmaterial.SelectedItem;
                WPF_Material_Manual wpF_Material_Manual = new WPF_Material_Manual(this, mySelectedElement, "edit");
                wpF_Material_Manual.material_id.Text = mySelectedElement.Row[0].ToString();
                wpF_Material_Manual.material_spec.Text = mySelectedElement.Row[1].ToString();
                wpF_Material_Manual.material_qty.Text = mySelectedElement.Row[2].ToString();
                wpF_Material_Manual.box_id.Text = mySelectedElement.Row[3].ToString();
                wpF_Material_Manual.Title = "编辑物料信息";
                wpF_Material_Manual.material_manual_determine.Content = "修改";

                wpF_Material_Manual.ShowDialog();
            }
            catch (InvalidCastException)
            {
                MessageBox.Show("导入的excel不能编辑");
            }
        }
    }
}
