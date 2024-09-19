using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechConnect.BLL.Abstract;
using TechConnect.Dto.CompareDtos;
using TechConnect.EL.Concrete;

namespace TechConnect.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompareController : ControllerBase
    {
        private readonly ICompareService _compareService;
        private readonly IMapper _mapper;

        public CompareController(ICompareService compareService, IMapper mapper)
        {
            _compareService = compareService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> CompareList()
        {
            var values = await _compareService.TGetAllAsync();
            var values2 = _mapper.Map<List<ResultCompareDto>>(values);
            return Ok(values2);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCompare(CreateCompareDto createCompareDto)
        {

            await _compareService.TCreateAsync(_mapper.Map<Compare>(createCompareDto));
            return Ok("Karşılaştırma kısmı başarıyla eklendi.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompare(string id)
        {
            await _compareService.TDeleteAsync(id);
            return Ok("Karşılaştırma kısmı başarıyla silindi.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCompare(UpdateCompareDto updateCompareDto, string id)
        {
            await _compareService.TUpdateAsync(_mapper.Map<Compare>(updateCompareDto), id);
            return Ok("Karşılaştırma kısmı başarıyla güncellendi.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdCompare(string id)
        {
            var value = await _compareService.TGetByIdAsync(id);
            var value2 = _mapper.Map<GetByIdCompareDto>(value);
            return Ok(value2);
        }



        [HttpGet("GetAllCompareWithProductByUserID")]
        public async Task<IActionResult> GetAllCompareWithProductByUserID()
        {
            var values = await _compareService.TGetAllCompareWithProductByUserID();
            var values2 = _mapper.Map<List<ResultCompareDto>>(values);
            return Ok(values2);
        }
    }
}
