using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechConnect.BLL.Abstract;
using TechConnect.Dto.ProductDtos;
using TechConnect.EL.Concrete;

namespace TechConnect.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }



        [HttpGet]
        public async Task<IActionResult> ProductList()
        {
            var values = await _productService.TGetAllProductWithCategory();
            var values2 = _mapper.Map<List<ResultProductDto>>(values);
            return Ok(values2);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDto createProductDto)
        {

            await _productService.TCreateAsync(_mapper.Map<Product>(createProductDto));
            return Ok("Ürünler kısmı başarıyla eklendi.");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            await _productService.TDeleteAsync(id);
            return Ok("Ürünler kısmı başarıyla silindi.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAbout(UpdateProductDto updateProductDto, string id)
        {
            await _productService.TUpdateAsync(_mapper.Map<Product>(updateProductDto), id);
            return Ok("Ürünler kısmı başarıyla güncellendi.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdProduct(string id)
        {
            var value = await _productService.TGetProductWithCategory(id);
            var value2 = _mapper.Map<GetByIdProductDto>(value);
            return Ok(value2);
        }


        [HttpGet("GetProductsWithCategoryByCategoryId/{id}")]
        public async Task<IActionResult> GetProductsWithCategoryByCategoryId(string id)
        {
            var values = await _productService.TGetProductsByCategoryId(id);
            var values2 = _mapper.Map<List<ResultProductDto>>(values);
            return Ok(values2);
        }

        [HttpGet("GetProductsWithCategoryByBrand/{brand}")]
        public async Task<IActionResult> GetProductsWithCategoryByBrand(string brand)
        {
            var values = await _productService.TGetProductsByBrand(brand);
            var values2 = _mapper.Map<List<ResultProductDto>>(values);
            return Ok(values2);
        }

        [HttpGet("GetProductsWithCategoryByPrice/{price}/{price2}")]
        public async Task<IActionResult> GetProductsWithCategoryByPrice(double price, double price2)
        {
            var values = await _productService.TGetProductsByPrice(price,price2);
            var values2 = _mapper.Map<List<ResultProductDto>>(values);
            return Ok(values2);
        }




        [HttpGet("GetProductsWithCategoryBySize/{size}")]
        public async Task<IActionResult> GetProductsWithCategoryBySize(string size)
        {
            var values = await _productService.TGetProductsBySize(size);
            var values2 = _mapper.Map<List<ResultProductDto>>(values);
            return Ok(values2);
        }


        [HttpGet("GetProductsWithCategoryByColorId/{ID}")]
        public async Task<IActionResult> GetProductsWithCategoryByColorId(string ID)
        {
            var values = await _productService.TGetProductsByColorId(ID);
            var values2 = _mapper.Map<List<ResultProductDto>>(values);
            return Ok(values2);
        }



        [HttpGet("DeleteFavourite/{id}")]
        public async Task<IActionResult> DeleteFavourite(string id)
        {
            await _productService.TDeleteFavourite(id);
            return Ok("Favorilerden kaldırıldı.");
        }


        [HttpGet("AddFavourite/{id}")]
        public async Task<IActionResult> AddFavourite(string id)
        {
            await _productService.TAddFavourite(id);
            return Ok("Favorilere eklendi.");
        }




        [HttpGet("DeleteCompare/{id}")]
        public async Task<IActionResult> DeleteCompare(string id)
        {
            await _productService.TDeleteCompare(id);
            return Ok("Karşılaştırmadan kaldırıldı.");
        }


        [HttpGet("AddCompare/{id}")]
        public async Task<IActionResult> AddCompare(string id)
        {
            await _productService.TAddCompare(id);
            return Ok("Karşılaştırmaya eklendi.");
        }
    }
}
