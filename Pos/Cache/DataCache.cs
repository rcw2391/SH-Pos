using DataLayer;
using DataLayer.Models;
using Pos.Cache;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pos
{
    public sealed class DataCache
    {
        private static readonly DataCache _instance = new();

        static DataCache() { }
        private DataCache() { }

        public static DataCache Instance => _instance;
        private IUnitOfWork _uow;

        public async Task<bool> InitAsync(IUnitOfWork uow)
        {
            _uow = uow;

            Customers = new CacheList<Customer>(uow);
            Users = new CacheList<User>(uow);

            List<Task<bool>> tasks = new();

            tasks.Add(Customers.Initialize());
            tasks.Add(Users.Initialize());

            await Task.WhenAll(tasks);

            return tasks.Where(t => t.Result).ToList().Count == tasks.Count;
        }

        public ICacheList<Customer> Customers { get; private set; }
        public ICacheList<User> Users { get; private set; }
    }
}
