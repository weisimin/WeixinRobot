using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
namespace NetFramework
{
    public class Util_Sql
    {

        public static object RunSqlText(string ConnectionStringName, string SqlText)
        {
            object Result = new object();
            string dbConnection = ConfigurationManager.ConnectionStrings[ConnectionStringName].ConnectionString;
            SqlConnection TempConnection = new SqlConnection(dbConnection);//连接字符串
            try
            {
                SqlDataAdapter ToRun = new SqlDataAdapter();　　//創建SqlDataAdapter 类

                ToRun.SelectCommand = new SqlCommand(SqlText, TempConnection);
                TempConnection.Open();
                ToRun.SelectCommand.CommandType = System.Data.CommandType.Text;
                Result = ToRun.SelectCommand.ExecuteScalar();
            }
            catch (Exception AnyError)
            {
                throw AnyError;
            }
            finally
            {
                TempConnection.Close();
            }


            return Result;
        }
        public static DataTable RunSqlDataTable(string ConnectionStringName, string SqlText)
        {

            DataTable Result = new DataTable();
            string dbConnection = ConfigurationManager.ConnectionStrings[ConnectionStringName].ConnectionString;
            SqlConnection TempConnection = new SqlConnection(dbConnection);//连接字符串
            try
            {
                SqlDataAdapter ToRun = new SqlDataAdapter(SqlText, TempConnection);　　//創建SqlDataAdapter 类
                TempConnection.Open();
                ToRun.Fill(Result);

            }
            catch (Exception AnyError)
            {
                throw AnyError;
            }
            finally
            {
                TempConnection.Close();
            }


            return Result;
        }
    }
}
