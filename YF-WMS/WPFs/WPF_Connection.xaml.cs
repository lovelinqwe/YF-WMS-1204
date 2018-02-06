using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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
using MySql.Data.MySqlClient;

namespace YF_WMS
{
    /// <summary>
    /// WPF_Connection.xaml 的交互逻辑
    /// </summary>
    public partial class WPF_Connection : Window
    {
        public WPF_Connection()
        {
            InitializeComponent();

            ///读取文本文件里的文本，并解码
            ///把解码内容 > Textbox
            FileStream fs = new FileStream("\\\\YF-SHARE\\Temp\\Development\\Codes\\YF-WMS-1204\\YF-WMS\\config.txt", FileMode.OpenOrCreate, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            string line;
            try
            {
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.StartsWith("ip"))
                    {
                        ip.Text = line.Replace("ip:","");
                    }
                    if (line.StartsWith("port"))
                    {
                        port.Text = line.Replace("port:","");
                    }
                    if (line.StartsWith("username"))
                    {
                        username.Text = line.Replace("username:","");
                    }
                    if (line.StartsWith("password"))
                    {
                        password.Password = line.Replace("password:","");
                    }
                }              
            }
            finally
            {
                if (sr != null)
                {
                    sr.Close();
                    fs.Close();
                }
            }
        }

        private void DBConn_Test(object sender, RoutedEventArgs e)
        {
            ///根据Textbox的内容测试数据库连接是否成功
            ///成功则提示
            ///失败则提示
            string conn = "Database=Yafine;server='"+ip.Text.Trim()+"';port='"+port.Text.Trim()+"'; User Id='"+username.Text.Trim()+"';Password='"+password.Password.Trim()+"';charset='utf8';pooling=true";   
            MySqlConnection  mySqlConnection = new MySqlConnection(conn);
            try
            {
                mySqlConnection.Open();
                if (mySqlConnection.State == ConnectionState.Open)
                {            
                    MessageBox.Show("连接成功！");    
                }
                else
                {
                    MessageBox.Show("连接失败！");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                mySqlConnection.Close();
            }
        }

        private void DBConn_Set(object sender, RoutedEventArgs e)
        {
            ///把Textbox里的内容进行编码
            ///把编码保存文本文件里
            FileStream fs = new FileStream("\\\\YF-SHARE\\Temp\\Development\\Codes\\YF-WMS-1204\\YF-WMS\\config.txt", FileMode.OpenOrCreate, FileAccess.Write);    
            StreamWriter sw = new StreamWriter(fs);
            try
            {        
                sw.WriteLine("ip:"+ip.Text);
                sw.WriteLine("port:"+port.Text);
                sw.WriteLine("username:"+username.Text);
                sw.WriteLine("password:"+password.Password);
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                    fs.Close();
                    this.Close();
                }
            }
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            ///关闭窗口，返回父（前）窗口
            this.Close();
        }
    }
}
