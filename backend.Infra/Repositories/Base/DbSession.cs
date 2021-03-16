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
                    "Server=localhost;Database=projetotcc;Uid=root;Pwd=root;"
                );
            Connection.Open();
        }

        public void Dispose() => Connection?.Dispose();
    }
}
