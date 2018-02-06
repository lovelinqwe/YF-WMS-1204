using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.IO;
using System.Windows;

namespace DatabaseHelper
{
    public class MySQLHelper
    {
       
        /// 数据库连接字符串 
        ///public static string Conn = "Database='Yafine';Data Source='192.168.2.18'; port='3306'; User Id='root';Password='Yafine2016';charset='utf8';pooling=true";

        public static string GetConn()
        {
            FileStream fs = new FileStream("C:\\Users\\Administrator\\Desktop\\config.txt", FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fs);
            string line;
            string conn= "Database='Yafine';";
            try
            {
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.StartsWith("ip"))
                    {
                        conn += "server='" + line.Replace("ip:", "") + "';";
                    }
                    if (line.StartsWith("port"))
                    {
                        conn += "port='" + line.Replace("port:", "") + "';";
                    }
                    if (line.StartsWith("username"))
                    {
                        conn += "User Id='" + line.Replace("username:", "") + "';";
                    }
                    if (line.StartsWith("password"))
                    {
                        conn += "Password='" + line.Replace("password:", "") + "';charset='utf8';pooling=true";
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (sr != null)
                {
                    sr.Close();
                    fs.Close();
                }
            }
            return conn;
        }

        /// 函数描述：给定连接的数据库用假设参数执行一个sql命令（不返回数据集） 
        /// 参数描述： 
        /// 参数="connectionString，一个有效的连接字符串 
        /// 参数="cmdType，命令类型(存储过程, 文本, 等等) 
        /// 参数="cmdText，存储过程名称或者sql命令语句 
        /// 参数="commandParameters，执行命令所用参数的集合 
        /// <returns>执行命令所影响的行数</returns> 
        public static int ExecuteNonQuery(string connectionString, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlCommand cmd = new MySqlCommand();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }
        }


        /// 函数描述：用现有的数据库连接执行一个sql命令（不返回数据集） 
        /// 参数描述： 
        /// 参数="connection，一个现有的数据库连接 
        /// 参数="cmdType，命令类型(存储过程, 文本, 等等) 
        /// 参数="cmdText，存储过程名称或者sql命令语句 
        /// 参数="commandParameters，执行命令所用参数的集合 
        /// <returns>执行命令所影响的行数</returns> 
        public static int ExecuteNonQuery(MySqlConnection connection, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlCommand cmd = new MySqlCommand();

            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            //cmd.Parameters.Clear();
            return val;
        }


        /// 函数描述：使用现有的SQL事务执行一个sql命令（不返回数据集） 
        /// 参数描述： 
        ///举例: 
        /// int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new MySqlParameter("@prodid", 24)); 
        /// 参数="trans，一个现有的事务 
        /// 参数="cmdType，命令类型(存储过程, 文本, 等等) 
        /// 参数="cmdText，存储过程名称或者sql命令语句 
        /// 参数="commandParameters，执行命令所用参数的集合 
        /// <returns>执行命令所影响的行数</returns> 
        public static int ExecuteNonQuery(MySqlTransaction trans, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlCommand cmd = new MySqlCommand();
            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }


        /// 函数描述：用执行的数据库连接执行一个返回数据集的sql命令 
        /// 参数描述： 
        /// 举例: 
        /// MySqlDataReader r = ExecuteReader(connString, CommandType.StoredProcedure, "PublishOrders", new MySqlParameter("@prodid", 24)); 
        /// 参数="connectionString，一个有效的连接字符串 
        /// 参数="cmdType，命令类型(存储过程, 文本, 等等) 
        /// 参数="cmdText，存储过程名称或者sql命令语句 
        /// 参数="commandParameters，执行命令所用参数的集合 
        /// <returns>包含结果的读取器</returns> 
        public static MySqlDataReader ExecuteReader(string connectionString, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            //创建一个MySqlCommand对象 
            MySqlCommand cmd = new MySqlCommand();
            //创建一个MySqlConnection对象 
            MySqlConnection conn = new MySqlConnection(connectionString);

            //在这里我们用一个try/catch结构执行sql文本命令/存储过程，因为如果这个方法产生一个异常我们要关闭连接，因为没有读取器存在， 
            //因此commandBehaviour.CloseConnection 就不会执行 
            try
            {
                //调用 PrepareCommand 方法，对 MySqlCommand 对象设置参数 
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                //调用 MySqlCommand 的 ExecuteReader 方法 
                MySqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                //清除参数 
                cmd.Parameters.Clear();
                return reader;
            }
            catch
            {
                //关闭连接，抛出异常 
                conn.Close();
                throw;
            }
        }


