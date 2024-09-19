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
    public class MongoDbOrderingRepository : GenericRepository<Ordering>, IOrderingDal
    {
        public MongoDbOrderingRepository(IDatabaseSettings _databaseSettings) : base(_databaseSettings, "Ordering")
        {
        }
    }
}
