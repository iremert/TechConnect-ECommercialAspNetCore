using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechConnect.BLL.Abstract;
using TechConnect.Dto.TestimonialDtos;
using TechConnect.EL.Concrete;

namespace TechConnect.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TestimonialController : ControllerBase
    {
        private readonly ITestimonialService _testimonialService;
        private readonly IMapper _mapper;

        public TestimonialController(ITestimonialService testimonialService, IMapper mapper)
        {
            _testimonialService = testimonialService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> TestimonialList()
        {
            var values = await _testimonialService.TGetAllAsync();
            var values2 = _mapper.Map<List<ResultTestimonialDto>>(values);
            return Ok(values2);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTestimonial(CreateTestimonialDto createTestimonialDto)
        {

            await _testimonialService.TCreateAsync(_mapper.Map<Testimonial>(createTestimonialDto));
            return Ok("Alıntı kısmı başarıyla eklendi.");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTestimonial(string id)
        {
            await _testimonialService.TDeleteAsync(id);
            return Ok("Alıntı kısmı başarıyla silindi.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAbout(UpdateTestimonialDto updateTestimonialDto, string id)
        {
            await _testimonialService.TUpdateAsync(_mapper.Map<Testimonial>(updateTestimonialDto), id);
            return Ok("Alıntı kısmı başarıyla güncellendi.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAbout(string id)
        {
            var value = await _testimonialService.TGetByIdAsync(id);
            var value2 = _mapper.Map<GetByIdTestimonialDto>(value);
            return Ok(value2);
        }

    }
}
