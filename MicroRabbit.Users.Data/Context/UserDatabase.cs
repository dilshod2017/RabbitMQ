using System;
using System.Collections.Generic;
using System.Text;
using MicroRabbit.Users.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroRabbit.Users.Data.Context
{
    public class UserDatabase : DbContext
    {
        public UserDatabase(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
    }
}
