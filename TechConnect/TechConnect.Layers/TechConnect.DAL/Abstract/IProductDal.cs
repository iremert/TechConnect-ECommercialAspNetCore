using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechConnect.EL.Concrete;

namespace TechConnect.DAL.Abstract
{
    public interface IProductDal:IGenericDal<Product>
    {
        Task<List<Product>> GetAllProductWithCategory();
        Task<Product> GetProductWithCategory(string id);
        Task<List<Product>> GetProductsByCategoryId(string id);
        Task<List<Product>> GetProductsByBrand(string brand);
        Task<List<Product>> GetProductsByPrice(double price, double price2);
        Task<List<Product>> GetProductsBySize(string size);
        Task<List<Product>> GetProductsByColorId(string ID);
        Task DeleteFavourite(string id);
        Task AddFavourite(string id);
        Task DeleteCompare(string id);
        Task AddCompare(string id);
    }
}
