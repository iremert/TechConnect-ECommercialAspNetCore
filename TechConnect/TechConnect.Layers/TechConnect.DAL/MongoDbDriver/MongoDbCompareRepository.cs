using Microsoft.AspNetCore.Http;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TechConnect.DAL.Abstract;
using TechConnect.DAL.Concrete;
using TechConnect.DAL.Repositories;
using TechConnect.EL.Concrete;

namespace TechConnect.DAL.MongoDbDriver
{
    public class MongoDbCompareRepository : GenericRepository<Compare>, ICompareDal
    {
        private readonly IMongoCollection<Compare> _comparecollection;
        private readonly IMongoCollection<Product> _productcollection;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public MongoDbCompareRepository(IDatabaseSettings _databaseSettings, IHttpContextAccessor httpContextAccessor) : base(_databaseSettings, "Compare")
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _comparecollection = database.GetCollection<Compare>(_databaseSettings.CollectionNames["Compare"]);
            _productcollection = database.GetCollection<Product>(_databaseSettings.CollectionNames["Product"]);
            _httpContextAccessor = httpContextAccessor;
        }



        public async Task<List<Compare>> GetAllCompareWithProductByUserID()
        {
            // Giriş yapmış kullanıcının ID'sini al
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                throw new Exception("Kullanıcı kimliği bulunamadı.");
            }

            var values = await _comparecollection.Find(x => x.UserID == userId).ToListAsync();
            foreach (var product in values)
            {
                product.Product = await _productcollection.Find(x => x.ID == product.ProductID).FirstAsync();
            }
            return values;
        }
    }
}
