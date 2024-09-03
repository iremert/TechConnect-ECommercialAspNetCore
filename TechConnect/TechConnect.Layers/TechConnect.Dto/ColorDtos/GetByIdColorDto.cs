using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechConnect.Dto.ColorDtos
{
    public class GetByIdColorDto
    {
        public string ID { get; set; }
        public string ColorName { get; set; }
        public bool Status { get; set; }
    }
}
