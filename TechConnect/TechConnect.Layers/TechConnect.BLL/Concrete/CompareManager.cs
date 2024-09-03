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
    public class CompareManager : ICompareService
    {
        private readonly ICompareDal _compareDal;

        public CompareManager(ICompareDal compareDal)
        {
            _compareDal = compareDal;
        }

        public async Task TCreateAsync(Compare t)
        {
            _compareDal.CreateAsync(t);
        }

        public async Task TDeleteAsync(string id)
        {
            _compareDal.DeleteAsync(id);
        }

        public Task<List<Compare>> TGetAllAsync()
        {
            return _compareDal.GetAllAsync();
        }

        public Task<List<Compare>> TGetAllCompareWithProductByUserID()
        {
            return _compareDal.GetAllCompareWithProductByUserID();
        }

        public Task<Compare> TGetByIdAsync(string id)
        {
            return _compareDal.GetByIdAsync(id);
        }

        public async Task TUpdateAsync(Compare t, string id)
        {
            _compareDal.UpdateAsync(t, id);
        }
    }
}
