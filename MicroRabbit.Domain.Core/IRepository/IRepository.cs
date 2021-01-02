using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MicroRabbit.Domain.Core.IRepository
{
    public interface IRepository<T> where T : class
    {
       Task<IEnumerable<T>> GetAll(Func<T, bool> predicate = null);
        Task<T> Get(Func<T, bool> predicate = null);
        Task<bool> Insert(T item);
        Task<IEnumerable<bool>> InsertMany(IEnumerable<T> list);
        Task<bool> Update(T item);
        Task<IEnumerable<bool>> UpdateMany(IEnumerable<T> list);
        Task<T> Delete(Func<T, bool> predicate = null);
        Task<IEnumerable<T>> DeleteMany(Func<T, bool> predicate = null);
    }
}
