using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechConnect.BLL.Abstract;
using TechConnect.DAL.Abstract;
using TechConnect.EL.Concrete;

namespace TechConnect.BLL.Concrete
{
    public class FavouriteManager : IFavouriteService
    {
        private readonly IFavouriteDal _favouriteDal;

        public FavouriteManager(IFavouriteDal favouriteDal)
        {
            _favouriteDal = favouriteDal;
        }

        public async Task TCreateAsync(Favourite t)
        {
            _favouriteDal.CreateAsync(t);
        }

        public async Task TDeleteAsync(string id)
        {
            _favouriteDal.DeleteAsync(id);
        }

        public Task<List<Favourite>> TGetAllAsync()
        {
            return _favouriteDal.GetAllAsync(); 
        }

        public Task<List<Favourite>> TGetAllFavouriteWithProductByUserID()
        {
            return _favouriteDal.GetAllFavouriteWithProductByUserID();
        }

        public Task<Favourite> TGetByIdAsync(string id)
        {
            return _favouriteDal.GetByIdAsync(id);
        }

        public async Task TUpdateAsync(Favourite t, string id)
        {
            _favouriteDal.UpdateAsync(t, id);
        }
    }
}
