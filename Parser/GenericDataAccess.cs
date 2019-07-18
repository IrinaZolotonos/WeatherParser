using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using MySql.Data.MySqlClient;

namespace WeatherParser
{
    public static class GenericDataAccess
    {
        public static MySqlCommand CreateCommand()
        {
            string dataProviderName = ConfigurationSettings.DbProviderName;
            string connectionString = ConfigurationSettings.DbConnectionString;

            MySqlConnection con = new MySqlConnection(connectionString);

            MySqlCommand comm = new MySqlCommand("SET NAMES cp1251", con);
            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandTimeout = 180;
            return comm;
        }

        public static int ExecuteNonQuery(MySqlCommand command)
        {
            int affectedRows = -1;
            try
            {
                command.Connection.Open();
                affectedRows = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
            finally
            {
                command.Connection.Close();
            }
            return affectedRows;
        }


        public static bool Execite(string StoredProcedure, string[] ParameterName, object[] ParameterValue)
        {
            MySqlCommand comm = CreateCommand();
            comm.CommandText = StoredProcedure;
            MySqlParameter param;
            if (ParameterName.Length > 0 && ParameterValue.Length > 0 && ParameterName.Length == ParameterValue.Length)
            {
                for (int i = 0; i < ParameterName.Length; i++)
                {
                    param = comm.CreateParameter();
                    param.ParameterName = ParameterName[i];
                    param.Value = ParameterValue[i];
                    comm.Parameters.Add(param);
                }
            }
            int result = -1;
            try
            {
                result = ExecuteNonQuery(comm);
            }
            catch  (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return (result != -1);
        }

        public static List<DataRow> ExecuteSelectCommand(MySqlCommand command)
        {
            System.Data.DataTable table = null;
            try
            {
                command.Connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                table = new System.Data.DataTable();
                table.Load(reader);
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                try
                {
                    command.Connection.Close();
                }
                catch { }
            }
            return table.AsEnumerable().ToList();
        }

        public static List<DataRow> ExeciteSelect(string StoredProcedure, string[] ParameterName,
                            object[] ParameterValue)
        {
            MySqlCommand comm = CreateCommand();
            comm.CommandText = StoredProcedure;
            MySqlParameter param;
            if (ParameterName.Length > 0 && ParameterValue.Length > 0 && ParameterName.Length == ParameterValue.Length)
            {
                for (int i = 0; i < ParameterName.Length; i++)
                {
                    param = comm.CreateParameter();
                    param.ParameterName = ParameterName[i];
                    param.Value = ParameterValue[i];
                    comm.Parameters.Add(param);
                }
            }
            return ExecuteSelectCommand(comm);
        }

    }
}
