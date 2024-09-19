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
    public class CommentManager : ICommentService
    {
        private readonly ICommentDal _commentDal;

        public CommentManager(ICommentDal commentDal)
        {
            _commentDal = commentDal;
        }

        public async Task TCreateAsync(Comment t)
        {
            _commentDal.CreateAsync(t);
        }

        public async Task TDeleteAsync(string id)
        {
            _commentDal.DeleteAsync(id);
        }

        public Task<List<Comment>> TGetAllAsync()
        {
           return _commentDal.GetAllAsync();
        }

        public Task<Comment> TGetByIdAsync(string id)
        {
            return _commentDal.GetByIdAsync(id);
        }

        public Task TUpdateAsync(Comment t, string id)
        {
            return _commentDal.UpdateAsync(t, id);
        }
    }
}
