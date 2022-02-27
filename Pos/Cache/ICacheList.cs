using DataLayer;
using DataLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pos.Cache
{
    public interface ICacheList<T> : IEnumerable<T> where T : ICacheable, ICrudObject
    {
        T Add(T entity);
        void Delete(T entity);
        Task<IEnumerable<T>> SaveAsync();
        Task<bool> Initialize();
    }
}
