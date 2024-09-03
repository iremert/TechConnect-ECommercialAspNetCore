using MongoDB.Driver;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechConnect.DAL.Abstract;
using TechConnect.DAL.Concrete;
using TechConnect.DAL.Repositories;
using TechConnect.EL.Concrete;

namespace TechConnect.DAL.MongoDbDriver
{
    public class MongoDbProductRepository : GenericRepository<Product>, IProductDal
    {
        private readonly IMongoCollection<Product> _productscollection;
        private readonly IMongoCollection<Category> _categoriescollection;
        public MongoDbProductRepository(IDatabaseSettings _databaseSettings) : base(_databaseSettings, "Product")
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _productscollection = database.GetCollection<Product>(_databaseSettings.CollectionNames["Product"]);
            _categoriescollection = database.GetCollection<Category>(_databaseSettings.CollectionNames["Category"]);
        }

        public async Task AddCompare(string id)
        {
            var value = await _productscollection.Find(x => x.ID == id).FirstOrDefaultAsync();
            value.IsCompare = true;
            await _productscollection.ReplaceOneAsync(x => x.ID == id, value);
        }

        public async Task AddFavourite(string id)
        {
            var value = await _productscollection.Find(x => x.ID == id).FirstOrDefaultAsync();
            value.IsFavourite = true;
            await _productscollection.ReplaceOneAsync(x => x.ID == id, value);


        }

        public async Task DeleteCompare(string id)
        {
            var value = await _productscollection.Find(x => x.ID == id).FirstOrDefaultAsync();
            value.IsCompare = false;
            await _productscollection.ReplaceOneAsync(x => x.ID == id, value);
        }

        public async Task DeleteFavourite(string id)
        {
            var value = await _productscollection.Find(x => x.ID == id).FirstOrDefaultAsync();
            value.IsFavourite = false;
            await _productscollection.ReplaceOneAsync(x => x.ID == id, value);
        }

        public async Task<List<Product>> GetAllProductWithCategory()
        {
            var values = await _productscollection.Find(x => true).ToListAsync();
            foreach (var product in values)
            {
                product.Category = await _categoriescollection.Find(x => x.ID == product.CategoryId).FirstAsync();
            }
            return values;
        }

        public async Task<List<Product>> GetProductsByBrand(string brand)
        {
            var values = await _productscollection.Find(x => x.Brand == brand).ToListAsync();
            return values;
        }

        public async Task<List<Product>> GetProductsByCategoryId(string id)
        {
            var values =await _productscollection.Find(x => x.CategoryId == id).ToListAsync();
            return values;
        }

        public async Task<List<Product>> GetProductsByColorId(string ID)
        {
            var values = await _productscollection.Find(x => x.ColorId == ID).ToListAsync();
            return values;
        }

        public async Task<List<Product>> GetProductsByPrice(double price,double price2)
        {
            var values = await _productscollection.Find(x => x.ProductPrice>=price && x.ProductPrice<=price2).ToListAsync();
            return values;
        }

        public async Task<List<Product>> GetProductsBySize(string size)
        {
            var values = await _productscollection.Find(x => x.TechnicalSpecifications["Size"]==size).ToListAsync();
            return values;
        }

        public async Task<Product> GetProductWithCategory(string id)
        {
            var value = await _productscollection.Find(x => x.ID == id).FirstOrDefaultAsync();
            value.Category = await _categoriescollection.Find(x => x.ID == value.CategoryId).FirstAsync();

            return value;
        }
    }
}
