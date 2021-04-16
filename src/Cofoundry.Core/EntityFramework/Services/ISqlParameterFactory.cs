using System;
using System.Collections.Generic;
using System.Threading.Tasks;
<<<<<<< HEAD
using MySql.Data.MySqlClient;
=======
using Microsoft.Data.SqlClient;
>>>>>>> 6ecdeb969200643b332b1c86e2aba97ab0ff9ce6

namespace Cofoundry.Core.EntityFramework
{
    public interface ISqlParameterFactory
    {
<<<<<<< HEAD
        MySqlParameter CreateOutputParameterByType(string name, Type t);
=======
        SqlParameter CreateOutputParameterByType(string name, Type t);
>>>>>>> 6ecdeb969200643b332b1c86e2aba97ab0ff9ce6
    }
}
