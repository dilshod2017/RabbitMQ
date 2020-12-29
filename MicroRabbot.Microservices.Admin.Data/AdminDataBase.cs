using System;
using System.Collections.Generic;
using System.Text;
using LinqToDB.Data;

namespace Admin.Data
{
    public class AdminDataBase : DataConnection
    {
        public AdminDataBase() : base("local")
        {
            
        }
    }
}
