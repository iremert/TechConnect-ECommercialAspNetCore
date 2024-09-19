using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechConnect.BLL.Abstract;
using TechConnect.Dto.BasketTotalDtos;
using TechConnect.EL.Concrete;

namespace TechConnect.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BasketTotalController : ControllerBase
    {
        private readonly IBasketTotalService _basketTotalService;
        private readonly IMapper _mapper;

        public BasketTotalController(IBasketTotalService basketTotalService, IMapper mapper)
        {
            _basketTotalService = basketTotalService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> BasketTotalList()
        {
            var values = await _basketTotalService.TGetAllAsync();
            var values2 = _mapper.Map<List<ResultBasketTotalDto>>(values);
            return Ok(values2);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBasketTotal(CreateBasketTotalDto createBasketTotalDto)
        {
            await _basketTotalService.TCreateAsync(_mapper.Map<BasketTotal>(createBasketTotalDto));
            return Ok("Sepet kısmı başarıyla eklendi.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBasketTotal(string id)
        {
            await _basketTotalService.TDeleteAsync(id);
            return Ok("Sepet kısmı başarıyla silindi.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBasketTotal(UpdateBasketTotalDto updateBasketTotalDto, string id)
        {
            await _basketTotalService.TUpdateAsync(_mapper.Map<BasketTotal>(updateBasketTotalDto), id);
            return Ok("Sepet kısmı başarıyla güncellendi.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdBasketTotal(string id)
        {
            var value = await _basketTotalService.TGetByIdAsync(id);
            var value2 = _mapper.Map<GetByIdBasketTotalDto>(value);
            return Ok(value2);
        }
    }
}
