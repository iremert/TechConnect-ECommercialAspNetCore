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
    public class MongoDbSliderRepository : GenericRepository<Slider>, ISliderDal
    {
        public MongoDbSliderRepository(IDatabaseSettings _databaseSettings) : base(_databaseSettings,"Slider")
        {
        }
    }
}
