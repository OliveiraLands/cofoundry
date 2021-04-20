using Cofoundry.Core.Data.SimpleDatabase;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cofoundry.Core.DistributedLocks.Internal
{
    /// <summary>
    /// A distributed lock is a locking mechanism that can be used to
    /// prevents code from running in multiple concurrent processes, particularly 
    /// in scenarios where processes might be running in more than one applicatin
    /// or application instance. A central database is used to hold the locking
    /// information.
    /// </summary>
    /// <remarks>
    /// The distributed lock manager is used by the auto-update 
    /// process and will run before any db updates have been made, 
    /// therefore SQL has to be defined in code instead of stored
    /// proces. The auto update process created the initial db
    /// infrastructure for the distributed lock manager.
    /// </remarks>
    public class DistributedLockManager : IDistributedLockManager
    {
        private readonly ICofoundryDatabase _db;
        private readonly IDistributedLockDefinitionRepository _distributedLockDefinitionRepository;

        public DistributedLockManager(
            ICofoundryDatabase db,
            IDistributedLockDefinitionRepository distributedLockDefinitionRepository
            )
        {
            _db = db;
            _distributedLockDefinitionRepository = distributedLockDefinitionRepository;
        }

        /// <summary>
        /// Creates a lock for the specified definition using a unique
        /// lock id that represents the running process. You can
        /// query the returned DistributedLock instance to determine if 
        /// the lock was successful.
        /// </summary>
        /// <typeparam name="TDefinition">
        /// The definition type that conmtains the locking parameters that
        /// represent the process to be run.
        /// </typeparam>
        /// <returns>
        /// Returns the current state of the lock for the specified 
        /// definition. If the lock could not be made then the state will
        /// contain information about the existing lock, if the lock was 
        /// successful then the new lock will be returned. You can
        /// query the returned object to determine if the lock was successful.
        /// </returns>
        public async Task<DistributedLock> LockAsync<TDefinition>()
            where TDefinition : IDistributedLockDefinition
        {
            var distributedLockDefinition = _distributedLockDefinitionRepository.Get<TDefinition>();
            EntityNotFoundException.ThrowIfNull(distributedLockDefinition, typeof(TDefinition));

            var lockingId = Guid.NewGuid();
            var query = @" 
                replace into Cofoundry.DistributedLock (DistributedLockId, Name)  values (@DistributedLockId, @DistributedLockName);

                update Cofoundry.DistributedLock 
                   set LockingId = @LockingId, LockDate = Now(), ExpiryDate = DATE_ADD(now(), interval @TimeoutInSeconds second)
                 where DistributedLockId = @DistributedLockId2
                   and (LockingId is null or ExpiryDate < Now());

                select DistributedLockId, LockingId, LockDate, ExpiryDate 
                  from Cofoundry.DistributedLock
                 where DistributedLockId = @DistributedLockId3;
                ";

            var distributedLock = (await _db.ReadAsync(query,
                MapDistributedLock,
                new MySqlParameter("@DistributedLockId", distributedLockDefinition.DistributedLockId),
                new MySqlParameter("@DistributedLockId2", distributedLockDefinition.DistributedLockId),
                new MySqlParameter("@DistributedLockId3", distributedLockDefinition.DistributedLockId),
                new MySqlParameter("@DistributedLockName", distributedLockDefinition.Name),
                new MySqlParameter("@LockingId", lockingId),
                new MySqlParameter("@TimeoutInSeconds", distributedLockDefinition.Timeout.TotalSeconds)
                ))
                .SingleOrDefault();

            if (distributedLock == null)
            {
                throw new Exception($"Unknown error creating a distributed lock with a DistributedLockId of '{distributedLockDefinition.DistributedLockId}'");
            }

            distributedLock.RequestedLockingId = lockingId;

            return distributedLock;
        }

        /// <summary>
        /// Unlocks the specified distributed lock, freeing it up
        /// for other processes to use.
        /// </summary>
        /// <param name="distributedLock">
        /// The distributed lock entry to unlock. This should be the instance
        /// you received from a call to the LockAsync method.
        /// </param>
        public Task UnlockAsync(DistributedLock distributedLock)
        {
            if (distributedLock == null) throw new ArgumentNullException(nameof(distributedLock));

            var sql = @"
                update Cofoundry.DistributedLock 
                set LockingId = null, LockDate = null, ExpiryDate = null
                where LockingId = @LockingId and DistributedLockId = @DistributedLockId
                ";

            return _db.ExecuteAsync(sql, 
                new MySqlParameter("@LockingId", distributedLock.LockedByLockingId),
                new MySqlParameter("@DistributedLockId", distributedLock.DistributedLockId)
                );
        }

        private DistributedLock MapDistributedLock(MySqlDataReader reader)
        {
            var result = new DistributedLock();
            if (reader[nameof(result.DistributedLockId)] == null) return null;

            result.DistributedLockId = reader["DistributedLockId"] as string;
            result.LockedByLockingId = Guid.Parse( reader["LockingId"].ToString()) as Guid?;
            result.LockDate = reader["LockDate"] as DateTime?;
            result.ExpiryDate = reader["ExpiryDate"] as DateTime?;

            return result;
        }
    }
}
