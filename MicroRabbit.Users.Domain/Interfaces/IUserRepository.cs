using System;
using System.Collections.Generic;
using System.Text;
using MicroRabbit.Users.Domain.Models;

namespace MicroRabbit.Users.Domain.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();
    }
}
