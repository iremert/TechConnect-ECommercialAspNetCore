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
    public class MongoDbCommentRepository : GenericRepository<Comment>, ICommentDal
    {
        public MongoDbCommentRepository(IDatabaseSettings _databaseSettings) : base(_databaseSettings, "Comment")
        {
        }
    }
}
