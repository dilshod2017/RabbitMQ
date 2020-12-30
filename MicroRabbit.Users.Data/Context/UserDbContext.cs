using System;
using System.Collections.Generic;
using System.Text;
using MicroRabbit.Users.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroRabbit.Users.Data.Context
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
    }
}
