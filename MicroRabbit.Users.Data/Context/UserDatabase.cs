using System;
using LinqToDB;
using LinqToDB.Data;
using MicroRabbit.Users.Domain.Models;

namespace MicroRabbit.Users.Data.Context
{

    public class UserDatabase : DataConnection
    {
        public UserDatabase() : base("local")
        {

        }

        public ITable<User> UserTables => GetTable<User>();
    }
    public static class UserDataBaseFactory
    {

        public static Lazy<UserDatabase> NewUserDataBase => new Lazy<UserDatabase>();
    }
}
