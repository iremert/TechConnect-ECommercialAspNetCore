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
    public class BasketTotalManager : IBasketTotalService
    {
        private readonly IBasketTotalDal _basketTotalDal;

        public BasketTotalManager(IBasketTotalDal basketTotalDal)
        {
            _basketTotalDal = basketTotalDal;
        }

        public async Task TCreateAsync(BasketTotal t)
        {
            _basketTotalDal.CreateAsync(t);
        }

        public async Task TDeleteAsync(string id)
        {
            _basketTotalDal.DeleteAsync(id);
        }

        public Task<List<BasketTotal>> TGetAllAsync()
        {
            return _basketTotalDal.GetAllAsync();
        }

        public Task<BasketTotal> TGetByIdAsync(string id)
        {
           return _basketTotalDal.GetByIdAsync(id);
        }

        public Task TUpdateAsync(BasketTotal t, string id)
        {
            return _basketTotalDal.UpdateAsync(t, id);
        }
    }
}
