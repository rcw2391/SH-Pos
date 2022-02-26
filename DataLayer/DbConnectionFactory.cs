using System.Data.SqlClient;

namespace DataLayer
{
    public sealed class DbConnectionFactory
    {
        private static readonly DbConnectionFactory _instance = new DbConnectionFactory();

        static DbConnectionFactory(){ }
        private DbConnectionFactory(){ }

        public static DbConnectionFactory Instance => _instance;

        private string _connectionString;

        public async Task<bool> SetConnectionStringAsync(string s)
        {
            _connectionString = s;

            bool canConnect;

            try
            {
                await using (var conn = new SqlConnection(s))
                {
                    await conn.OpenAsync();
                    canConnect = true;
                    await conn.CloseAsync();
                }
            }
            catch (Exception ex)
            {
                canConnect = false;
            }

            return canConnect;
        }

        public async Task<SqlConnection> GetConnectionAsync()
        {
            if (_connectionString is null)
                throw new InvalidOperationException("Connection not established. Please set connection string.");

            try
            {
                var connection = new SqlConnection(_connectionString);

                await connection.OpenAsync();

                return connection;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            throw new InvalidOperationException("Connection failed.");
        }
    }
}
