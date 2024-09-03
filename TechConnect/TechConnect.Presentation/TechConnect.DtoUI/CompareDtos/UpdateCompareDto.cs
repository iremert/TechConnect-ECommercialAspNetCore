using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechConnect.EL.Concrete;

namespace TechConnect.DtoUI.CompareDtos
{
    public class UpdateCompareDto
    {
        public string ID { get; set; }
        public string ProductID { get; set; }
        public Product Product { get; set; }
        public string UserID { get; set; }
    }
}
