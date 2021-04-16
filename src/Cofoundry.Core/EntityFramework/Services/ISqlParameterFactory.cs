using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Cofoundry.Core.EntityFramework
{
    public interface ISqlParameterFactory
    {
        MySqlParameter CreateOutputParameterByType(string name, Type t);
    }
}
