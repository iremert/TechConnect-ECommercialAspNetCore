using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechConnect.EL.Concrete;

namespace TechConnect.DAL.Abstract
{
    public interface ICompareDal:IGenericDal<Compare>
    {
        Task<List<Compare>> GetAllCompareWithProductByUserID();
    }
}
