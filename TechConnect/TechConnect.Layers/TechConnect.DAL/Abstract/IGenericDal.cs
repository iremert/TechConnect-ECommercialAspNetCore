using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechConnect.DAL.Abstract
{
    public interface IGenericDal<T> where T : class
    {
        Task CreateAsync(T t);
        Task UpdateAsync(T t,string id);
        Task DeleteAsync(string id);
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(string id);
    }
}
