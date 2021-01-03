using System;
using System.Collections.Generic;
using System.Text;
using LinqToDB.Configuration;

namespace MicroRabbit.Users.Data.Context
{
    public class ConnectionStringSettings : IConnectionStringSettings
    {
        public string ConnectionString { get; set; }
        public string Name { get; set; }
        public string? ProviderName { get; set; }
        public bool IsGlobal => false;
    }
}
