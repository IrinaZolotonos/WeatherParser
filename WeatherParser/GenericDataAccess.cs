using System.Data;
using System.Data.Common;

namespace WeatherParser
{
    public static class GenericDataAccess
    {
        public static DbCommand CreateCommand()
        {
            string dataProviderName = ConfigurationSettings.DbProviderName;
            string connectionString = ConfigurationSettings.DbConnectionString;

            DbProviderFactory factory = DbProviderFactories.
            GetFactory(dataProviderName);

            DbConnection conn = factory.CreateConnection();
            conn.ConnectionString = connectionString;

            DbCommand comm = conn.CreateCommand();
            comm.CommandType = CommandType.StoredProcedure;
            comm.CommandTimeout = 180;
            return comm;
        }

        public static int ExecuteNonQuery(DbCommand command)
        {
            int affectedRows = -1;
            try
            {
                command.Connection.Open();
                affectedRows = command.ExecuteNonQuery();
            }
            catch
            {
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
            DbCommand comm = CreateCommand();
            comm.CommandText = StoredProcedure;
            DbParameter param;
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
                result = GenericDataAccess.ExecuteNonQuery(comm);
            }
            catch 
            {
            }
            return (result != -1);
        }
    }
}
