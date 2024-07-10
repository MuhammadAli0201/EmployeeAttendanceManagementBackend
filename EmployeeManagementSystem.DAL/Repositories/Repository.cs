using EmployeeManagementSystem.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.DAL.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        public Repository(AppDbContext appDbContext)
        {
            this._context = appDbContext;
        }

        public virtual async Task<T> Add(T item)
        {
            try
            {
                await _context.Set<T>().AddAsync(item);
                await _context.SaveChangesAsync();
                return item;
            }
            catch
            {
                throw;
            }
        }

        public virtual async Task<List<T>> Add(List<T> items)
        {
            try
            {
                foreach (var entity in items)
                {
                    await _context.Set<T>().AddAsync(entity);
                }
                await _context.SaveChangesAsync();
                return items;
            }
            catch
            {
                throw;
            }
        }

        public virtual async Task<T> Delete(Guid id)
        {
            try
            {
                var entity = await GetSingle(id);
                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch
            {
                throw;
            }
        }

        public virtual async Task<List<T>> Delete(List<T> items)
        {
            try
            {
                foreach (var item in items)
                {
                    _context.Set<T>().Remove(item);
                }
                await _context.SaveChangesAsync();
                return items;
            }
            catch
            {
                throw;
            }
        }

        public virtual async Task<List<T>> Get()
        {
            try
            {
                return await _context.Set<T>().ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public virtual async Task<List<T>> Get(Func<T, bool> func)
        {
            try
            {
                return _context.Set<T>().Where(func).ToList();
            }
            catch
            {
                throw;
            }
        }

        public virtual async Task<T> GetSingle(Guid id)
        {
            try
            {
                return await _context.Set<T>().FindAsync(id);
            }
            catch
            {
                throw;
            }
        }

        public virtual async Task<T> GetSingle(Func<T, bool> func)
        {
            try
            {
                return _context.Set<T>().Where(func).FirstOrDefault();
            }
            catch
            {
                throw;
            }
        }

        public virtual async Task<T> Update(T item)
        {
            try
            {
                var itemIdProperty = typeof(T).GetProperty("Id");

                var itemId = itemIdProperty.GetValue(item);
                var existingItem = _context.Set<T>().Find(itemId);
                if (existingItem != null)
                {
                    _context.Entry(existingItem).CurrentValues.SetValues(item);
                }
                await _context.SaveChangesAsync();
                return item;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while updating the item: {ex.Message}", ex);
            }
        }

        public virtual async Task<List<T>> Update(List<T> items)
        {
            try
            {
                foreach (var item in items)
                {
                    _context.Entry<T>(item).State = EntityState.Detached;
                    _context.Set<T>().Update(item);
                    _context.Entry(item).State = EntityState.Modified;
                }
                await _context.SaveChangesAsync();
                return items;
            }
            catch
            {
                throw;
            }
        }
    }
}
