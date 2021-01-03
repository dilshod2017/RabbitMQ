using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinqToDB;
using LinqToDB.Data;

namespace MicroRabbit.Domain.Core.IRepository
{
    public abstract class Repository<T> : IRepository<T> where T: class
    {
        protected Type _Ttype { get; } = typeof(T);

        private IDisposable _db { get; }
        // private static CancellationTokenSource CTS () => new CancellationTokenSource();
        protected Repository(IDisposable db)
        {
            _db = db;
        }

        public async Task<IEnumerable<T>> GetAll(Func<T, bool> predicate = null) => await GetAllAsync(predicate);
        public async Task<T> Get(Func<T, bool> predicate = null) => await GetAsync(predicate);
        public async Task<bool> Update(T item) => await UpdateAsync(item);
        public async Task<IEnumerable<bool>> UpdateMany(IEnumerable<T> list) => await UpdateManyAsync(list);
        public async Task<bool> Insert(T item) => await InsertAsync(item);
        public async Task<IEnumerable<bool>> InsertMany(IEnumerable<T> list) => await InserManyAsync(list);
        public async Task<T> Delete(Func<T, bool> predicate = null) => await DeleteAsync(predicate);
        public async Task<IEnumerable<T>> DeleteMany(Func<T, bool> predicate = null) =>
            await DeleteManyAsync(predicate);

        protected async Task<IEnumerable<T>> GetAllAsync(Func<T, bool> predicate = null)
        {
            try
            {
                using var database = _db;
                var method = typeof(DataConnection).GetMethods().FirstOrDefault(s => s.Name ==
                    nameof(DataConnection.GetTable));
                var genericMethod = method?.MakeGenericMethod(_Ttype);
                var list =await ((ITable<T>) genericMethod.Invoke(database, null)).ToArrayAsync();
                return predicate != null ? list.Where(predicate) : list;
            }
            catch (Exception a)
            {
                Console.WriteLine(a);
                throw;
            }

        }

        private async Task<T> GetAsync(Func<T, bool> predicate = null)
        {
            if (predicate is null) return null;
            var p = await GetAllAsync(predicate);
            var enumerable = p.ToList();
            return enumerable.Any() ? enumerable.FirstOrDefault() : null;
        }


        protected async Task<bool> InsertAsync(T item)
        {
            return false;
        }


        protected async Task<IEnumerable<bool>> InserManyAsync(IEnumerable<T> list)
        {
            return new List<bool>();
        }


        protected async Task<bool> UpdateAsync(T item)
        {
            return false;
        }


        protected async Task<IEnumerable<bool>> UpdateManyAsync(IEnumerable<T> list)
        {
            return new List<bool>();
        }


        protected async Task<T> DeleteAsync(Func<T, bool> predicate)
        {
            return null;
        }

    

        protected async Task<IEnumerable<T>> DeleteManyAsync(Func<T, bool> predicate)
        {
            return new List<T>();
        }
    }
}
