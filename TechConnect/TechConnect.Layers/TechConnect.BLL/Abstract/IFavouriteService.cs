using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechConnect.EL.Concrete;

namespace TechConnect.BLL.Abstract
{
    public interface IFavouriteService:IGenericService<Favourite>
    {
        Task<List<Favourite>> TGetAllFavouriteWithProductByUserID();
    }
}
