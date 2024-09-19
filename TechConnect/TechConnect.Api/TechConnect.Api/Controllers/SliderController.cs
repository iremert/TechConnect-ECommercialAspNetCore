using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechConnect.BLL.Abstract;
using TechConnect.Dto.SliderDtos;
using TechConnect.EL.Concrete;

namespace TechConnect.Api.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class SliderController : ControllerBase
    {
        private readonly ISliderService _sliderService;
        private readonly IMapper _mapper;

        public SliderController(ISliderService sliderService, IMapper mapper)
        {
            _sliderService = sliderService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> SliderList()
        {
            var values = await _sliderService.TGetAllAsync();
            var values2 = _mapper.Map<List<ResultSliderDto>>(values);
            return Ok(values2);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSlider(CreateSliderDto createSliderDto)
        {

            await _sliderService.TCreateAsync(_mapper.Map<Slider>(createSliderDto));
            return Ok("Slider kısmı başarıyla eklendi.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSlider(string id)
        {
            await _sliderService.TDeleteAsync(id);
            return Ok("Slider kısmı başarıyla silindi.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSlider(UpdateSliderDto updateSliderDto, string id)
        {
            await _sliderService.TUpdateAsync(_mapper.Map<Slider>(updateSliderDto), id);
            return Ok("Slider kısmı başarıyla güncellendi.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdSlider(string id)
        {
            var value = await _sliderService.TGetByIdAsync(id);
            var value2 = _mapper.Map<GetByIdSliderDto>(value);
            return Ok(value2);
        }
    }
}
