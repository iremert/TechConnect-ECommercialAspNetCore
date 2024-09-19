using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechConnect.EL.Concrete
{
    public class Ordering:IEntity
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ID { get; set; }
        public string UserId { get; set; }
        public string AddressId { get; set; }
        [BsonIgnore]
        public Address Address { get; set; }
        public string BasketTotalId{ get; set; }
        [BsonIgnore]
        public BasketTotal BasketTotal { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime OrderDeliveryDate { get; set; }
        public OrderState OrderState { get; set; }

    }
}
