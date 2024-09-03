using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechConnect.EL.Concrete
{
    public class Payment : IEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ID { get; set; }
        public string UserId { get; set; }
        public string CartNumber { get; set; } //16 haneli
        public int ExpirationDateMonth { get; set; }
        public decimal ExpirationDateYear { get; set; }
        public decimal CVV { get; set; }
    }
}
