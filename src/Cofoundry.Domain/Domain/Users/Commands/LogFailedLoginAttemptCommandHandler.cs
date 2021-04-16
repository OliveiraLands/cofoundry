using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cofoundry.Domain.CQS;
using Cofoundry.Core.EntityFramework;
<<<<<<< HEAD
using Cofoundry.Core;
using Cofoundry.Domain.Data;
using MySql.Data.MySqlClient;
=======
using Microsoft.Data.SqlClient;
using Cofoundry.Core;
using Cofoundry.Domain.Data;
>>>>>>> 6ecdeb969200643b332b1c86e2aba97ab0ff9ce6

namespace Cofoundry.Domain.Internal
{
    public class LogFailedLoginAttemptCommandHandler
        : ICommandHandler<LogFailedLoginAttemptCommand>
        , IIgnorePermissionCheckHandler
    {
        #region constructor

        private readonly CofoundryDbContext _dbContext;
        private readonly IEntityFrameworkSqlExecutor _sqlExecutor;
        private readonly IClientConnectionService _clientConnectionService;

        public LogFailedLoginAttemptCommandHandler(
            CofoundryDbContext dbContext,
            IEntityFrameworkSqlExecutor sqlExecutor,
            IClientConnectionService clientConnectionService
            )
        {
            _dbContext = dbContext;
            _sqlExecutor = sqlExecutor;
            _clientConnectionService = clientConnectionService;
        }
        #endregion

        public async Task ExecuteAsync(LogFailedLoginAttemptCommand command, IExecutionContext executionContext)
        {
            var connectionInfo = _clientConnectionService.GetConnectionInfo();

            await _sqlExecutor.ExecuteCommandAsync(_dbContext,
                "Cofoundry.FailedAuthticationAttempt_Add",
<<<<<<< HEAD
                new MySqlParameter("UserAreaCode", command.UserAreaCode),
                new MySqlParameter("Username", TextFormatter.Limit(command.Username, 150)),
                new MySqlParameter("IPAddress", connectionInfo.IPAddress),
                new MySqlParameter("DateTimeNow", executionContext.ExecutionDate)
=======
                new SqlParameter("UserAreaCode", command.UserAreaCode),
                new SqlParameter("Username", TextFormatter.Limit(command.Username, 150)),
                new SqlParameter("IPAddress", connectionInfo.IPAddress),
                new SqlParameter("DateTimeNow", executionContext.ExecutionDate)
>>>>>>> 6ecdeb969200643b332b1c86e2aba97ab0ff9ce6
                );
        }
    }
}
