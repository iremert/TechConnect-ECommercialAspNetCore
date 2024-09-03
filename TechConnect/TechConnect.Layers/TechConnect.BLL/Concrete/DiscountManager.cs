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
    public class DiscountManager : IDiscountService
    {
        private readonly IDiscountDal _discountDal;

        public DiscountManager(IDiscountDal discountDal)
        {
            _discountDal = discountDal;
        }

        public async Task TCreateAsync(Discount t)
        {
            _discountDal.CreateAsync(t);
        }

        public async Task TDeleteAsync(string id)
        {
            _discountDal.DeleteAsync(id);
        }

        public Task<List<Discount>> TGetAllAsync()
        {
            return _discountDal.GetAllAsync();
        }

        public Task<Discount> TGetByIdAsync(string id)
        {
          return _discountDal.GetByIdAsync(id);
        }

        public async Task TUpdateAsync(Discount t, string id)
        {
            _discountDal.UpdateAsync(t, id);
        }
    }
}
