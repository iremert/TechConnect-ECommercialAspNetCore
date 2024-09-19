using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechConnect.BLL.Abstract;
using TechConnect.Dto.CommentDtos;
using TechConnect.EL.Concrete;

namespace TechConnect.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;

        public CommentController(ICommentService commentService, IMapper mapper)
        {
            _commentService = commentService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> CommentList()
        {
            var values = await _commentService.TGetAllAsync();
            var values2 = _mapper.Map<List<ResultCommentDto>>(values);
            return Ok(values2);
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment(CreateCommentDto createCommentDto)
        {

            await _commentService.TCreateAsync(_mapper.Map<Comment>(createCommentDto));
            return Ok("Yorum kısmı başarıyla eklendi.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(string id)
        {
            await _commentService.TDeleteAsync(id);
            return Ok("Yorum kısmı başarıyla silindi.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComment(UpdateCommentDto updateCommentDto, string id)
        {
            await _commentService.TUpdateAsync(_mapper.Map<Comment>(updateCommentDto), id);
            return Ok("Yorum kısmı başarıyla güncellendi.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdComment(string id)
        {
            var value = await _commentService.TGetByIdAsync(id);
            var value2 = _mapper.Map<GetByIdCommentDto>(value);
            return Ok(value2);
        }
    }
}
