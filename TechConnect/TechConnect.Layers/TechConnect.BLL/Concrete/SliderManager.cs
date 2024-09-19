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
    public class SliderManager:ISliderService
    {
        private readonly ISliderDal _sliderDal;

        public SliderManager(ISliderDal sliderDal)
        {
            _sliderDal = sliderDal;
        }

        public async  Task TCreateAsync(Slider t)
        {
            _sliderDal.CreateAsync(t);
        }

        public async Task TDeleteAsync(string id)
        {
            _sliderDal.DeleteAsync(id);
        }

        public Task<List<Slider>> TGetAllAsync()
        {
            return _sliderDal.GetAllAsync();
        }

        public Task<Slider> TGetByIdAsync(string id)
        {
            return _sliderDal.GetByIdAsync(id);
        }

        public async Task TUpdateAsync(Slider t, string id)
        {
            _sliderDal.UpdateAsync(t, id);
        }
    }
}
