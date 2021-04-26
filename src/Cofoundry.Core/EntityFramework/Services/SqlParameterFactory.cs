using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using MySql.Data.MySqlClient;
using Pomelo.EntityFrameworkCore.MySql;

namespace Cofoundry.Core.EntityFramework.Internal
{
    public class SqlParameterFactory : ISqlParameterFactory
    {
        private static readonly Dictionary<Type, DbType> _dbTypeMap = new Dictionary<Type, DbType>()
        {
            { typeof(string),  DbType.String },
            { typeof(int),  DbType.Int32 },
            { typeof(decimal),  DbType.Decimal },
            { typeof(long),  DbType.Int64 },
            { typeof(bool),  DbType.Boolean },
            { typeof(DateTime),  DbType.DateTime2 },
            { typeof(float),  DbType.Double },
            { typeof(Guid),  DbType.String }
        };

        public MySqlParameter CreateOutputParameterByType(string name, Type t)
        {
            var outputParam = new MySqlParameter(name, DBNull.Value);
            outputParam.Direction = ParameterDirection.Output;

            // If this is a non-null value nullable type, return the converted base type
            var type = Nullable.GetUnderlyingType(t) ?? t;

            if (_dbTypeMap.ContainsKey(type))
            {
                outputParam.DbType = _dbTypeMap[type];
            }

            switch (outputParam.DbType)
            {
                case DbType.String:
                case DbType.AnsiString:
                    // Size required for variable size output types
                    outputParam.Size = -1;
                    break;
            }

            return outputParam;
        }
    }
}
