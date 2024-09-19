using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechConnect.BLL.Abstract;
using TechConnect.Dto.CategoryDtos;
using TechConnect.EL.Concrete;

namespace TechConnect.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }



        [HttpGet]
        public async Task<IActionResult> CategoryList()
        {
            var values = await _categoryService.TGetAllAsync();
            var values2 = _mapper.Map<List<ResultCategoryDto>>(values);
            return Ok(values2);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto createCategoryDto)
        {
            
            await _categoryService.TCreateAsync(_mapper.Map<Category>(createCategoryDto));
            return Ok("Kategori kısmı başarıyla eklendi.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(string id)
        {
            await _categoryService.TDeleteAsync(id);
            return Ok("Kategori kısmı başarıyla silindi.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDto updateCategoryDto, string id)
        {
            await _categoryService.TUpdateAsync(_mapper.Map<Category>(updateCategoryDto), id);
            return Ok("Kategori kısmı başarıyla güncellendi.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdCategory(string id)
        {
            var value = await _categoryService.TGetByIdAsync(id);
            var value2 = _mapper.Map<GetByIdCategoryDto>(value);
            return Ok(value2);
        }
    }
}
