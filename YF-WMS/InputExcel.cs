using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace YF_WMS
{
    public class InputExcel
    {
        private static DataTable schemaTable;

        public static DataTable GetTable()
        {
            string resultFile = "";
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = "D:\\";
            openFileDialog1.Filter = "XLS Files (*.xls)|*.xls|All files (*.*)|*.*";
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == true)
                  {
                   resultFile = openFileDialog1.FileName;  
                   }

            try
            {
                string strConn = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + resultFile + ";" + "Extended Properties='Excel 12.0;HDR=NO'";      //连接语句，读取文件路劲
                string strExcel = "select * from [Sheet1$]";                                   //查询Excel表名，默认是Sheet1
                OleDbConnection ole = new OleDbConnection(strConn);
                ole.Open();
                schemaTable = new DataTable();
                OleDbDataAdapter odp = new OleDbDataAdapter(strExcel, strConn);
                odp.Fill(schemaTable);
                ole.Close();//打开连接
            }
            catch (OleDbException)
            {
                MessageBox.Show("excel表不是预期表，请重新导入");
            }
            DataTable dt = schemaTable;
            return dt;
        }
    }
}
