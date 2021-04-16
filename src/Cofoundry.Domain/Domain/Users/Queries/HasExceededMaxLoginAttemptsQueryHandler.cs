using System;
using System.Collections.Generic;
<<<<<<< HEAD
=======
using Microsoft.Data.SqlClient;
>>>>>>> 6ecdeb969200643b332b1c86e2aba97ab0ff9ce6
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cofoundry.Domain.Data;
using Cofoundry.Domain.CQS;
using Cofoundry.Core.EntityFramework;
<<<<<<< HEAD
using MySql.Data.MySqlClient;
=======
>>>>>>> 6ecdeb969200643b332b1c86e2aba97ab0ff9ce6

namespace Cofoundry.Domain.Internal
{
    public class HasExceededMaxLoginAttemptsQueryHandler 
        : IQueryHandler<HasExceededMaxLoginAttemptsQuery, bool>
        , IIgnorePermissionCheckHandler
    {
        #region constructor

        private readonly CofoundryDbContext _dbContext;
        private readonly IEntityFrameworkSqlExecutor _sqlExecutor;
        private readonly AuthenticationSettings _authenticationSettings;
        private readonly IClientConnectionService _clientConnectionService;

        public HasExceededMaxLoginAttemptsQueryHandler(
            CofoundryDbContext dbContext,
            IEntityFrameworkSqlExecutor sqlExecutor,
            AuthenticationSettings authenticationSettings,
            IClientConnectionService clientConnectionService
            )
        {
            _dbContext = dbContext;
            _sqlExecutor = sqlExecutor;
            _authenticationSettings = authenticationSettings;
            _clientConnectionService = clientConnectionService;
        }
        
        #endregion

        #region execution

        public async Task<bool> ExecuteAsync(HasExceededMaxLoginAttemptsQuery query, IExecutionContext executionContext)
        {
            var connectionInfo = _clientConnectionService.GetConnectionInfo();

            var isValid = await _sqlExecutor.ExecuteScalarAsync<int>(_dbContext,
                "Cofoundry.FailedAuthticationAttempt_IsAttemptValid",
<<<<<<< HEAD
                new MySqlParameter("UserAreaCode", query.UserAreaCode),
                new MySqlParameter("Username", query.Username.Trim()),
                new MySqlParameter("IPAddress", connectionInfo.IPAddress),
                new MySqlParameter("DateTimeNow", executionContext.ExecutionDate),
                new MySqlParameter("MaxIPAttempts", _authenticationSettings.MaxIPAttempts),
                new MySqlParameter("MaxUsernameAttempts", _authenticationSettings.MaxUsernameAttempts),
                new MySqlParameter("MaxIPAttemptsBoundaryInMinutes", _authenticationSettings.MaxIPAttemptsBoundaryInMinutes),
                new MySqlParameter("MaxUsernameAttemptsBoundaryInMinutes", _authenticationSettings.MaxUsernameAttemptsBoundaryInMinutes)
=======
                new SqlParameter("UserAreaCode", query.UserAreaCode),
                new SqlParameter("Username", query.Username.Trim()),
                new SqlParameter("IPAddress", connectionInfo.IPAddress),
                new SqlParameter("DateTimeNow", executionContext.ExecutionDate),
                new SqlParameter("MaxIPAttempts", _authenticationSettings.MaxIPAttempts),
                new SqlParameter("MaxUsernameAttempts", _authenticationSettings.MaxUsernameAttempts),
                new SqlParameter("MaxIPAttemptsBoundaryInMinutes", _authenticationSettings.MaxIPAttemptsBoundaryInMinutes),
                new SqlParameter("MaxUsernameAttemptsBoundaryInMinutes", _authenticationSettings.MaxUsernameAttemptsBoundaryInMinutes)
>>>>>>> 6ecdeb969200643b332b1c86e2aba97ab0ff9ce6
                );

            return isValid != 1;
        }

        #endregion
    }

}
