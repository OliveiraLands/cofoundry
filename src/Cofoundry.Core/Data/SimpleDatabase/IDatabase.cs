using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
<<<<<<< HEAD
using MySql.Data.MySqlClient;
=======
using Microsoft.Data.SqlClient;
>>>>>>> 6ecdeb969200643b332b1c86e2aba97ab0ff9ce6

namespace Cofoundry.Core.Data.SimpleDatabase
{
    /// <summary>
    /// Simple Db raw sql execution to avoid a dependency on any particular
    /// framework. Does not support connection management, connection resiliency 
    /// or retry logic.
    /// </summary>
    public interface IDatabase
    {
        /// <summary>
        /// Returns the database connection being used by this instance. Used
        /// to enlist in transactions.
        /// </summary>
        DbConnection GetDbConnection();

        /// <summary>
        /// Executes a sql command with the specified parameters.
        /// </summary>
        /// <param name="sql">Raw SQL string to execute against the database..</param>
        /// <param name="sqlParams">Any parameters to add to the command.</param>
<<<<<<< HEAD
        Task ExecuteAsync(string sql, params MySqlParameter[] sqlParams);
=======
        Task ExecuteAsync(string sql, params SqlParameter[] sqlParams);
>>>>>>> 6ecdeb969200643b332b1c86e2aba97ab0ff9ce6

        /// <summary>
        /// Executes raw sql and uses a reader with a mapping function to return
        /// typed results.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity returned from the query.</typeparam>
        /// <param name="sql">The raw sql to execute against the database.</param>
        /// <param name="map">A mapping function to use to map each row.</param>
        /// <param name="sqlParams">Any parameters to add to the command.</param>
        /// <returns>Collection of mapped entities.</returns>
<<<<<<< HEAD
        Task<ICollection<TEntity>> ReadAsync<TEntity>(string sql, Func<MySqlDataReader, TEntity> map, params MySqlParameter[] sqlParams);
=======
        Task<ICollection<TEntity>> ReadAsync<TEntity>(string sql, Func<SqlDataReader, TEntity> map, params SqlParameter[] sqlParams);
>>>>>>> 6ecdeb969200643b332b1c86e2aba97ab0ff9ce6
    }
}
