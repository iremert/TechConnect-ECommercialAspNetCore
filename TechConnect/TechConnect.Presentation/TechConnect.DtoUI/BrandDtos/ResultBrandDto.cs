using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechConnect.DtoUI.BrandDtos
{
    public class ResultBrandDto
    {
        public ResultBrandDto()
        {
            BrandNames = new List<string>();
        }

        public List<string> BrandNames { get; set; }
    }
}
