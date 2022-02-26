using Dapper.Contrib.Extensions;
using System.Data.SqlClient;
using System.Transactions;

namespace DataLayer.Repositories
{
    public class RepositoryBase<T> : IRepository<T> where T : class, ICrudObject, new()
    {
        public virtual async Task<T> CreateAsync(T entity)
        {
            using var connection = await DbConnectionFactory.Instance.GetConnectionAsync();

            await connection.InsertAsync<T>(entity);

            return entity;
        }

        public virtual async Task<T> CreateAsync(T entity, SqlTransaction trans)
        {
            using var connection = await DbConnectionFactory.Instance.GetConnectionAsync();

            await connection.InsertAsync<T>(entity, trans);

            return entity;
        }

        public virtual async Task<IEnumerable<T>> CreateAsync(IEnumerable<T> entities)
        {
            using var connection = await DbConnectionFactory.Instance.GetConnectionAsync();

            List<Task<T>> saveTasks = new();

            foreach (var entity in entities)
                saveTasks.Add(CreateAsync(entity));

            await Task.WhenAll(saveTasks);

            List<T> ret = new();

            foreach (var task in saveTasks)
                ret.Add(task.Result);

            return ret;
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            using var connection = await DbConnectionFactory.Instance.GetConnectionAsync();

            return connection.DeleteAsync(entity).Result;
        }

        public async Task<bool> DeleteAsync(T entity, SqlTransaction trans)
        {
            using var connection = await DbConnectionFactory.Instance.GetConnectionAsync();

            return connection.DeleteAsync(entity, trans).Result;
        }

        public async Task<bool> DeleteAsync(IEnumerable<T> entities)
        {
            using var connection = await DbConnectionFactory.Instance.GetConnectionAsync();

            List<Task<bool>> deleteTasks = new();

            SqlTransaction transaction = connection.BeginTransaction();

            foreach (var entity in entities)
                deleteTasks.Add(DeleteAsync(entity, transaction));

            await Task.WhenAll(deleteTasks);

            bool ret = true;

            foreach (var task in deleteTasks)
            {
                if (!task.Result)
                {
                    ret = false;
                    break;
                }
            }

            try
            {
                transaction.Commit();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Commit failed.");

                try
                {
                    transaction.Rollback();
                }
                catch (Exception ex2)
                {
                    Console.WriteLine("Rollback failed.");
                }
            }
            return ret;
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            using var connection = await DbConnectionFactory.Instance.GetConnectionAsync();

            return await connection.DeleteAsync(new T() { ID = id });
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            using var connection = await DbConnectionFactory.Instance.GetConnectionAsync();

            return await connection.GetAllAsync<T>();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            using var connection = await DbConnectionFactory.Instance.GetConnectionAsync();

            return await connection.GetAsync<T>(id);
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            using var connection = await DbConnectionFactory.Instance.GetConnectionAsync();

            return await connection.UpdateAsync(entity);
        }

        public async Task<bool> UpdateAsync(IEnumerable<T> entities)
        {
            using var connection = await DbConnectionFactory.Instance.GetConnectionAsync();

            return await connection.UpdateAsync(entities);
        }
    }
}
