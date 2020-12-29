using LinqToDB.Configuration;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Admin.Data
{
     public class DbSettings : ILinqToDBSettings
    {
        public string ConnectionString { get; }

        public DbSettings(string connectionString)
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

