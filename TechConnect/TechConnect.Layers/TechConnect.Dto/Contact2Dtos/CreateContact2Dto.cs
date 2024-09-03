using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechConnect.Dto.Contact2Dtos
{
    public class CreateContact2Dto
    {
        public string Place { get; set; }
        public string Address { get; set; }
        public string Number { get; set; }
        public bool Status { get; set; }
    }
}
