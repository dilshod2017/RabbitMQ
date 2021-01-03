using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using LinqToDB.Configuration;

namespace MicroRabbit.Users.Data.Context
{
    public class DbSetting : ILinqToDBSettings
    {
        private readonly string ConnectionString;

        public DbSetting(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public IEnumerable<IDataProviderSettings> DataProviders
        {
            get { yield break; }
        }

        public string? DefaultConfiguration => "SqlServer";
        public string? DefaultDataProvider => "SqlServer";

        public IEnumerable<IConnectionStringSettings> ConnectionStrings
        {
            get
            {
                yield return new ConnectionStringSettings()
                {
                    Name = "local",
                    ProviderName = "SqlServer",
                    ConnectionString = ConnectionString
                };
            }
        }
    }
}
