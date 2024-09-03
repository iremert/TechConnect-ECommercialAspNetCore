using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechConnect.DtoUI.TestimonialDtos
{
    public class GetByIdTestimonialDto
    {
        public string ID { get; set; }
        public string Quote { get; set; }
        public string TestimonialName { get; set; }
        public string TestimonialJob { get; set; }
        public string TestimonialImageUrl { get; set; }
        public string TestimonialImageUrl2 { get; set; }
        public bool Status { get; set; }
    }
}
