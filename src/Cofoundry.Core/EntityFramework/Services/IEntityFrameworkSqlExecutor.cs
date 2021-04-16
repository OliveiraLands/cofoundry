using Microsoft.EntityFrameworkCore;
<<<<<<< HEAD
using MySql.Data.MySqlClient;
=======
>>>>>>> 6ecdeb969200643b332b1c86e2aba97ab0ff9ce6
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
<<<<<<< HEAD

=======
using Microsoft.Data.SqlClient;
>>>>>>> 6ecdeb969200643b332b1c86e2aba97ab0ff9ce6

namespace Cofoundry.Core.EntityFramework
{
    /// <summary>
    /// A service for executing raw SQL statements against an EF DataContext.
    /// </summary>
    public interface IEntityFrameworkSqlExecutor
    {
        #region ExecuteQuery

        /// <summary>
        /// Executes a stored procedure returning the results as an array forcing query execution.
        /// </summary>
        /// <param name="dbContext">EF DbContext to run the command against.</param>
        /// <param name="spName">Name of the stored procedure to run</param>
        /// <param name="sqlParams">Collection of SqlParameters to pass to the command</param>
        /// <returns>
        /// An array of the results of the query.
        /// </returns>
<<<<<<< HEAD
        Task<T[]> ExecuteQueryAsync<T>(DbContext dbContext, string spName, params MySqlParameter[] sqlParams)
=======
        Task<T[]> ExecuteQueryAsync<T>(DbContext dbContext, string spName, params SqlParameter[] sqlParams)
>>>>>>> 6ecdeb969200643b332b1c86e2aba97ab0ff9ce6
            where T : class;

        #endregion

        #region ExecuteScalar

        /// <summary>
        /// Executes a stored procedure returning a single result and forcing query 
        /// execution. This does not run in a transaction.
        /// </summary>
        /// <param name="dbContext">EF DbContext to run the command against.</param>
        /// <param name="spName">Name of the stored procedure to run</param>
        /// <param name="sqlParams">Collection of SqlParameters to pass to the command</param>
        /// <returns>
        /// The result of the query. Throws an exception if more than one result is returned.
        /// </returns>
<<<<<<< HEAD
        Task<T> ExecuteScalarAsync<T>(DbContext dbContext, string spName, params MySqlParameter[] sqlParams);
=======
        Task<T> ExecuteScalarAsync<T>(DbContext dbContext, string spName, params SqlParameter[] sqlParams);
>>>>>>> 6ecdeb969200643b332b1c86e2aba97ab0ff9ce6

        #endregion

        #region ExecuteCommand

        /// <summary>
        /// Executes a stored procedure or function returning either the number of rows affected or 
        /// optionally returning the value of the first output parameter passed in the parameters 
        /// collection. The command is executed in a transaction created via ITransactionFactory.
        /// </summary>
        /// <param name="dbContext">EF DbContext to run the command against.</param>
        /// <param name="spName">Name of the stored procedure to run</param>
        /// <param name="sqlParams">Collection of SqlParameters to pass to the command</param>
        /// <returns>
        /// Either the number of rows affected or optionally returning the value of the 
        /// first output parameter passed in the parameters collection.
        /// </returns>
<<<<<<< HEAD
        Task<object> ExecuteCommandAsync(DbContext dbContext, string spName, params MySqlParameter[] sqlParams);
=======
        Task<object> ExecuteCommandAsync(DbContext dbContext, string spName, params SqlParameter[] sqlParams);
>>>>>>> 6ecdeb969200643b332b1c86e2aba97ab0ff9ce6

        #endregion

        #region ExecuteCommandWithOutput

        /// <summary>
        /// Executes a stored procedure or function returning the value of the 
        /// output paramter. The output parameter is created for you so you do not need
        /// to specify it in the sqlParams collection. If more than one output parameter
        /// is specified only the first is returned. The generic type parameter is used
        /// as the output parameter type. The command is executed in a transaction created 
        /// via ITransactionFactory.
        /// </summary>
        /// <typeparam name="T">Type of the returned output parameter.</typeparam>
        /// <param name="dbContext">EF DbContext to run the command against.</param>
        /// <param name="spName">Name of the stored procedure to run</param>
        /// <param name="outputParameterName">Name to use when creating the output parameter</param>
        /// <param name="sqlParams">Collection of SqlParameters to pass to the command</param>
        /// <returns>
        /// The value of the first output parameter in the executed query.
        /// </returns>
<<<<<<< HEAD
        Task<T> ExecuteCommandWithOutputAsync<T>(DbContext dbContext, string spName, string outputParameterName, params MySqlParameter[] sqlParams);
=======
        Task<T> ExecuteCommandWithOutputAsync<T>(DbContext dbContext, string spName, string outputParameterName, params SqlParameter[] sqlParams);
>>>>>>> 6ecdeb969200643b332b1c86e2aba97ab0ff9ce6

        #endregion
    }
}
