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
    public class ContactManager : IContactService
    {
        private readonly IContactDal _contactDal;

        public ContactManager(IContactDal contactDal)
        {
            _contactDal = contactDal;
        }

        public async Task TCreateAsync(Contact t)
        {
            _contactDal.CreateAsync(t);
        }

        public async Task TDeleteAsync(string id)
        {
            _contactDal.DeleteAsync(id);
        }

        public Task<List<Contact>> TGetAllAsync()
        {
            return _contactDal.GetAllAsync();
        }

        public Task<Contact> TGetByIdAsync(string id)
        {
            return _contactDal.GetByIdAsync(id);
        }

        public async Task TUpdateAsync(Contact t, string id)
        {
            _contactDal.UpdateAsync(t, id);
        }
    }
}
