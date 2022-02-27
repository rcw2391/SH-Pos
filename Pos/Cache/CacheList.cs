using Common;
using DataLayer;
using DataLayer.Models;
using DataLayer.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Pos.Cache
{
    public class CacheList<T> : ICacheList<T> where T : class, ICacheable, ICrudObject
    {

        private IRepository<T> _repo;
        
        public CacheList(IUnitOfWork uow)
        {
            _repo = uow.GetRepository<T>();
        }
        
        private ObservableCollection<T> _collection = new();
        
        public T Add(T entity)
        {
            entity.ID = PersistenceActionID.Insert;

            _collection.Add(entity);

            return entity;
        }

        public void Delete(T entity)
        {
            entity.IsDeleted = true;
        }

        public IEnumerator<T> GetEnumerator() => _collection.GetEnumerator();

        public async Task<bool> Initialize()
        {
            if (_repo is null) throw new InvalidOperationException("Repository is null.");

            if (_collection.Count > 0) return true;

            _collection = new(await _repo.GetAllAsync());

            return _collection.Count > 0;
        }

        public async Task<IEnumerable<T>> SaveAsync()
        {
            if (_repo is null) throw new InvalidOperationException("Repository is null.");
            
            List<T> toInsert = new(_collection.Where(x => x.ID == PersistenceActionID.Insert));
            List<T> toDelete = new(_collection.Where(x => x.IsDeleted));
            List<T> toUpdate = new(_collection.Where(x => x.ID != PersistenceActionID.Insert));

            List<Task> tasks = new();

            tasks.Add(_repo.CreateAsync(toInsert));
            tasks.Add(_repo.DeleteAsync(toDelete));
            tasks.Add(_repo.UpdateAsync(toUpdate));

            await Task.WhenAll(tasks);

            return _collection;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
