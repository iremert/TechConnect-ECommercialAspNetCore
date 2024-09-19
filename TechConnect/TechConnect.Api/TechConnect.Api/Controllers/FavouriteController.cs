using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechConnect.BLL.Abstract;
using TechConnect.Dto.FavouriteDtos;
using TechConnect.EL.Concrete;

namespace TechConnect.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FavouriteController : ControllerBase
    {
        private readonly IFavouriteService _favouriteService;
        private readonly IMapper _mapper;

        public FavouriteController(IFavouriteService favouriteService, IMapper mapper)
        {
            _favouriteService = favouriteService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> FavouriteList()
        {
            var values = await _favouriteService.TGetAllAsync();
            var values2 = _mapper.Map<List<ResultFavouriteDto>>(values);
            return Ok(values2);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFavourite(CreateFavouriteDto createFavouriteDto)
        {

            await _favouriteService.TCreateAsync(_mapper.Map<Favourite>(createFavouriteDto));
            return Ok("Favoriler kısmı başarıyla eklendi.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFavourite(string id)
        {
            await _favouriteService.TDeleteAsync(id);
            return Ok("Favoriler kısmı başarıyla silindi.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFavourite(UpdateFavouriteDto updateFavouriteDto, string id)
        {
            await _favouriteService.TUpdateAsync(_mapper.Map<Favourite>(updateFavouriteDto), id);
            return Ok("Favoriler kısmı başarıyla güncellendi.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdFavourite(string id)
        {
            var value = await _favouriteService.TGetByIdAsync(id);
            var value2 = _mapper.Map<GetByIdFavouriteDto>(value);
            return Ok(value2);
        }



        [HttpGet("GetAllFavouriteWithProductByUserID")]
        public async Task<IActionResult> GetAllFavouriteWithProductByUserID()
        {
            var values = await _favouriteService.TGetAllFavouriteWithProductByUserID();
            var values2 = _mapper.Map<List<ResultFavouriteDto>>(values);
            return Ok(values2);
        }
    }
}
