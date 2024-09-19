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
    public class AddressManager : IAddressService
    {
        private readonly IAddressDal _addressDal;

        public AddressManager(IAddressDal addressDal)
        {
            _addressDal = addressDal;
        }

        public async Task TCreateAsync(Address t)
        {
            _addressDal.CreateAsync(t); 
        }

        public async Task TDeleteAsync(string id)
        {
            _addressDal.DeleteAsync(id);
        }

        public Task<List<Address>> TGetAllAsync()
        {
            return _addressDal.GetAllAsync();
        }

        public Task<Address> TGetByIdAsync(string id)
        {
           return _addressDal.GetByIdAsync(id);
        }

        public Task TUpdateAsync(Address t, string id)
        {
            return _addressDal.UpdateAsync(t, id);
        }
    }
}
