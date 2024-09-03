using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechConnect.BLL.Abstract;
using TechConnect.DAL.Abstract;
using TechConnect.EL.Concrete;

namespace TechConnect.BLL.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public async Task TAddCompare(string id)
        {
            _productDal.AddCompare(id);
        }

        public async Task TAddFavourite(string id)
        {
            _productDal.AddFavourite(id);
        }

        public async Task TCreateAsync(Product t)
        {
            _productDal.CreateAsync(t);
        }

        public async Task TDeleteAsync(string id)
        {
            _productDal.DeleteAsync(id);
        }

        public async Task TDeleteCompare(string id)
        {
            _productDal.DeleteCompare(id);
        }

        public async Task TDeleteFavourite(string id)
        {
            _productDal.DeleteFavourite(id);
        }

        public Task<List<Product>> TGetAllAsync()
        {
            return _productDal.GetAllAsync();
        }

        public Task<List<Product>> TGetAllProductWithCategory()
        {
            return _productDal.GetAllProductWithCategory();
        }

        public Task<Product> TGetByIdAsync(string id)
        {
            return _productDal.GetByIdAsync(id);
        }

        public Task<List<Product>> TGetProductsByBrand(string brand)
        {
           return _productDal.GetProductsByBrand(brand);
        }

        public Task<List<Product>> TGetProductsByCategoryId(string id)
        {
            return _productDal.GetProductsByCategoryId(id);
        }

        public Task<List<Product>> TGetProductsByColorId(string ID)
        {
            return _productDal.GetProductsByColorId(ID);
        }

        public Task<List<Product>> TGetProductsByPrice(double price, double price2)
        {
            return _productDal.GetProductsByPrice(price,price2);
        }

        public Task<List<Product>> TGetProductsBySize(string size)
        {
            return _productDal.GetProductsBySize(size);
        }

        public Task<Product> TGetProductWithCategory(string id)
        {
            return _productDal.GetProductWithCategory(id);
        }

        public Task TUpdateAsync(Product t, string id)
        {
            return _productDal.UpdateAsync(t, id);
        }
    }
}
