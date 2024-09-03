using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechConnect.BLL.Abstract;
using TechConnect.DAL.Abstract;
using TechConnect.Dto.DiscountDtos;
using TechConnect.EL.Concrete;

namespace TechConnect.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountService _discountService;
        private readonly IMapper _mapper;

        public DiscountController(IDiscountService discountService, IMapper mapper)
        {
            _discountService = discountService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> DiscountList()
        {
            var values = await _discountService.TGetAllAsync();
            var values2 = _mapper.Map<List<ResultDiscountDto>>(values);
            return Ok(values2);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDiscount(CreateDiscountDto createDiscountDto)
        {

            await _discountService.TCreateAsync(_mapper.Map<Discount>(createDiscountDto));
            return Ok("İndirim kısmı başarıyla eklendi.");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDiscount(string id)
        {
            await _discountService.TDeleteAsync(id);
            return Ok("İndirim kısmı başarıyla silindi.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDiscount(UpdateDiscountDto updateDiscountDto, string id)
        {
            await _discountService.TUpdateAsync(_mapper.Map<Discount>(updateDiscountDto), id);
            return Ok("İndirim kısmı başarıyla güncellendi.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdDiscount(string id)
        {
            var value = await _discountService.TGetByIdAsync(id);
            var value2 = _mapper.Map<GetByIdDiscountDto>(value);
            return Ok(value2);
        }
    }
}
