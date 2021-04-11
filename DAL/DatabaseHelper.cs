using System;
using System.Configuration;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class DatabaseHelper
    {
        private readonly string _dbConnectionName;

        private SqlConnection OpenSqlConnection()
        {
            string cnstr = ConfigurationManager.ConnectionStrings[_dbConnectionName].ConnectionString;
            SqlConnection cn = new SqlConnection(cnstr);
            cn.Open();

            return cn;
        }

        public DatabaseHelper(string dbConnectionName)
        {
            _dbConnectionName = dbConnectionName;
        }

        public DataSet ExecuteQueryByStoredProc(string storedProc, Dictionary<string, SqlParameter> parameters)
        {
            DataSet ds = new DataSet();
            using (SqlConnection cn = OpenSqlConnection())
            {
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = storedProc;

                    foreach (var param in parameters)
                    {
                        cmd.Parameters.Add(param.Value);
                    }

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(ds);
                    }
                }
            }

            return ds;
        }

        public int ExecuteCommandByStoredProc(string storedProc, Dictionary<string, SqlParameter> parameters)
        {
            int cmdResult = 0;
            using (SqlConnection cn = OpenSqlConnection())
            {
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = storedProc;

                    foreach (var param in parameters)
                    {
                        cmd.Parameters.Add(param.Value);
                    }

                    cmdResult = cmd.ExecuteNonQuery();
                }
            }

            return cmdResult;
        }

    }
}
