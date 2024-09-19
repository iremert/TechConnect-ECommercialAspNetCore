using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechConnect.BLL.Abstract;
using TechConnect.Dto.AboutDtos;
using TechConnect.EL.Concrete;

namespace TechConnect.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AboutController : ControllerBase
    {
        private readonly IAboutService _aboutService;
        private readonly IMapper _mapper;

        public AboutController(IAboutService aboutService, IMapper mapper)
        {
            _aboutService = aboutService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> AboutList()
        {
            var values =await _aboutService.TGetAllAsync();
            var values2=_mapper.Map<List<ResultAboutDto>>(values);
            return Ok(values2);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAbout(CreateAboutDto createAboutDto)
        {

           await _aboutService.TCreateAsync(_mapper.Map<About>(createAboutDto));
            return Ok("Hakkımızda kısmı başarıyla eklendi.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAbout(string id)
        {
           await  _aboutService.TDeleteAsync(id);
            return Ok("Hakkımızda kısmı başarıyla silindi.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAbout(UpdateAboutDto updateAboutDto,string id)
        {
            await _aboutService.TUpdateAsync(_mapper.Map<About>(updateAboutDto),id);
            return Ok("Hakkımızda kısmı başarıyla güncellendi.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAbout(string id)
        {
            var value=await _aboutService.TGetByIdAsync(id);
            var value2=_mapper.Map<GetByIdAboutDto>(value);
            return Ok(value2);
        }
    }
}
