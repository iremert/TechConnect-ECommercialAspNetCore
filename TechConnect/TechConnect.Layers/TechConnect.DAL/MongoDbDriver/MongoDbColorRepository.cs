
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechConnect.DAL.Abstract;
using TechConnect.DAL.Concrete;
using TechConnect.DAL.Repositories;
using TechConnect.EL.Concrete;

namespace TechConnect.DAL.MongoDbDriver
{
    public class MongoDbColorRepository : GenericRepository<Color>, IColorDal
    {
        public MongoDbColorRepository(IDatabaseSettings _databaseSettings) : base(_databaseSettings,"Color")
        {
        }
    }
}
