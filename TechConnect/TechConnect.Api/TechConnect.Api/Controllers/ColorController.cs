using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechConnect.BLL.Abstract;
using TechConnect.Dto.ColorDtos;
using TechConnect.EL.Concrete;

namespace TechConnect.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ColorController : ControllerBase
    {
        private readonly IColorService _colorService;
        private readonly IMapper _mapper;

        public ColorController(IColorService aboutService, IMapper mapper)
        {
            _colorService = aboutService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> ColorList()
        {
            var values = await _colorService.TGetAllAsync();
            var values2 = _mapper.Map<List<ResultColorDto>>(values);
            return Ok(values2);
        }

        [HttpPost]
        public async Task<IActionResult> CreateColor(CreateColorDto createColorDto)
        {

            await _colorService.TCreateAsync(_mapper.Map<Color>(createColorDto));
            return Ok("Renk kısmı başarıyla eklendi.");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteColor(string id)
        {
            await _colorService.TDeleteAsync(id);
            return Ok("Renk kısmı başarıyla silindi.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateColor(UpdateColorDto updateColorDto, string id)
        {
            await _colorService.TUpdateAsync(_mapper.Map<Color>(updateColorDto), id);
            return Ok("Renk kısmı başarıyla güncellendi.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdColor(string id)
        {
            var value = await _colorService.TGetByIdAsync(id);
            var value2 = _mapper.Map<GetByIdColorDto>(value);
            return Ok(value2);
        }
    }
}
