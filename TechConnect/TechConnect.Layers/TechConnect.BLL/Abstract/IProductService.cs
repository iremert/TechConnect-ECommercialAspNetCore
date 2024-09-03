using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechConnect.EL.Concrete;

namespace TechConnect.BLL.Abstract
{
    public interface IProductService:IGenericService<Product>
    {
        Task<List<Product>> TGetAllProductWithCategory();
        Task<Product> TGetProductWithCategory(string id);
        Task<List<Product>> TGetProductsByCategoryId(string id);
        Task<List<Product>> TGetProductsByBrand(string brand);
        Task<List<Product>> TGetProductsByPrice(double price, double price2);
        Task<List<Product>> TGetProductsBySize(string size);
        Task<List<Product>> TGetProductsByColorId(string ID);
        Task TDeleteFavourite(string id);
        Task TAddFavourite(string id);
        Task TDeleteCompare(string id);
        Task TAddCompare(string id);
    }
}
