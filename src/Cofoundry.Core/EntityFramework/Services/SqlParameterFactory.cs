using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
<<<<<<< HEAD
using MySql.Data.MySqlClient;
using Pomelo.EntityFrameworkCore.MySql;
=======
using Microsoft.Data.SqlClient;
>>>>>>> 6ecdeb969200643b332b1c86e2aba97ab0ff9ce6

namespace Cofoundry.Core.EntityFramework.Internal
{
    public class SqlParameterFactory : ISqlParameterFactory
    {
<<<<<<< HEAD
        private static readonly Dictionary<Type, DbType> _dbTypeMap = new Dictionary<Type, DbType>()
        {
            { typeof(string),  DbType.String },
            { typeof(int),  DbType.Int32 },
            { typeof(decimal),  DbType.Decimal },
            { typeof(long),  DbType.Int64 },
            { typeof(bool),  DbType.Boolean },
            { typeof(DateTime),  DbType.DateTime2 },
            { typeof(float),  DbType.Double },
            { typeof(Guid),  DbType.Guid }
        };

        public MySqlParameter CreateOutputParameterByType(string name, Type t)
        {
            var outputParam = new MySqlParameter(name, DBNull.Value);
=======
        private static readonly Dictionary<Type, SqlDbType> _dbTypeMap = new Dictionary<Type, SqlDbType>()
        {
            { typeof(string),  SqlDbType.NVarChar },
            { typeof(int),  SqlDbType.Int },
            { typeof(decimal),  SqlDbType.Decimal },
            { typeof(long),  SqlDbType.BigInt },
            { typeof(bool),  SqlDbType.Bit },
            { typeof(DateTime),  SqlDbType.DateTime2 },
            { typeof(float),  SqlDbType.Float },
            { typeof(Guid),  SqlDbType.UniqueIdentifier }
        };

        public SqlParameter CreateOutputParameterByType(string name, Type t)
        {
            var outputParam = new SqlParameter(name, DBNull.Value);
>>>>>>> 6ecdeb969200643b332b1c86e2aba97ab0ff9ce6
            outputParam.Direction = ParameterDirection.Output;

            // If this is a non-null value nullable type, return the converted base type
            var type = Nullable.GetUnderlyingType(t) ?? t;

            if (_dbTypeMap.ContainsKey(type))
            {
<<<<<<< HEAD
                outputParam.DbType = _dbTypeMap[type];
            }

            switch (outputParam.DbType)
            {
                case DbType.String:
                case DbType.AnsiString:
=======
                outputParam.SqlDbType = _dbTypeMap[type];
            }

            switch (outputParam.SqlDbType)
            {
                case SqlDbType.NVarChar:
                case SqlDbType.VarChar:
>>>>>>> 6ecdeb969200643b332b1c86e2aba97ab0ff9ce6
                    // Size required for variable size output types
                    outputParam.Size = -1;
                    break;
            }

            return outputParam;
        }
    }
}
