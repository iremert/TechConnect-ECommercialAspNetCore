using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechConnect.BLL.Abstract;
using TechConnect.Dto.AddressDtos;
using TechConnect.EL.Concrete;

namespace TechConnect.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;
        private readonly IMapper _mapper;

        public AddressController(IAddressService addressService, IMapper mapper)
        {
            _addressService = addressService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> AddressList()
        {
            var values = await _addressService.TGetAllAsync();
            var values2 = _mapper.Map<List<ResultAddressDto>>(values);
            return Ok(values2);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAddress(CreateAddressDto createAddressDto)
        {

            await _addressService.TCreateAsync(_mapper.Map<Address>(createAddressDto));
            return Ok("Adres kısmı başarıyla eklendi.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress(string id)
        {
            await _addressService.TDeleteAsync(id);
            return Ok("Adres kısmı başarıyla silindi.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAddress(UpdateAddressDto updateAddressDto, string id)
        {
            await _addressService.TUpdateAsync(_mapper.Map<Address>(updateAddressDto), id);
            return Ok("Adres kısmı başarıyla güncellendi.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAddress(string id)
        {
            var value = await _addressService.TGetByIdAsync(id);
            var value2 = _mapper.Map<GetByIdAddressDto>(value);
            return Ok(value2);
        }
    }
}
