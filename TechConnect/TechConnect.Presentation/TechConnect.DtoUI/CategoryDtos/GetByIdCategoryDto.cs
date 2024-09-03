using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechConnect.DtoUI.CategoryDtos
{
    public class GetByIdCategoryDto
    {
        public string ID { get; set; }

        public string Name { get; set; }
        public int Count { get; set; }
        public List<string> Size { get; set; }

    }
}
