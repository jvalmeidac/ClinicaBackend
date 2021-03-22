using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace backend.Infra.Repositories.Base
{
    public sealed class DbSession : IDisposable
    {
        public IDbConnection Connection { get; }
        public IDbTransaction Transaction { get; set; }

        public DbSession()
        {
            Connection = new MySqlConnection(
                    "Server=projetotcc.csep8uv6hcgw.sa-east-1.rds.amazonaws.com;Database=projetotcc;Uid=admin;Pwd=r4ieqspFYfneYT0KF2cK;"
                );
            Connection.Open();
        }

        public void Dispose() => Connection?.Dispose();
    }
}
