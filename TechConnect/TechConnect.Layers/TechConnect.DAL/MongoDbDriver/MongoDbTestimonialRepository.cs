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
    public class MongoDbTestimonialRepository : GenericRepository<Testimonial>, ITestimonialDal
    {
        public MongoDbTestimonialRepository(IDatabaseSettings _databaseSettings) : base(_databaseSettings,"Testimonial")
        {
        }
    }
}
