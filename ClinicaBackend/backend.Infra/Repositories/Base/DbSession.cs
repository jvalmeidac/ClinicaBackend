using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace backend.Infra.Repositories.Base
{
    public sealed class DbSession : IDisposable
    {
        private IConfiguration _configuration;
        public IDbConnection Connection { get; }
        public IDbTransaction Transaction { get; set; }

        public DbSession(IConfiguration configuration)
        {
            _configuration = configuration;

            Connection = new MySqlConnection(
                    _configuration.GetConnectionString("MySQLString")
                );
            Connection.Open();
        }

        public void Dispose() => Connection?.Dispose();
    }
}
