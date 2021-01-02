using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using MicroRabbit.Domain.Core.IRepository;
 using MicroRabbit.Users.Domain.Models;

namespace MicroRabbit.Users.Data.Repository
{
    public class UserRepository : Repository<User>
    {
        public UserRepository(IDisposable database) : base(database)
        {
            
        }
    }
}
