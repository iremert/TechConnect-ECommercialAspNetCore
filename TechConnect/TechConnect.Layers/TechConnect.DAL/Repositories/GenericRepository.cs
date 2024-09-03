using AutoMapper;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechConnect.DAL.Abstract;
using TechConnect.DAL.Concrete;
using TechConnect.EL.Concrete;

namespace TechConnect.DAL.Repositories
{
    public class GenericRepository<T> : IGenericDal<T> where T : class,IEntity
    {
        private readonly IMongoCollection<T> _collection;

        public GenericRepository(IDatabaseSettings _databaseSettings, string collectionName)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _collection = database.GetCollection<T>(_databaseSettings.CollectionNames[collectionName]);

        }

        public async Task CreateAsync(T t)
        {
            await _collection.InsertOneAsync(t);
        }

        public async Task DeleteAsync(string id)
        {
            var filter = Builders<T>.Filter.Eq(x => x.ID, id);
            await _collection.DeleteOneAsync(filter);
        }

        public async Task<List<T>> GetAllAsync()
        {
            var values = await _collection.Find(x => true).ToListAsync();
            return values;
        }

        public async Task<T> GetByIdAsync(string id)
        {
            var filter = Builders<T>.Filter.Eq(x => x.ID, id);
            var value = await _collection.Find(filter).FirstOrDefaultAsync();
            return value;
        }

        public async Task UpdateAsync(T t, string id)
        {
            var filter = Builders<T>.Filter.Eq(x => x.ID, id);
            t.ID = id;
            await _collection.FindOneAndReplaceAsync(filter, t);

        }
    }
}
