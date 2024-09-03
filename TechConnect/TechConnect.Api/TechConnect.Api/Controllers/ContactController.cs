using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechConnect.BLL.Abstract;
using TechConnect.Dto.ContactDtos;
using TechConnect.EL.Concrete;

namespace TechConnect.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;
        private readonly IMapper _mapper;

        public ContactController(IContactService contactService, IMapper mapper)
        {
            _contactService = contactService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> ContactList()
        {
            var values = await _contactService.TGetAllAsync();
            var values2 = _mapper.Map<List<ResultContactDto>>(values);
            return Ok(values2);
        }

        [HttpPost]
        public async Task<IActionResult> CreateContact(CreateContactDto createContactDto)
        {

            await _contactService.TCreateAsync(_mapper.Map<Contact>(createContactDto));
            return Ok("İletişim kısmı başarıyla eklendi.");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteContact(string id)
        {
            await _contactService.TDeleteAsync(id);
            return Ok("İletişim kısmı başarıyla silindi.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateContact(UpdateContactDto updateContactDto, string id)
        {
            await _contactService.TUpdateAsync(_mapper.Map<Contact>(updateContactDto), id);
            return Ok("İletişim kısmı başarıyla güncellendi.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdContact(string id)
        {
            var value = await _contactService.TGetByIdAsync(id);
            var value2 = _mapper.Map<GetByIdContactDto>(value);
            return Ok(value2);
        }
    }
}
