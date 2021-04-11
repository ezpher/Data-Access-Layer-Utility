using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace TestDAL
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                #region Initalize DbHelper

                DatabaseHelper dbHelper = new DatabaseHelper("TestDALDb");

                #endregion


                #region Query Empty Table

                Dictionary<string, SqlParameter> queryParams = new Dictionary<string, SqlParameter>();

                DataSet ds = dbHelper.ExecuteQueryByStoredProc("[dbo].[SP_QueryTestDALTable]", queryParams);

                Console.WriteLine("Testing Querying Empty Table using Stored Proc: ");
                if (ds.Tables[0].Rows.Count == 0)
                {
                    Console.WriteLine("dataset is empty");
                }
                else
                {
                    Console.WriteLine("dataset is not empty");
                }

                #endregion


                #region Inserting into Empty Table

                Console.WriteLine();

                queryParams = new Dictionary<string, SqlParameter>();
                queryParams.Add("@BigInt", new SqlParameter("@BigInt", 1000));
                queryParams.Add("@NVarChar50", new SqlParameter("@NVarChar50", "Test @NVarChar(50)"));
                queryParams.Add("@VarChar50", new SqlParameter("@VarChar50", "Test @VarChar50"));
                queryParams.Add("@DateTime2", new SqlParameter("@DateTime2", DateTime.Now.AddDays(1)));
                queryParams.Add("@Decimal", new SqlParameter("@Decimal", 12345.67m));
                queryParams.Add("@Bit", new SqlParameter("@Bit", true));

                int result = 0;
                result = dbHelper.ExecuteCommandByStoredProc("[dbo].[SP_InsertIntoTestDALTable]", queryParams);

                Console.WriteLine("Testing Inserting into Empty Table using Stored Proc: ");
                if (result == 0)
                {
                    Console.WriteLine("insertion failure");
                }
                else
                {
                    Console.WriteLine("insertion successful");
                }

                #endregion


                #region Querying From Filled Table

                Console.WriteLine();

                queryParams = new Dictionary<string, SqlParameter>();
                queryParams.Add("@BigInt", new SqlParameter("@BigInt", 1000));
                queryParams.Add("@NVarChar50", new SqlParameter("@NVarChar50", "Anything"));
                queryParams.Add("@VarChar50", new SqlParameter("@VarChar50", "Anything"));
                queryParams.Add("@DateTime2", new SqlParameter("@DateTime2", DateTime.Now));
                queryParams.Add("@Decimal", new SqlParameter("@Decimal", 123.12m));
                queryParams.Add("@Bit", new SqlParameter("@Bit", false));

                ds = null;
                ds = dbHelper.ExecuteQueryByStoredProc("[dbo].[SP_QueryTestDALTableWithParams]", queryParams);

                Console.WriteLine("Testing Querying from filled Table using Stored Proc: ");
                if (ds.Tables[0].Rows.Count == 0)
                {
                    Console.WriteLine("querying failure");
                }
                else
                {
                    Console.WriteLine("querying successful");
                    DataTable t = ds.Tables[0];
                    foreach (DataRow r in t.Rows)
                    {
                        Console.WriteLine(string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}",
                          r[0].ToString(),
                          r[1].ToString(),
                          r[2].ToString(),
                          r[3].ToString(),
                          r[4].ToString(),
                          r[5].ToString()));
                    }
                }

                #endregion


                #region Clearing Table (Reset)

                Console.WriteLine();

                queryParams = new Dictionary<string, SqlParameter>();

                result = 0;
                result = dbHelper.ExecuteCommandByStoredProc("[dbo].[SP_ClearTestDALTable]", queryParams);

                Console.WriteLine("Clearing Table using Stored Proc: ");
                if (result == 0)
                {
                    Console.WriteLine("delete failure");
                }
                else
                {
                    Console.WriteLine("delete successful");
                }

                #endregion

            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