        /// 函数描述：返回DataSet 
        /// 参数描述： 
        /// 参数="connectionString，一个有效的连接字符串 
        /// 参数="cmdType，命令类型(存储过程, 文本, 等等) 
        /// 参数="cmdText，存储过程名称或者sql命令语句 
        /// 参数="commandParameters，执行命令所用参数的集合 
        /// <returns></returns> 
        public static DataSet GetDataSet(string connectionString, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            //创建一个MySqlCommand对象 
            MySqlCommand cmd = new MySqlCommand();
            //创建一个MySqlConnection对象 
            MySqlConnection conn = new MySqlConnection(connectionString);

            //在这里我们用一个try/catch结构执行sql文本命令/存储过程，因为如果这个方法产生一个异常我们要关闭连接，因为没有读取器存在，

            try
            {
                //调用 PrepareCommand 方法，对 MySqlCommand 对象设置参数 
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                //调用 MySqlCommand 的 ExecuteReader 方法 
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = cmd;
                DataSet ds = new DataSet();

                adapter.Fill(ds);
                //清除参数 
                cmd.Parameters.Clear();
                conn.Close();
                return ds;
            }
            catch (Exception e)
            { 
                throw e;
            }
        }


        public static DataTable GetDataTable(string connectionString, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            //创建一个MySqlCommand对象 
            MySqlCommand cmd = new MySqlCommand();
            //创建一个MySqlConnection对象 
            MySqlConnection conn = new MySqlConnection(connectionString);

            //在这里我们用一个try/catch结构执行sql文本命令/存储过程，因为如果这个方法产生一个异常我们要关闭连接，因为没有读取器存在，

            try
            {
                //调用 PrepareCommand 方法，对 MySqlCommand 对象设置参数 
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                //调用 MySqlCommand 的 ExecuteReader 方法 
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = cmd;
                DataTable ds = new DataTable();

                adapter.Fill(ds);
                //清除参数 
                cmd.Parameters.Clear();
                conn.Close();
                return ds;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        /// 函数描述：用指定的数据库连接字符串执行一个命令并返回一个数据集的第一列 
        /// 参数描述： 
        /// <remarks> 
        ///例如: 
        /// Object obj = ExecuteScalar(connString, CommandType.StoredProcedure, "PublishOrders", new MySqlParameter("@prodid", 24)); 
        /// </remarks> 
        ///参数="connectionString，一个有效的连接字符串 
        /// 参数="cmdType，命令类型(存储过程, 文本, 等等) 
        /// 参数="cmdText，存储过程名称或者sql命令语句 
        /// 参数="commandParameters，执行命令所用参数的集合 
        /// <returns>用 Convert.To{Type}把类型转换为想要的 </returns> 
        public static object ExecuteScalar(string connectionString, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlCommand cmd = new MySqlCommand();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
        }


        /// 函数描述：用指定的数据库连接执行一个命令并返回一个数据集的第一列 
        /// 参数描述： 
        /// <remarks> 
        /// 例如: 
        /// Object obj = ExecuteScalar(connString, CommandType.StoredProcedure, "PublishOrders", new MySqlParameter("@prodid", 24)); 
        /// </remarks> 
        /// 参数="connection，一个存在的数据库连接 
        /// 参数="cmdType，命令类型(存储过程, 文本, 等等) 
        /// 参数="cmdText，存储过程名称或者sql命令语句 
        /// 参数="commandParameters，执行命令所用参数的集合 
        /// <returns>用 Convert.To{Type}把类型转换为想要的 </returns> 
        public static object ExecuteScalar(MySqlConnection connection, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlCommand cmd = new MySqlCommand();
            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            object val = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return val;
        }

        /// 函数描述：准备执行一个命令 
        /// 参数描述： 
        /// 参数="cmd，sql命令 
        /// 参数="conn，OleDb连接 
        /// 参数="trans，OleDb事务 
        /// 参数="cmdType，命令类型例如 存储过程或者文本 
        /// 参数="cmdText，命令文本,例如:Select * from Products 
        /// 参数="cmdParms，执行命令的参数 
        private static void PrepareCommand(MySqlCommand cmd, MySqlConnection conn, MySqlTransaction trans, CommandType cmdType, string cmdText, MySqlParameter[] cmdParms)
        {

            if (conn.State != ConnectionState.Open)
                conn.Open();

            cmd.Connection = conn;
            cmd.CommandText = cmdText;

            if (trans != null)
                cmd.Transaction = trans;

            cmd.CommandType = cmdType;

            if (cmdParms != null)
            {
                foreach (MySqlParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }

    }
}

