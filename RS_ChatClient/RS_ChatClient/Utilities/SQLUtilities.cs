using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS_ChatClient.Utilities
{
    public static class SQLUtilities
    {
        public static int? ExecuteSQLScalarQuery(SqlConnection sqlConnection, string procName, SqlParameter[] parameters)
        {
            int? value = 0;
            using (SqlCommand command = new SqlCommand(procName, sqlConnection))
            {
                // don't forget the command type or the parameters
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddRange(parameters);
                try
                {
                    sqlConnection.Open();
                    var holder = command.ExecuteScalar();
                    value = (int?)holder;
                }
                catch (SqlException sqlException)
                {
                    if (sqlConnection.State == System.Data.ConnectionState.Closed)
                    {
                        throw new Exception("The database could not be connected to.");
                    }
                }
                catch(InvalidCastException castException)
                {
                    value = null;
                }
                finally
                {
                    if (sqlConnection.State == System.Data.ConnectionState.Open)
                    {
                        sqlConnection.Close();
                    }
                }
            }
            return value;
        }

        public static DataTable ExecuteSQLTabularQuery(SqlConnection sqlConnection, string procName, SqlParameter[] parameters)
        {
            DataTable table = new DataTable();
            using (SqlCommand command = new SqlCommand(procName, sqlConnection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddRange(parameters);
                try
                {
                    sqlConnection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(table);
                }
                catch (SqlException sqlException)
                {
                    if (sqlConnection.State == ConnectionState.Closed)
                    {
                       throw new Exception("The database could not be connected to.");
                    }
                }
                finally
                {
                    if (sqlConnection.State == System.Data.ConnectionState.Open)
                    {
                        sqlConnection.Close();
                    }
                }
            }
            return table;
        }
    }
}
