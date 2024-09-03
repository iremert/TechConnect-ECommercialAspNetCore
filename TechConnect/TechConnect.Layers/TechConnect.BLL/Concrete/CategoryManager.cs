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
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public async Task TCreateAsync(Category t)
        {
            _categoryDal.CreateAsync(t);
        }

        public async Task TDeleteAsync(string id)
        {
            _categoryDal.DeleteAsync(id);
        }

        public  Task<List<Category>> TGetAllAsync()
        {
            return _categoryDal.GetAllAsync();
        }

        public  Task<Category> TGetByIdAsync(string id)
        {
            return _categoryDal.GetByIdAsync(id);
        }

        public async Task TUpdateAsync(Category t, string id)
        {
            _categoryDal.UpdateAsync(t, id);
        }
    }
}
