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
    public class TagManager : ITagService
    {
        private readonly ITagDal _tagDal;

        public TagManager(ITagDal tagDal)
        {
            _tagDal = tagDal;
        }

        public async Task TCreateAsync(Tag t)
        {
            _tagDal.CreateAsync(t);
        }

        public async Task TDeleteAsync(string id)
        {
            _tagDal.DeleteAsync(id);
        }

        public Task<List<Tag>> TGetAllAsync()
        {
            return _tagDal.GetAllAsync();
        }

        public  Task<Tag> TGetByIdAsync(string id)
        {
            return _tagDal.GetByIdAsync(id);
        }

        public async Task TUpdateAsync(Tag t, string id)
        {
            _tagDal.UpdateAsync(t, id);
        }
    }
}
