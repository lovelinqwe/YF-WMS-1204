using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data.OleDb;
using Microsoft.Win32;
using System.Data;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;
using System.Data;
using System.IO;
using Microsoft.Win32;
namespace YF_WMS
{
    public  class ExportExcel
    {
        public static bool DataTableToExcel(DataTable dt)
        {
            bool result = false;
            IWorkbook workbook = null;
            FileStream fs = null;
            IRow row = null;
            ISheet sheet = null;
            ICell cell = null;
            try
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    workbook = new HSSFWorkbook();
                    sheet = workbook.CreateSheet("Sheet0");//创建一个名称为Sheet0的表  
                    int rowCount = dt.Rows.Count;//行数  
                    int columnCount = dt.Columns.Count;//列数  

                    //设置列头  
                    row = sheet.CreateRow(0);//excel第一行设为列头  
                    for (int c = 0; c < columnCount; c++)
                    {
                        cell = row.CreateCell(c);
                        cell.SetCellValue(dt.Columns[c].Caption);
                    }

                    //设置每行每列的单元格,  
                    for (int i = 0; i < rowCount; i++)
                    {
                        row = sheet.CreateRow(i + 1);
                        for (int j = 0; j < columnCount; j++)
                        {
                            cell = row.CreateCell(j);//excel第二行开始写入数据  
                            cell.SetCellValue(dt.Rows[i][j].ToString());
                        }
                    }
                    SaveFileDialog sfd = new SaveFileDialog()
                    {
                        DefaultExt = "csv",
                        Filter = "XLS Files (*.xls)|*.xls|XLSX Files (*.xlsx)|*.xlsx|All files (*.*)|*.*",
                        FilterIndex = 1
                    };
                    if (sfd.ShowDialog() == true)
                    {
                        Console.WriteLine(sfd.FileName);
     
                    }
                    using (fs = File.OpenWrite(@sfd.FileName))
                    {
                        workbook.Write(fs);//向打开的这个xls文件中写入数据  
                        result = true;
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                if (fs != null)
                {
                    fs.Close();
                }
                return false;
            }
        }
        public static DataTable Returntable(System.Windows.Controls.DataGrid dataGrid)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            for (int i = 0; i < dataGrid.Columns.Count; i++)
            {
                if (dataGrid.Columns[i].Visibility == System.Windows.Visibility.Visible)//只导出可见列
                {
                    dt.Columns.Add(dataGrid.Columns[i].Header.ToString());//构建表头

                }
            }
            Console.WriteLine(dataGrid.Items.Count);
            for (int i = 0; i < dataGrid.Items.Count; i++)
            {
                int columnsIndex = 0;
                DataRow row = dt.NewRow();

                for (int j = 0; j < dataGrid.Columns.Count; j++)
                {
                    if (dataGrid.Columns[j].Visibility == System.Windows.Visibility.Visible)
                    {

                        if (dataGrid.Items[i] != null && (dataGrid.Columns[j].GetCellContent(dataGrid.Items[i]) as TextBlock) != null)//填充可见列数据
                        {
                            row[columnsIndex] = (dataGrid.Columns[j].GetCellContent(dataGrid.Items[i]) as TextBlock).Text.ToString();

                        }
                        else
                        {
                            row[columnsIndex] = "";
                        }
                        columnsIndex++;
                    }
                }
                dt.Rows.Add(row);
            }
            return dt;
        }
        public static void Export(System.Windows.Controls.DataGrid dataGrid)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            for (int i = 0; i < dataGrid.Columns.Count; i++)
            {
              if (dataGrid.Columns[i].Visibility == System.Windows.Visibility.Visible)//只导出可见列
               {
                    dt.Columns.Add(dataGrid.Columns[i].Header.ToString());//构建表头

                }
            }
            Console.WriteLine(dataGrid.Items.Count);
            for (int i = 0; i < dataGrid.Items.Count; i++)
            {
                int columnsIndex = 0;
                DataRow row = dt.NewRow();

                for (int j = 0; j < dataGrid.Columns.Count; j++)
                {
                    if (dataGrid.Columns[j].Visibility == System.Windows.Visibility.Visible)
                    {
                   
                        if (dataGrid.Items[i] != null && (dataGrid.Columns[j].GetCellContent(dataGrid.Items[i]) as TextBlock) != null)//填充可见列数据
                        {
                            row[columnsIndex] = (dataGrid.Columns[j].GetCellContent(dataGrid.Items[i]) as TextBlock).Text.ToString();
                          
                        }
                        else
                        {
                            row[columnsIndex] = "";
                        }
                        columnsIndex++;
                   }
                }
                dt.Rows.Add(row);
            }

            System.Data.DataTable tmpDataTable = dt;


            //方法一：
            DataTabletoExcel(dt);

            //方法二：



            //打开保存文件路径          
            //System.Windows.Forms.SaveFileDialog save = new System.Windows.Forms.SaveFileDialog();
            ////save.Filter = "Excel文件(*.xls)|";
            //save.Title = "请选择要保存的路径";
            //save.FileName = _fileName;

            //if (save.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //{
            //   // string fileName = save.FileName;
            //    //ExportFiles.ExportExcel(fileName, dt);
            //}
        }
        public static void DataTabletoExcel(System.Data.DataTable tmpDataTable)
        {
            if (tmpDataTable == null)
            {
                return;
            }
            int rowNum = tmpDataTable.Rows.Count;
            int columnNum = tmpDataTable.Columns.Count;

            int rowIndex = 1;
            int columnIndex = 0;
            //Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            //xlApp.DefaultFilePath = "";
            //xlApp.DisplayAlerts = true;
            //xlApp.SheetsInNewWorkbook = 1;
            //Workbook xlBook = xlApp.Workbooks.Add(true);



            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
            app.Visible = false;//不显示app
            Excel.Workbook wBook = app.Workbooks.Add(true);
            Excel.Worksheet sheet = (Excel.Worksheet)wBook.Sheets[1];
            foreach (DataColumn dc in tmpDataTable.Columns)
            {
                columnIndex++;
                app.Cells[rowIndex, columnIndex] = dc.ColumnName;
            }

            for (int i = 0; i < rowNum; i++)
            {
                rowIndex++;
                columnIndex = 0;
                for (int j = 0; j < columnNum; j++)
                {
                    columnIndex++;
                    app.Cells[rowIndex, columnIndex] = tmpDataTable.Rows[i][j].ToString();
                }
            }


            // Excel.Range range = sheet.Columns;//选中所有区域
            //Excel.Range range = app.Range[app.Cells[1, 1], app.Cells[1, 3]];//选中第一行数据
            //range.Font.Size = 15;
            //range.Font.Underline = true;
            //range.Font.Name = "黑体";

            // range.Cells.Interior.Color = System.Drawing.Color.FromArgb(255, 204, 153).ToArgb();

            //range.Columns.AutoFit();
            //range.Interior.ColorIndex = 3;
            ///string str3 = Directory.GetCurrentDirectory();
            ///
            SaveFileDialog sfd = new SaveFileDialog()
            {
                DefaultExt = "csv",
                Filter = "XLSX Files (*.xlsx)|*.xlsx|All files (*.*)|*.*",
                FilterIndex = 1
            };
            if (sfd.ShowDialog() == true)
            {
                Console.WriteLine(sfd.FileName);
                wBook.SaveAs(sfd.FileName);
            }
            app.Quit();
        }

    }
}
  