using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechConnect.BLL.Abstract;
using TechConnect.Dto.Contact2Dtos;
using TechConnect.Dto.ContactDtos;
using TechConnect.EL.Concrete;

namespace TechConnect.Api.Controllers
{
    //[Authorize(Policy = "ContactFullPermission")]
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class Contact2Controller : ControllerBase
    {
        private readonly IContact2Service _contact2Service;
        private readonly IMapper _mapper;

        public Contact2Controller(IContact2Service contact2Service, IMapper mapper)
        {
            _contact2Service = contact2Service;
            _mapper = mapper;
        }



        [HttpGet]
        public async Task<IActionResult> Contact2List()
        {
            var values = await _contact2Service.TGetAllAsync();
            var values2 = _mapper.Map<List<ResultContact2Dto>>(values);
            return Ok(values2);
        }

        [HttpPost]
        public async Task<IActionResult> CreateContact2(CreateContact2Dto createContact2Dto)
        {

            await _contact2Service.TCreateAsync(_mapper.Map<Contact2>(createContact2Dto));
            return Ok("İletişim kısmı başarıyla eklendi.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact2(string id)
        {
            await _contact2Service.TDeleteAsync(id);
            return Ok("İletişim kısmı başarıyla silindi.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContact2(UpdateContact2Dto updateContact2Dto, string id)
        {
            await _contact2Service.TUpdateAsync(_mapper.Map<Contact2>(updateContact2Dto), id);
            return Ok("İletişim kısmı başarıyla güncellendi.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdContact2(string id)
        {
            var value = await _contact2Service.TGetByIdAsync(id);
            var value2 = _mapper.Map<GetByIdContact2Dto>(value);
            return Ok(value2);
        }
    }
}
