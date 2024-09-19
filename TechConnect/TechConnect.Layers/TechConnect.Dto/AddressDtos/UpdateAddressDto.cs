using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechConnect.Dto.AddressDtos
{
    public class UpdateAddressDto
    {
        public string ID { get; set; }
        public string UserId { get; set; }

        //iletişim bilgileri
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        //adres bilgileri
        public string City { get; set; } //il
        public string District { get; set; }//ilçe
        public string District2 { get; set; }//mahalle
        public string RealAddress { get; set; }
        public string AddressTitle { get; set; }
        public string ZipCode { get; set; }
    }
}
