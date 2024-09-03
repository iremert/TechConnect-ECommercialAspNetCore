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
    public class TestimonialManager : ITestimonialService
    {
        private readonly ITestimonialDal _testimonialDal;

        public TestimonialManager(ITestimonialDal testimonialDal)
        {
            _testimonialDal = testimonialDal;
        }

        public async Task TCreateAsync(Testimonial t)
        {
            _testimonialDal.CreateAsync(t);
        }

        public async Task TDeleteAsync(string id)
        {
            _testimonialDal.DeleteAsync(id);
        }

        public Task<List<Testimonial>> TGetAllAsync()
        {
            return _testimonialDal.GetAllAsync();
        }

        public Task<Testimonial> TGetByIdAsync(string id)
        {
            return _testimonialDal.GetByIdAsync(id);
        }

        public async Task TUpdateAsync(Testimonial t, string id)
        {
            _testimonialDal.UpdateAsync(t, id);
        }
    }
}
