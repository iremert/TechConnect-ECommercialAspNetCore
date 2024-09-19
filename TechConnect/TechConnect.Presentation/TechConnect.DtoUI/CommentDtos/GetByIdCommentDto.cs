using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechConnect.DtoUI.CommentDtos
{
    public class GetByIdCommentDto
    {
        public string ID { get; set; }
        public string UserId { get; set; }
        public string CommentReal { get; set; }
        public int Rating { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Status { get; set; }
        public string ProductId { get; set; }
    }
}
