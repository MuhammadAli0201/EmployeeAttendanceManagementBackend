using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.BLL.Interfaces
{
    public interface IService<T> where T : class
    {
        public Task<T> GetSingle(Guid id);
        public Task<T> GetSingle(Func<T, bool> func);
        public Task<List<T>> Get();
        public Task<List<T>> Get(Func<T, bool> func);
        public Task<T> Add(T item);
        public Task<List<T>> Add(List<T> items);
        public Task<T> Update(T item);
        public Task<List<T>> Update(List<T> items);
        public Task<T> Delete(Guid id);
        public Task<List<T>> Delete(List<T> items);       
    }
}
