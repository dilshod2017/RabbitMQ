using System;
using System.Collections.Generic;
using System.Text;
using LinqToDB.Configuration;

namespace Admin.Data
{
    public class ConnectionStringSettings : IConnectionStringSettings
    {
        public string ConnectionString { get; set; }
        public string Name { get; set; }
        public string ProviderName { get; set; }
        public bool IsGlobal { get; set; } = false;
    }
}
