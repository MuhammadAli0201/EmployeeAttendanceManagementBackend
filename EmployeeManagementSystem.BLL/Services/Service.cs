using EmployeeManagementSystem.BLL.Interfaces;
using EmployeeManagementSystem.DAL.Interfaces;

namespace EmployeeManagementSystem.BLL.Services
{
    public class Service<T> : IService<T> where T : class
    {
        private readonly IRepository<T> _repository;

        public Service(IRepository<T> repository)
        {
            _repository = repository;
        }

        public virtual async Task<T> Add(T item)
        {
            return await _repository.Add(item);
        }

        public virtual async Task<List<T>> Add(List<T> items)
        {
            return await _repository.Add(items);
        }

        public virtual async Task<T> Delete(T item)
        {
            return await _repository.Delete(item);
        }

        public virtual async Task<List<T>> Delete(List<T> items)
        {
            return await _repository.Delete(items);
        }

        public virtual async Task<List<T>> Get()
        {
            return await _repository.Get();
        }

        public virtual async Task<List<T>> Get(Func<T, bool> func)
        {
            return await _repository.Get(func);
        }

        public virtual async Task<T> GetSingle(Guid id)
        {
            return await _repository.GetSingle(id);
        }

        public virtual async Task<T> GetSingle(Func<T, bool> func)
        {
            return await _repository.GetSingle(func);
        }

        public virtual async Task<T> Update(T item)
        {
            return await _repository.Update(item);
        }

        public virtual async Task<List<T>> Update(List<T> items)
        {
            return await _repository.Update(items);
        }
    }
}
