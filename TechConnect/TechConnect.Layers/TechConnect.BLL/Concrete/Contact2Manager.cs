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
    public class Contact2Manager : IContact2Service
    {
        private readonly IContact2Dal _contact2Dal;

        public Contact2Manager(IContact2Dal contact2Dal)
        {
            _contact2Dal = contact2Dal;
        }

        public async Task TCreateAsync(Contact2 t)
        {
            _contact2Dal.CreateAsync(t);
        }

        public async Task TDeleteAsync(string id)
        {
            _contact2Dal.DeleteAsync(id);
        }

        public Task<List<Contact2>> TGetAllAsync()
        {
            return _contact2Dal.GetAllAsync();
        }

        public Task<Contact2> TGetByIdAsync(string id)
        {
            return _contact2Dal.GetByIdAsync(id);
        }

        public async Task TUpdateAsync(Contact2 t, string id)
        {
            _contact2Dal.UpdateAsync(t, id);
        }
    }
}
