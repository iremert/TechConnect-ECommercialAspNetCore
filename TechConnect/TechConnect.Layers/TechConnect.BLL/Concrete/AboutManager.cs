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
    public class AboutManager : IAboutService
    {
        private readonly IAboutDal _ıaboutdal;

        public AboutManager(IAboutDal ıaboutdal)
        {
            _ıaboutdal = ıaboutdal;
        }

        public async Task TCreateAsync(About t)
        {
            _ıaboutdal.CreateAsync(t);
        }

        public async Task TDeleteAsync(string id)
        {
            _ıaboutdal.DeleteAsync(id);
        }

        public Task<List<About>> TGetAllAsync()
        {
            return _ıaboutdal.GetAllAsync();
        }

        public Task<About> TGetByIdAsync(string id)
        {
            return _ıaboutdal.GetByIdAsync(id);
        }

        public async Task TUpdateAsync(About t, string id)
        {
            _ıaboutdal.UpdateAsync(t, id);
        }
    }
}
