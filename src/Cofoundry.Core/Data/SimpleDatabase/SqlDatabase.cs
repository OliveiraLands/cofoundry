using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Cofoundry.Core.Data.SimpleDatabase.Internal
{
    /// <summary>
    /// Simple MS SqlServer raw sql execution to avoid a dependency on any particular
    /// framework
    /// </summary>
    public class SqlDatabase : IDisposable, IDatabase
    {
        private readonly MySqlConnection _sqlConnection;

        public SqlDatabase(MySqlConnection sqlConnection)
        {
            _sqlConnection = sqlConnection;
        }

        /// <summary>
        /// Returns the database connection being used by this instance. Used
        /// to enlist in transactions.
        /// </summary>
        public DbConnection GetDbConnection()
        {
            return _sqlConnection;
        }

        /// <summary>
        /// Executes a sql command with the specified parameters.
        /// </summary>
        /// <param name="sql">Raw SQL string to execute against the database..</param>
        /// <param name="sqlParams">Any parameters to add to the command.</param>
        public async Task ExecuteAsync(string sql, params MySqlParameter[] sqlParams)
        {
            var isInitialStateClosed = IsClosed();
            if (isInitialStateClosed)
            {
                _sqlConnection.Open();
            }

            using (var sqlCmd = new MySqlCommand(sql, _sqlConnection))
            {
                if (sqlParams != null && sqlParams.Any())
                {
                    sqlCmd.Parameters.AddRange(sqlParams);
                }
                await sqlCmd.ExecuteNonQueryAsync();
            }

            if (isInitialStateClosed)
            {
                _sqlConnection.Close();
            }
        }

        /// <summary>
        /// Executes raw sql and uses a reader with a mapping function to return
        /// typed results.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity returned from the query.</typeparam>
        /// <param name="sql">The raw sql to execute against the database.</param>
        /// <param name="map">A mapping function to use to map each row.</param>
        /// <param name="sqlParams">Any parameters to add to the command.</param>
        /// <returns>Collection of mapped entities.</returns>
        public async Task<ICollection<TEntity>> ReadAsync<TEntity>(
            string sql, 
            Func<MySqlDataReader, TEntity> mapper, 
            params MySqlParameter[] sqlParams
            )
        {
            var isInitialStateClosed = IsClosed();
            if (isInitialStateClosed)
            {
                _sqlConnection.Open();
            }

            List<TEntity> result = new List<TEntity>();

            using (var sqlCmd = new MySqlCommand(sql, _sqlConnection))
            {
                if (sqlParams != null && sqlParams.Any())
                {
                    sqlCmd.Parameters.AddRange(sqlParams);
                }

                
                using (var reader = sqlCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var mapped = mapper(reader);
                        result.Add(mapped);
                    }
                }
            }

            if (isInitialStateClosed)
            {
                _sqlConnection.Close();
            }

            return result;
        }

        private bool IsClosed()
        {
            return _sqlConnection.State == ConnectionState.Closed;
        }

        public void Dispose()
        {
            if (_sqlConnection != null && _sqlConnection.State != ConnectionState.Closed)
            {
                _sqlConnection.Close();
            }
        }
    }
}
