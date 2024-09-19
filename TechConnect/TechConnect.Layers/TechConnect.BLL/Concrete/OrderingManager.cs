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
    public class OrderingManager : IOrderingService
    {
        private readonly IOrderingDal _orderingDal;

        public OrderingManager(IOrderingDal orderingDal)
        {
            _orderingDal = orderingDal;
        }

        public async Task TCreateAsync(Ordering t)
        {
            _orderingDal.CreateAsync(t);
        }

        public async Task TDeleteAsync(string id)
        {
            _orderingDal.DeleteAsync(id);
        }

        public Task<List<Ordering>> TGetAllAsync()
        {
            return _orderingDal.GetAllAsync();
        }

        public Task<Ordering> TGetByIdAsync(string id)
        {
            return _orderingDal.GetByIdAsync(id);
        }

        public async Task TUpdateAsync(Ordering t, string id)
        {
            _orderingDal.UpdateAsync(t, id);
        }
    }
}
