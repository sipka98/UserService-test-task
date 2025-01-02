using System;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Services
{
    public class DatabaseService
    {
        private readonly string _connectionString;

        public DatabaseService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void ExecuteNonQuery(string query, Action<SqlCommand> configureCommand = null)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    configureCommand?.Invoke(command);
                    command.ExecuteNonQuery();
                }
            }
        }

        public T ExecuteScalar<T>(string query, Action<SqlCommand> configureCommand = null)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    configureCommand?.Invoke(command);
                    return (T)command.ExecuteScalar();
                }
            }
        }

        public List<T> ExecuteReader<T>(string query, Func<SqlDataReader, T> readFunc, Action<SqlCommand> configureCommand = null)
        {
            var results = new List<T>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    configureCommand?.Invoke(command);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            results.Add(readFunc(reader));
                        }
                    }
                }
            }

            return results;
        }
    }
}