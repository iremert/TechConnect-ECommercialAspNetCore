using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechConnect.BLL.Abstract;
using TechConnect.Dto.TagDtos;
using TechConnect.EL.Concrete;

namespace TechConnect.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagService _tagService;
        private readonly IMapper _mapper;

        public TagController(ITagService tagService, IMapper mapper)
        {
            _tagService = tagService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> TagList()
        {
            var values = await _tagService.TGetAllAsync();
            var values2 = _mapper.Map<List<ResultTagDto>>(values);
            return Ok(values2);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTag(CreateTagDto createTagDto)
        {

            await _tagService.TCreateAsync(_mapper.Map<Tag>(createTagDto));
            return Ok("Etiket kısmı başarıyla eklendi.");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTag(string id)
        {
            await _tagService.TDeleteAsync(id);
            return Ok("Etiket kısmı başarıyla silindi.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTag(UpdateTagDto updateTagDto, string id)
        {
            await _tagService.TUpdateAsync(_mapper.Map<Tag>(updateTagDto), id);
            return Ok("Etiket kısmı başarıyla güncellendi.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdTag(string id)
        {
            var value = await _tagService.TGetByIdAsync(id);
            var value2 = _mapper.Map<GetByIdTagDto>(value);
            return Ok(value2);
        }
    }
}
