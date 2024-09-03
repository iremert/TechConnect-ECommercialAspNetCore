using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechConnect.DAL.Abstract;
using TechConnect.DAL.Concrete;
using TechConnect.DAL.Repositories;
using TechConnect.EL.Concrete;

namespace TechConnect.DAL.MongoDbDriver
{
    public class MongoDbContact2Repository : GenericRepository<Contact2>, IContact2Dal
    {
        public MongoDbContact2Repository(IDatabaseSettings _databaseSettings) : base(_databaseSettings, "Contact2")
        {
        }
    }
}
