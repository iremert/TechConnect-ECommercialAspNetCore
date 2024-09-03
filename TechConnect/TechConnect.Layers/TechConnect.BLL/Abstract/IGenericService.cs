using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechConnect.BLL.Abstract
{
    public interface IGenericService<T> where T:class
    {
        Task TCreateAsync(T t);
        Task TUpdateAsync(T t, string id);
        Task TDeleteAsync(string id);
        Task<List<T>> TGetAllAsync();
        Task<T> TGetByIdAsync(string id);
    }
}
