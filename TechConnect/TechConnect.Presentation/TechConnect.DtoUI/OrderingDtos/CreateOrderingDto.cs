using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechConnect.EL.Concrete;

namespace TechConnect.DtoUI.OrderingDtos
{
    public class CreateOrderingDto
    {
        public string UserId { get; set; }
        public string AddressId { get; set; }
        public string BasketTotalId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime OrderDeliveryDate { get; set; }
        public OrderState OrderState { get; set; }
    }
}
