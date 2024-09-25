using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThuVien
{
    public class SQLClass
    {
        private SqlConnection _connection;

        public void createConnection(string pConnectionString)
        {
            _connection = new SqlConnection(pConnectionString);
        }

        public object ExecuteScalar(string pQuery)
        {
            object returnObject = null;

            try
            {
                // Create new command and set query
                SqlCommand command = _connection.CreateCommand();
                command.CommandText = pQuery;

                // Open connect
                if (_connection.State == ConnectionState.Closed)
                {
                    _connection.Open();
                }

                // Excute
                returnObject = command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return returnObject;
        }

        public DataTable ExecuteQuery(string query, Dictionary<string, object> parameters = null)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlCommand command = _connection.CreateCommand())
                {
                    command.CommandText = query;
                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                        {
                            command.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                        }
                    }

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dt);
                    }
                }

                return dt;
            }
            catch (Exception ex)
            {
                // Log hoặc xử lý lỗi
                throw new ApplicationException("Error executing query: " + ex.Message, ex);
            }
        }

        public int ExecuteNonQuery(string query, Dictionary<string, object> parameters = null)
        {
            int rowsAffected = 0;
            try
            {
                using (SqlCommand command = _connection.CreateCommand())
                {
                    command.CommandText = query;

                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                        {
                            command.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                        }
                    }

                    // Open connection if closed
                    if (_connection.State == ConnectionState.Closed)
                    {
                        _connection.Open();
                    }

                    rowsAffected = command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error executing non-query: " + ex.Message, ex);
            }

            return rowsAffected;
        }

        public bool Update(object pDataAdapter, DataRow pDataRow)
        {
            try
            {
                SqlDataAdapter dataAdapter = (SqlDataAdapter) pDataAdapter;

                SqlCommandBuilder builder = new SqlCommandBuilder();

                dataAdapter.UpdateCommand = builder.GetUpdateCommand();
                dataAdapter.DeleteCommand = builder.GetDeleteCommand();
                dataAdapter.InsertCommand = builder.GetInsertCommand();

                int i = dataAdapter.Update(new DataRow[] { pDataRow });

                return true;
            }
            catch { return false; }
        }

        public bool Update(object pDataAdapter, DataRow[] pDataRows)
        {
            try
            {
                SqlDataAdapter dataAdapter = (SqlDataAdapter)pDataAdapter;

                // Create new sqlcommand builder
                SqlCommandBuilder builder = new SqlCommandBuilder(dataAdapter);

                // Set update command for data adapter
                dataAdapter.UpdateCommand = builder.GetUpdateCommand();
                dataAdapter.DeleteCommand = builder.GetDeleteCommand();
                dataAdapter.InsertCommand = builder.GetInsertCommand();

                // Update data
                dataAdapter.Update(pDataRows);

                return true;
            }
            catch { return false; }
        }

        public bool Update(object pDataAdapter, DataTable pDataTable)
        {
            try
            {
                SqlDataAdapter dataAdapter = (SqlDataAdapter)pDataAdapter;

                // Create new sqlcommand builder
                SqlCommandBuilder builder = new SqlCommandBuilder(dataAdapter);

                // Set update command for data adapter
                dataAdapter.UpdateCommand = builder.GetUpdateCommand();
                dataAdapter.DeleteCommand = builder.GetDeleteCommand();
                dataAdapter.InsertCommand = builder.GetInsertCommand();

                // Update data
                dataAdapter.Update(pDataTable);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool TestConnection()
        {
            try
            {
                _connection.Open();

            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
