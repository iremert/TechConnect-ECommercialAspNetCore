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
    public class ColorManager : IColorService
    {
        private readonly IColorDal _colorDal;

        public ColorManager(IColorDal colorDal)
        {
            _colorDal = colorDal;
        }

        public async Task TCreateAsync(Color t)
        {
            _colorDal.CreateAsync(t);
        }

        public  async Task TDeleteAsync(string id)
        {
            _colorDal.DeleteAsync(id);
        }

        public  Task<List<Color>> TGetAllAsync()
        {
            return _colorDal.GetAllAsync();
        }

        public Task<Color> TGetByIdAsync(string id)
        {
            return _colorDal.GetByIdAsync(id);
        }

        public async Task TUpdateAsync(Color t, string id)
        {
            _colorDal.UpdateAsync(t, id);
        }
    }
}
