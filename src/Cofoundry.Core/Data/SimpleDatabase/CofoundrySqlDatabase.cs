<<<<<<< HEAD
﻿using MySql.Data.MySqlClient;
using System;
=======
﻿using System;
>>>>>>> 6ecdeb969200643b332b1c86e2aba97ab0ff9ce6
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
<<<<<<< HEAD
=======
using Microsoft.Data.SqlClient;
>>>>>>> 6ecdeb969200643b332b1c86e2aba97ab0ff9ce6

namespace Cofoundry.Core.Data.SimpleDatabase.Internal
{
    /// <summary>
    /// Simple Db raw sql execution on the Cofoundry database which by
    /// default used the shared Cofoundry connection with a scoped lifetime. 
    /// This can be used in place of an EF DbContext to avoid a dependency on 
    /// any particular framework. Does not support connection resiliency 
    /// or retry logic.
    /// </summary>
    public class CofoundrySqlDatabase : IDisposable, ICofoundryDatabase
    {
        private readonly SqlDatabase _sqlDatabase;

        public CofoundrySqlDatabase(
            ICofoundryDbConnectionManager _cofoundryDbConnectionFactory
            )
        {
            var dbConnection = _cofoundryDbConnectionFactory.GetShared();

<<<<<<< HEAD
            if (dbConnection is MySqlConnection)
            {
                _sqlDatabase = new SqlDatabase((MySqlConnection)dbConnection);
=======
            if (dbConnection is SqlConnection)
            {
                _sqlDatabase = new SqlDatabase((SqlConnection)dbConnection);
>>>>>>> 6ecdeb969200643b332b1c86e2aba97ab0ff9ce6
            }
            else
            {
                throw new NotSupportedException($"{nameof(SqlDatabase)} only supports SqlConnection. {nameof(ICofoundryDbConnectionManager)} returned connection of type {dbConnection.GetType().FullName}");
            }
        }

        /// <summary>
        /// Returns the database connection being used by this instance. Used
        /// to enlist in transactions.
        /// </summary>
        public DbConnection GetDbConnection()
        {
            return _sqlDatabase.GetDbConnection();
        }

        /// <summary>
        /// Executes a sql command with the specified parameters.
        /// </summary>
        /// <param name="sql">Raw SQL string to execute against the database.</param>
        /// <param name="sqlParams">Any parameters to add to the command.</param>
<<<<<<< HEAD
        public Task ExecuteAsync(string sql, params MySqlParameter[] sqlParams)
=======
        public Task ExecuteAsync(string sql, params SqlParameter[] sqlParams)
>>>>>>> 6ecdeb969200643b332b1c86e2aba97ab0ff9ce6
        {
            return _sqlDatabase.ExecuteAsync(sql, sqlParams);
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
        public Task<ICollection<TEntity>> ReadAsync<TEntity>(
            string sql, 
<<<<<<< HEAD
            Func<MySqlDataReader, TEntity> mapper, 
            params MySqlParameter[] sqlParams
=======
            Func<SqlDataReader, TEntity> mapper, 
            params SqlParameter[] sqlParams
>>>>>>> 6ecdeb969200643b332b1c86e2aba97ab0ff9ce6
            )
        {

            return _sqlDatabase.ReadAsync(sql, mapper, sqlParams);
        }

        public void Dispose()
        {
            if (_sqlDatabase != null)
            {
                _sqlDatabase.Dispose();
            }
        }

    }
}
