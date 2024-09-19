using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechConnect.BLL.Abstract;
using TechConnect.Dto.OrderingDtos;
using TechConnect.EL.Concrete;

namespace TechConnect.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderingController : ControllerBase
    {
        private readonly IOrderingService _orderingService;
        private readonly IMapper _mapper;

        public OrderingController(IOrderingService orderingService, IMapper mapper)
        {
            _orderingService = orderingService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> OrderingList()
        {
            var values = await _orderingService.TGetAllAsync();
            var values2 = _mapper.Map<List<ResultOrderingDto>>(values);
            return Ok(values2);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrdering(CreateOrderingDto createOrderingDto)
        {

            await _orderingService.TCreateAsync(_mapper.Map<Ordering>(createOrderingDto));
            return Ok("Sipariş kısmı başarıyla eklendi.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrdering(string id)
        {
            await _orderingService.TDeleteAsync(id);
            return Ok("Sipariş kısmı başarıyla silindi.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrdering(UpdateOrderingDto updateOrderingDto, string id)
        {
            await _orderingService.TUpdateAsync(_mapper.Map<Ordering>(updateOrderingDto), id);
            return Ok("Hakkımızda kısmı başarıyla güncellendi.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdOrdering(string id)
        {
            var value = await _orderingService.TGetByIdAsync(id);
            var value2 = _mapper.Map<GetByIdOrderingDto>(value);
            return Ok(value2);
        }
    }
}
